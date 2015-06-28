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
    public class BookMediaViewModel : BindableBase
    {
        private IEventAggregator eventAggregator;
        private SubscriptionToken token;
        private string _bookMedia;

        public string BookMedia
        {
            set
            {
                _bookMedia = value;
                OnPropertyChanged("BookMedia");
            }
            get
            {
                return _bookMedia;
            }
        }

        public BookMediaViewModel()
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
                BookMedia = book.MediaFile;
            }
            else
            {
                BookMedia = "";
            }
        }
    }
}
