using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iBooks.Models;
using Microsoft.Practices.Prism.Mvvm;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using iBooks.Notifications;

namespace iBooks.ViewModels
{
    public class AddBookViewModel : BindableBase, IInteractionRequestAware
    {
        public string BookName { set; get; }
        public string BookCoverImg { set; get; }
        public string BookMediaPath { set; get; }
        public string BookCategory { set; get; }
        public string BookUri { set; get; }
        public Action FinishInteraction { get; set; }

        private AddBookNotification notification;
        private List<string> _bookCategories = null;

        public AddBookViewModel()
        {
            InitializeBookCategories();

            SetupCommands();
        }


        public INotification Notification
        {
            get
            {
                return this.notification;
            }
            set
            {
                if (value is AddBookNotification)
                {
                    this.notification = value as AddBookNotification;
                    this.OnPropertyChanged(() => this.Notification);
                }
            }
        }

        public List<String> BookCategories
        {
            get
            {
                return this._bookCategories;
            }

            private set
            {
                this._bookCategories = value;
            }

        }
           


        public ICommand SubmitAddBook
        {
            set;
            get;
        }

        public ICommand CancelAddBook
        {
            set;
            get;
        }

        private void InitializeBookCategories()
        {
            List<String> cats = new List<string>();
            cats.Add(Book.BookCategory.History.ToString());
            cats.Add(Book.BookCategory.ScienceTech.ToString());
            cats.Add(Book.BookCategory.Literature.ToString());

            BookCategories = cats;
        }

        private void SetupCommands()
        {
            this.SubmitAddBook = new DelegateCommand(this.OnSubmitAddNewBook);
            this.CancelAddBook = new DelegateCommand(this.OnCancelAddNewBook);
           
        }

        private void OnSubmitAddNewBook()
        {
            if (null != this.notification)
            {
                Book book = new Book();
                book.BookName = BookName;
                book.CoverImg = BookCoverImg;
                book.MediaFile = BookMediaPath;
                book.Category = Book.ParseBookCategory(this.BookCategory);
                book.BookUri = BookUri;

                this.notification.AddedBook = book;
                this.notification.Confirmed = true;
            }
            this.FinishInteraction();
        }

        private void OnCancelAddNewBook()
        {
            this.FinishInteraction();
        }


    }
}
