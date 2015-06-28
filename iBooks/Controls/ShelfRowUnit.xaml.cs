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
using iBooks.Models;

namespace iBooks.Controls
{
    /// <summary>
    /// Interaction logic for ShelfRowUnit.xaml
    /// </summary>
    public partial class ShelfRowUnit : UserControl
    {
        public static readonly RoutedEvent BookItemClickEvent;
        public static readonly RoutedEvent BookItemRemovedEvent;
        public static readonly DependencyProperty BooksRowProperty;


        static ShelfRowUnit()
        {
            BookItemClickEvent = EventManager.RegisterRoutedEvent("BookItemClicked", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<BookItemView>), typeof(ShelfRowUnit));

            BookItemRemovedEvent = EventManager.RegisterRoutedEvent("BookItemRemoved", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<BookItemView>), typeof(ShelfRowUnit));

            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(OnBooksRowChanged));
            BooksRowProperty = DependencyProperty.Register("BooksRow", typeof(ShelfRowBooks), typeof(ShelfRowUnit), metadata, null);

        }

        public ShelfRowUnit()
        {
            InitializeComponent();
           
            //SetBindings();
        }

        static public void OnBooksRowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ShelfRowUnit sender = d as ShelfRowUnit;
            sender.ReArrangeRowBooks();
        }

        public void ShowRemoveBooksButtons()
        {
            foreach (BookItemView bookItem in this.RowUnit.Children)
            {
                bookItem.ShowRemoveBookBtn();
            }
        }

        //public void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("DependencyPropertyChangedEventArgs");
        //}

        public ShelfRowBooks BooksRow
        {
            set
            {
                SetValue(BooksRowProperty, value);
            }
            get
            {
                return (ShelfRowBooks)GetValue(BooksRowProperty);
            }
        }


        public event RoutedPropertyChangedEventHandler<BookItemView> BookItemClicked
        {
            add
            {
                AddHandler(BookItemClickEvent, value);
            }
            remove
            {
                RemoveHandler(BookItemClickEvent, value);
            }
        }

        public event RoutedPropertyChangedEventHandler<BookItemView> BookItemRemoved
        {
            add
            {
                AddHandler(BookItemRemovedEvent, value);
            }
            remove
            {
                RemoveHandler(BookItemRemovedEvent, value);
            }
        }


        private void OnClickBookItem(object sender, RoutedPropertyChangedEventArgs<BookItemView> args)
        {
            RoutedPropertyChangedEventArgs<BookItemView> arg = new RoutedPropertyChangedEventArgs<BookItemView>((BookItemView)sender, (BookItemView)sender);
            args.RoutedEvent = ShelfRowUnit.BookItemClickEvent;

            this.RaiseEvent(args);
        }

        private void OnBookItemRemoved(Object sender, RoutedPropertyChangedEventArgs<BookItemView> args)
        {
            RoutedPropertyChangedEventArgs<BookItemView> arg = new RoutedPropertyChangedEventArgs<BookItemView>((BookItemView)sender, (BookItemView)sender);
            args.RoutedEvent = ShelfRowUnit.BookItemRemovedEvent;

            this.RaiseEvent(args);
        }

        //private void SetBindings()
        //{
        //    //this.SetBinding(this.Book1, ShelfRowUnit.BooksRowProperty, "BooksRow");
        //}

        //private void SetBinding(FrameworkElement obj, DependencyProperty p, string path)
        //{
        //    Binding b = new Binding();
        //    b.Source = this;
        //    b.Path = new PropertyPath(path);
        //    b.Mode = BindingMode.OneWay;
        //    obj.SetBinding(p, b);
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(this.BooksRow.RowName);


            //Binding bind = BindingOperations.GetBinding(this.BTest, TextBlock.TextProperty);

            //Binding bid2 = bind;

            //this.BTest.Text = this.BooksRow.RowName;
        }

        private void ReArrangeRowBooks()
        {
            this.RowUnit.ColumnDefinitions.Clear();

            for (int i = 0; i < ShelfRowBooks.BOOKS_PER_ROW; i++)
            {
                this.RowUnit.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < this.BooksRow.BookCount; i++)
            {
                BookItemView bookItem = new BookItemView();
                bookItem.Margin = new Thickness(10.0, 10.0, 10.0, 2.0);
                bookItem.BookItem = this.BooksRow.Books[i];
                bookItem.ItemClicked += OnClickBookItem;
                bookItem.ItemRemoved += OnBookItemRemoved;
                bookItem.CanRemoveBookItem = BooksRow.CanRemoveBooks;
                this.RowUnit.Children.Add(bookItem);
                Grid.SetColumn(bookItem, i);
            }
        }
    }
}
