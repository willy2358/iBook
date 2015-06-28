using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBooks.Models;
using System.ComponentModel;

namespace iBooks.Controls
{
    public class ShelfRowBooks : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public const int BOOKS_PER_ROW = 5;
          
        private Book[] _books = new Book[BOOKS_PER_ROW];

        protected void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public ShelfRowBooks()
        {
            RowName = "default row name";
        }

        public String RowName
        {
            set;
            get;
        }

        public Book[] Books
        {
            get
            {
                return _books;
            }
        }

        public int BookCount
        {
            set;
            get;
        }

        public bool CanRemoveBooks
        {
            set;
            get;
        }
        
    }
}
