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
using System.Windows.Shapes;

using iBooks.ViewModels;

namespace iBooks.Views
{
    /// <summary>
    /// Interaction logic for AddBook.xaml
    /// </summary>
    public partial class AddBookView : UserControl
    {
        public AddBookView()
        {
            this.DataContext = new AddBookViewModel();
            InitializeComponent();
        }

        private void BrowseBookCover(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDlg = new System.Windows.Forms.OpenFileDialog();
            fileDlg.Filter = "image files (*.jpg;*.bmp;*.png)|*.jpg;*.bmp;*.png||";
            fileDlg.FilterIndex = 1;
            fileDlg.RestoreDirectory = true;
            if (fileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BookCover.SetValue(TextBox.TextProperty, fileDlg.FileName);
                ((AddBookViewModel)this.DataContext).BookCoverImg = fileDlg.FileName;
            }
        }

        private void BrowseBookMedia(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDlg = new System.Windows.Forms.OpenFileDialog();
            fileDlg.Filter = "image files (*.wmv;*.mp4;*.avi;*.mp3)|*.wmv;*.mp4;*.avi;*.mp3||";
            fileDlg.FilterIndex = 1;
            fileDlg.RestoreDirectory = true;
            if (fileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BookMedia.SetValue(TextBox.TextProperty, fileDlg.FileName);
                ((AddBookViewModel)this.DataContext).BookMediaPath = fileDlg.FileName;
            }
        }

        private void BrowseBookFile(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDlg = new System.Windows.Forms.OpenFileDialog();
            fileDlg.Filter = "book files (*.pdf;*.html;*.htm)|*.pdf;*.html;*.htm||";
            fileDlg.FilterIndex = 1;
            fileDlg.RestoreDirectory = true;
            if (fileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtBookUri.SetValue(TextBox.TextProperty, fileDlg.FileName);
                ((AddBookViewModel)this.DataContext).BookUri = fileDlg.FileName;
            }
        }
    }
}
