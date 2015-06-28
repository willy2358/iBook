using iBooks.Events;
using iBooks.Models;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBooks.ViewModels
{
    public class BookCoverViewModel : BindableBase
    {
        private IEventAggregator eventAggregator;
        private SubscriptionToken token;
        private string _bookCover;
        public string BookCover
        {
            set
            {
                _bookCover = value;
                OnPropertyChanged("BookCover");
            }
            get
            {
                return _bookCover;
            }
        }

        public BookCoverViewModel()
        {
            SubscribeBookChangeEvent();
        }

        private void SubscribeBookChangeEvent()
        {
            eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            token = eventAggregator.GetEvent<FocusBookChangedEvent>().Subscribe(FocusBookChanged);
        }

        private void FocusBookChanged(Book book)
        {
            if (null != book)
            {
                BookCover = book.CoverImg;
            }
            else
            {
                BookCover = "";
            }
        }
    }
}
