using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iBooks.Models;
using Microsoft.Practices.Prism.Mvvm;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using iBooks.Notifications;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.PubSubEvents;
using iBooks.Events;
using iBooks.Controls;
using System.Collections.ObjectModel;

namespace iBooks.ViewModels
{
    public class ShelfFrameViewModel : BindableBase
    {
        private IEventAggregator eventAggregator;

        private Book _currentBook = null;

        private bool _canRemoveBooks = false;
        

        public ShelfFrameViewModel()
        {
            LoadBookRows();

            SetupRequests();

            SetupCommands();

            SetupEventPublisher();
        }

        public void LoadBookRows()
        {
            this.BookRows = new ShelfBookRows();
            CanRemoveBooks = false;
            ReloadBookRows();
        }

        public ShelfBookRows BookRows
        {
            set;
            get;
        }

        public InteractionRequest<AddBookNotification> AddBookRequest 
        {
            get; 
            private set; 
        }

        public bool CanRemoveBooks
        {
            set
            {
                _canRemoveBooks = value;
                //foreach (ShelfRowBooks row in BookRows)
                //{
                //    row.CanRemoveBooks = value;
                //}
                //OnPropertyChanged("BookRows");
                ReloadBookRows();
            }
            get
            {
                return _canRemoveBooks;
            }
        }

        public ICommand AddNewBookCommand
        {
            get;
            private set;
        }

        public ICommand RemoveBookCommand
        {
            get;
            private set;
        }

        public ICommand ViewBookInfoCommand
        {
            get;
            private set;
        }

        private void SetupRequests()
        {
            this.AddBookRequest = new InteractionRequest<AddBookNotification>();
        }

        private void SetupEventPublisher()
        {
            this.eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        }

        private void SetupCommands()
        {
            this.AddNewBookCommand = new DelegateCommand<object>(this.OnAddNewBook);
            this.ViewBookInfoCommand = new DelegateCommand<object>(this.OnViewBookInfo);
            this.RemoveBookCommand = new DelegateCommand(this.OnRemoveBook);
        }

        private void OnAddNewBook(object bookview)
        {
            AddBookNotification notification = new AddBookNotification();
            notification.Title = "Add Book Item";
            this.AddBookRequest.Raise(notification,
                 returned =>
                 {
                     if (returned != null && returned.Confirmed && returned.AddedBook != null)
                     {
                         BooksManager.AddBookLibrary(returned.AddedBook);
                         ReloadBookRows();
                     }
                 });
        }

        private void OnRemoveBook()
        {
            if (null != this._currentBook)
            {
                BooksManager.RemoveBook(this._currentBook);
                this._currentBook = null;
                OnViewBookInfo(null);
            }
        }

        private void OnViewBookInfo(Object obj)
        {
            Book book = (Book)obj;
            this._currentBook = book;
            eventAggregator.GetEvent<FocusBookChangedEvent>().Publish(book);
        }

        public void ReloadBookRows()
        {
            int rows;
            List<ShelfRowBooks> bookRows = SplitBooksIntoBookRows(out rows);

            AppendLastBlankBookForAddNew(rows, bookRows);

            UpdateBookRows(bookRows);
        }

        private void UpdateBookRows(List<ShelfRowBooks> bookRows)
        {
            this.BookRows.Clear();
            for (int i = 0; i < bookRows.Count; i++)
            {
                this.BookRows.Add(bookRows[i]);
            }
        }

        private static void AppendLastBlankBookForAddNew(int rows, List<ShelfRowBooks> bookRows)
        {
            if (bookRows[rows - 1].BookCount == ShelfRowBooks.BOOKS_PER_ROW)
            {
                ShelfRowBooks blankRow = new ShelfRowBooks();
                Book blankBook = new Book();
                blankRow.Books[0] = blankBook;
                blankRow.BookCount = 1;
                bookRows.Add(blankRow);
            }
            else
            {
                ShelfRowBooks lastRow = bookRows[rows - 1];
                lastRow.Books[lastRow.BookCount] = new Book();
                lastRow.BookCount++;
            }
        }

        private List<ShelfRowBooks> SplitBooksIntoBookRows(out int rows)
        {
            List<ShelfRowBooks> bookRows = new List<ShelfRowBooks>();
            rows = BooksManager.AllBooks.Count / ShelfRowBooks.BOOKS_PER_ROW;
            rows += BooksManager.AllBooks.Count % ShelfRowBooks.BOOKS_PER_ROW == 0 ? 0 : 1;

            for (int i = 0; i < rows; i++)
            {
                ShelfRowBooks rowBooks = new ShelfRowBooks();
                rowBooks.CanRemoveBooks = CanRemoveBooks;
                rowBooks.RowName = string.Format("book row :{0}", i + 1);
                for (int j = 0; j < ShelfRowBooks.BOOKS_PER_ROW; j++)
                {
                    int pos = i * ShelfRowBooks.BOOKS_PER_ROW + j;
                    if (pos < BooksManager.AllBooks.Count)
                    {
                        rowBooks.Books[j] = BooksManager.AllBooks[pos];
                        rowBooks.BookCount = j + 1;
                    }
                }
                bookRows.Add(rowBooks);
            }

            return bookRows;
        }
    }
}
