using iBooks.Models;
using Microsoft.Practices.Prism.Commands;
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

namespace iBooks.Controls
{
    /// <summary>
    /// Interaction logic for BookView.xaml
    /// </summary>
    public partial class BookItemView : UserControl
    {
        public static readonly RoutedEvent ItemClickEvent;
        public static readonly RoutedEvent ItemRemovedEvent;
        public static readonly DependencyProperty BookCoverProperty;
        public static readonly DependencyProperty BookItemProperty;

        private Book _bookItem = null;
        private bool _CanRemove = false;

        static BookItemView()
        {
            ItemClickEvent = EventManager.RegisterRoutedEvent("ItemClicked", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<BookItemView>), typeof(BookItemView));

            ItemRemovedEvent = EventManager.RegisterRoutedEvent("BookItemRemoved", RoutingStrategy.Bubble,
                              typeof(RoutedPropertyChangedEventHandler<BookItemView>), typeof(BookItemView));

            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure);
            BookCoverProperty = DependencyProperty.Register("BookCover", typeof(ImageSource), typeof(BookItemView), metadata, null);


            metadata = new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(OnBookItemChanged));
            BookItemProperty = DependencyProperty.Register("BookItem", typeof(Book), typeof(BookItemView), metadata, null);

        }

        static private void OnBookItemChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnBookItemChanged");
        }

        public BookItemView()
        {
            InitializeComponent();

            this.SetBinding(this.MyCover, Image.SourceProperty, "BookCover");
            this.SetBinding(this.container, BookItemView.BookItemProperty, "BookItem");

            InitDefaultBookCoverView();
        }


        public event RoutedPropertyChangedEventHandler<BookItemView> ItemClicked
        {
            add
            {
                AddHandler(ItemClickEvent, value);
            }
            remove
            {
                RemoveHandler(ItemClickEvent, value);
            }
        }

        public event RoutedPropertyChangedEventHandler<BookItemView> ItemRemoved
        {
            add
            {
                AddHandler(ItemRemovedEvent, value);
            }
            remove
            {
                RemoveHandler(ItemRemovedEvent, value);
            }
        }

        public ImageSource BookCover
        {
            get
            {
                return (ImageSource)GetValue(BookCoverProperty);
            }
            set
            {

                SetValue(BookCoverProperty, value);
            }
        }

        public bool CanRemoveBookItem
        {
            set
            {
                this._CanRemove = value;

                AdjustBookCoverImage();
            }
        }

        

        public void ShowRemoveBookBtn()
        {
            if (null != this.BookItem)
            {
                if (!this._CanRemove )
                {
                    if (!string.IsNullOrEmpty(this._bookItem.BookName))
                    {
                        this.RemoveBook.Visibility = Visibility.Visible;
                        Grid.SetRow(this.MyCover, 1);
                        Grid.SetRowSpan(this.MyCover, 3);
                        this._CanRemove = true;
                    }
                }
                else 
                {
                    Grid.SetRow(this.MyCover, 0);
                    Grid.SetRowSpan(this.MyCover, 4);
                    this.RemoveBook.Visibility = Visibility.Hidden;
                    this._CanRemove = false;
                }

            }
        }


        public Book BookItem
        {
            set
            {
                SetValue(BookItemProperty, value);
                this._bookItem = value;
                if (null != this._bookItem)
                {
                    if (!string.IsNullOrEmpty(this._bookItem.CoverImg)
                        && this._bookItem.BookName.Length > 2)
                    {
                        BookCover = (ImageSource)(new ImageSourceConverter().ConvertFromString(this._bookItem.CoverImg));
                        Grid.SetRow(this.MyCover, 0);
                        Grid.SetRowSpan(this.MyCover, 4);
                        MyBorder.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        InitDefaultBookCoverView();
                    }
                }

                if (null == this._bookItem)
                {
                    this.Visibility = System.Windows.Visibility.Hidden;
                }
            }
            get
            {
                return (Book)GetValue(BookItemProperty);
            }
        }

        private void OnClickItem(object sender, MouseEventArgs arg)
        {
            RoutedPropertyChangedEventArgs<BookItemView> args = new RoutedPropertyChangedEventArgs<BookItemView>(this, this);
            args.RoutedEvent = BookItemView.ItemClickEvent;

            this.RaiseEvent(args);
        }

        private void OnDelBook(object sender, MouseEventArgs arg)
        {
            BooksManager.RemoveBook(this.BookItem);

            RoutedPropertyChangedEventArgs<BookItemView> args = new RoutedPropertyChangedEventArgs<BookItemView>(this, this);
            args.RoutedEvent = BookItemView.ItemRemovedEvent;

            this.RaiseEvent(args);
        }

        private void InitDefaultBookCoverView()
        {
            string defCover = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "res", "addbook.png");
            this.BookCover = (ImageSource)(new ImageSourceConverter().ConvertFromString(defCover));
            Grid.SetRow(this.MyCover, 2);
            Grid.SetRowSpan(this.MyCover, 1);
        }

        private void SetBinding(FrameworkElement obj, DependencyProperty p, string path)
        {
            Binding b = new Binding();
            b.Source = this;
            b.Path = new PropertyPath(path);
            b.Mode = BindingMode.OneWay;
            obj.SetBinding(p, b);
        }

        private void AdjustBookCoverImage()
        {
            if (null == this._bookItem || string.IsNullOrEmpty(this._bookItem.BookName))
            {
                return;
            }

            if (this._CanRemove)
            {
                this.RemoveBook.Visibility = Visibility.Visible;
                Grid.SetRow(this.MyCover, 1);
                Grid.SetRowSpan(this.MyCover, 3);
            }
            else
            {
                Grid.SetRow(this.MyCover, 0);
                Grid.SetRowSpan(this.MyCover, 4);
                this.RemoveBook.Visibility = Visibility.Hidden;
            }
        }
    }
}
