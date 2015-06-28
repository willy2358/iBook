using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iBooks.Views
{
    /// <summary>
    /// Interaction logic for BookMediaView.xaml
    /// </summary>
    public partial class BookMediaView : UserControl, IView
    {
        public static readonly RoutedEvent MaxingViewEvent;

        private bool _viewMaxed = false;

        static BookMediaView()
        {
            MaxingViewEvent = EventManager.RegisterRoutedEvent("MaxingView", RoutingStrategy.Bubble,
                               typeof(RoutedPropertyChangedEventHandler<BookMediaView>), typeof(BookMediaView));

        }

        public BookMediaView()
        {
            InitializeComponent();
        }

        public event RoutedPropertyChangedEventHandler<BookCoverView> MaxingView
        {
            add
            {
                AddHandler(MaxingViewEvent, value);
            }
            remove
            {
                RemoveHandler(MaxingViewEvent, value);
            }
        }

        public void OnMaxingView(object sender, MouseButtonEventArgs e)
        {
            if (!this._viewMaxed)
            {
                string restoreImg = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "res", "restoreBtn.png");
                this.MaxingViewBtn.Source = (ImageSource)(new ImageSourceConverter().ConvertFromString(restoreImg));
                this._viewMaxed = true;
            }
            else
            {
                string maxImg = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "res", "maxbtn.png");
                this.MaxingViewBtn.Source = (ImageSource)(new ImageSourceConverter().ConvertFromString(maxImg));
                this._viewMaxed = false;
            }

            RoutedPropertyChangedEventArgs<BookMediaView> args = new RoutedPropertyChangedEventArgs<BookMediaView>(this, this);
            args.RoutedEvent = BookMediaView.MaxingViewEvent;

            this.RaiseEvent(args);
        }

        private void MediaElement_MediaOpened_1(object sender, RoutedEventArgs e)
        {
            this.MaxingViewBtn.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
