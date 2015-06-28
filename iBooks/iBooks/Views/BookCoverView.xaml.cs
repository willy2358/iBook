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
    /// Interaction logic for BookCoverView.xaml
    /// </summary>
    public partial class BookCoverView : UserControl, IView
    {
        public static readonly RoutedEvent MaxingViewEvent;

        private bool _viewMaxed = false;

        static BookCoverView()
        {
            MaxingViewEvent = EventManager.RegisterRoutedEvent("MaxingView", RoutingStrategy.Bubble,
                               typeof(RoutedPropertyChangedEventHandler<BookCoverView>), typeof(BookCoverView));

        }
        public BookCoverView()
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

            RoutedPropertyChangedEventArgs<BookCoverView> args = new RoutedPropertyChangedEventArgs<BookCoverView>(this, this);
            args.RoutedEvent = BookCoverView.MaxingViewEvent;
            this.RaiseEvent(args);
        }
    }
}
