using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace iBooks.Models
{
    static class BooksManager
    {
        static private List<Book> _Books = null;

        static private bool LoadBooks()
        {
            if (Properties.Settings.Default.MyBooks.Length < 1)
            {
                _Books = new List<Book>();
                Book book = new Book();
                book.BookName = "WPF高级编程";
                book.CoverImg = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "res", "wpf1.jpg");
                book.MediaFile = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "res", "wpf.mp4");
                book.Category = Book.BookCategory.ScienceTech;
                _Books.Add(book);

                book = new Book();
                book.BookName = "WPF高级编程";
                book.CoverImg = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "res", "wpf1.jpg");
                book.MediaFile = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "res", "wpf.mp4");
                book.Category = Book.BookCategory.ScienceTech;
                _Books.Add(book);

                book = new Book();
                book.BookName = "WPF高级编程";
                book.CoverImg = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "res", "wpf1.jpg");
                book.MediaFile = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "res", "wpf.mp4");
                book.Category = Book.BookCategory.ScienceTech;
                _Books.Add(book);

                book = new Book();
                book.BookName = "WPF高级编程";
                book.CoverImg = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "res", "wpf1.jpg");
                book.MediaFile = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "res", "wpf.mp4");
                book.Category = Book.BookCategory.ScienceTech;
                _Books.Add(book);

                book = new Book();
                book.BookName = "WPF高级编程";
                book.CoverImg = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "res", "wpf1.jpg");
                book.MediaFile = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "res", "wpf.mp4");
                book.Category = Book.BookCategory.ScienceTech;
                _Books.Add(book);

            }
            else
            {
                ParseBooksItems(Properties.Settings.Default.MyBooks);
            }

            return true;
        }

        static public void RemoveBook(Book book)
        {
            _Books.Remove(book);
        }

        static public void SaveBooksLibrary()
        {
            string books = "";
            foreach (Book book in _Books)
            {
                string item = string.Format("{0},{1},{2},{3};", book.BookName, book.BookUri, book.CoverImg, book.MediaFile);
                //string item = string.Format("{0};", book.BookName);
                books += item;
            }

            books.TrimEnd(';');
            Properties.Settings.Default.MyBooks = books;
            Properties.Settings.Default.Save();
        }

        static public List<Book> AllBooks
        {
            get 
            {
                if (null == _Books)
                {
                    LoadBooks();
                }
                return _Books; 
            }
        }

        static public void AddBookLibrary(Book newBook)
        {
            _Books.Add(newBook);
        }

        static void ParseBooksItems(String items)
        {
            _Books = new List<Book>();
            string[] books = items.Split(';');
            foreach (string book in books)
            {
                if (string.IsNullOrEmpty(book))
                {
                    continue;
                }

                string[] entries = book.Split(',');
                if (entries.Length < 4)
                {
                    continue;
                }
                //Book.BookCategory cat = Book.ParseBookCategory(entries[1]);
                Book b = new Book(entries[0], Book.BookCategory.Other, entries[2], entries[3]);
                b.BookUri = entries[1];
                _Books.Add(b);
            }
        }

        
    }
}
