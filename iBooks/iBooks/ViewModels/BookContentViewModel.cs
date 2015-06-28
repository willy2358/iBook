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
    class BookContentViewModel : BindableBase
    {
        private IEventAggregator eventAggregator;
        private SubscriptionToken token;
        private string _bookUri;
        public string BookUri
        {
            set
            {
                _bookUri = value;
                OnPropertyChanged("BookUri");
            }
            get
            {
                return _bookUri;
            }
        }

        public BookContentViewModel()
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
                BookUri = book.BookUri;
            }
            else
            {
                BookUri = "";
            }
        }
    }
}
