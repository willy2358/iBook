using iBooks.Controls;
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
    /// Interaction logic for BookContentView.xaml
    /// </summary>
    public partial class BookContentView : UserControl
    {
        public static readonly RoutedEvent MaxingViewEvent;

        private bool _viewMaxed = false;

        static BookContentView()
        {
            MaxingViewEvent = EventManager.RegisterRoutedEvent("MaxingView", RoutingStrategy.Bubble,
                               typeof(RoutedPropertyChangedEventHandler<BookContentView>), typeof(BookContentView));

        }

        public BookContentView()
        {
            InitializeComponent();
        }

        public event RoutedPropertyChangedEventHandler<BookContentView> MaxingView
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

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            PdfReaderControl pdfCtrl = new PdfReaderControl(UriHelper.Text);
            pdfReaderHost.Child = pdfCtrl;
            this.MaxingViewBtn.Visibility = System.Windows.Visibility.Visible;
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

            RoutedPropertyChangedEventArgs<BookContentView> args = new RoutedPropertyChangedEventArgs<BookContentView>(this, this);
            args.RoutedEvent = BookContentView.MaxingViewEvent;
            this.RaiseEvent(args);
        }

    }
}
