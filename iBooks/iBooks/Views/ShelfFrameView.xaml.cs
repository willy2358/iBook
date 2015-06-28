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

using Microsoft.Practices.Prism;
using iBooks.ViewModels;
using iBooks.Controls;
using iBooks.Models;

namespace iBooks.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShelfFrameView : UserControl, IView
    {
        private bool _bookCoverMaxed = false;
        private bool _bookMediaMaxed = false;

        public ShelfFrameView()
        {
            InitializeComponent();
        }

        private void OnBookItemClicked(object sender, RoutedPropertyChangedEventArgs<Controls.BookItemView> e)
        {
            ShelfFrameViewModel sfvm = (ShelfFrameViewModel)this.DataContext;
            Book book = ((BookItemView)(e.NewValue)).BookItem;
            if (null == book || string.IsNullOrEmpty(book.CoverImg))
            {
                ((ShelfFrameViewModel)this.DataContext).AddNewBookCommand.Execute(e.NewValue);
            }
            else
            {
                ((ShelfFrameViewModel)this.DataContext).ViewBookInfoCommand.Execute(book);
            }
        }

        private void OnBookItemRemoved(object sender, RoutedPropertyChangedEventArgs<Controls.BookItemView> e)
        {
            ShelfFrameViewModel sfvm = (ShelfFrameViewModel)this.DataContext;
            sfvm.ReloadBookRows();
        }

        private void OnMaxingCoverView(object sender, RoutedPropertyChangedEventArgs<BookContentView> e)
        {
            if (this._bookCoverMaxed)
            {
                ShowOriginalParts();
                RestoreViewPart(this.CoverView, this.CoverViewHost);
                this._bookCoverMaxed = false;
                this.LeftPart.SelectedItem = null;
            }
            else
            {
                HideOriginalParts();
                ExpandViewPart(this.CoverView, this.CoverViewHost);
                this._bookCoverMaxed = true;
            }
        }

        private void OnMaxingMediaView(object sender, RoutedPropertyChangedEventArgs<BookMediaView> e)
        {
            if (this._bookMediaMaxed)
            {
                ShowOriginalParts();
                RestoreViewPart(this.MediaView, this.MediaViewHost);
                this._bookMediaMaxed = false;
                this.LeftPart.SelectedItem = null;
            }
            else
            {
                HideOriginalParts();
                ExpandViewPart(this.MediaView, this.MediaViewHost);
                this._bookMediaMaxed = true;
            }
        }

        private void ExpandViewPart(UIElement viewPart, Grid hostGrid)
        {
            hostGrid.Children.Remove(viewPart);
            this.Shelf.Children.Add(viewPart);
            Grid.SetColumn(viewPart, 0);
            Grid.SetColumnSpan(viewPart, 3);
        }

        private void RestoreViewPart(UIElement viewPart, Grid hostGrid)
        {
            this.Shelf.Children.Remove(viewPart);
            hostGrid.Children.Add(viewPart);
        }

        private void HideOriginalParts()
        {
            foreach (UIElement elem in this.Shelf.Children)
            {

                 elem.Visibility = System.Windows.Visibility.Collapsed;
                
            }
        }

        private void ShowOriginalParts()
        {
            foreach (UIElement elem in this.Shelf.Children)
            {
                elem.Visibility = System.Windows.Visibility.Visible;
            }
        }


        private void OnRemovingBooks(object sender, MouseButtonEventArgs e)
        {
            ShelfFrameViewModel sfvm = (ShelfFrameViewModel)this.DataContext;
            sfvm.CanRemoveBooks = !sfvm.CanRemoveBooks;

            //for (int i = 0; i < this.LeftPart.Items.Count; i++)
            //{
            //    ListBoxItem lbi = (ListBoxItem)this.LeftPart.ItemContainerGenerator.ContainerFromIndex(i);// as ShelfRowUnit;
            //    ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(lbi);
            //    DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            //    ShelfRowUnit target = (ShelfRowUnit)myDataTemplate.FindName("MyBooksRow", myContentPresenter);
            //    target.ShowRemoveBooksButtons();
            //}
        }

        //public void ListBoxMouseDouleClick(object sender, MouseButtonEventArgs e)
        //{
        //}


        //private childItem FindVisualChild<childItem>(DependencyObject obj)  where childItem : DependencyObject
        // {
        //     for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
        //     {
        //         DependencyObject child = VisualTreeHelper.GetChild(obj, i);
        //         if (child != null && child is childItem)
        //             return (childItem)child;
        //         else
        //         {
        //             childItem childOfChild = FindVisualChild<childItem>(child);
        //             if (childOfChild != null)
        //                 return childOfChild;
        //         }
        //     }
        //     return null;
        // }
    
    }
}
