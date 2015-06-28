using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using iBooks.Models;

namespace iBooks.Style
{
    public class BookItemStyleTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate
        {
            set;
            get;
        }

        public DataTemplate TechBookTemplate
        {
            get;
            set;
        }

        public DataTemplate HistoryBookTemplate
        {
            set;
            get;
        }

        public DataTemplate LiteratureBookTemplate
        {
            set;
            get;
        }

        public string PropertyToEvaluate
        {
            set;
            get;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Book book = (Book)item;
            Type type = book.GetType();
            PropertyInfo property = type.GetProperty(PropertyToEvaluate);
            Object obj = property.GetValue(book, null);
            Book.BookCategory cat = (Book.BookCategory)Convert.ToInt32(obj);
            if (cat == Book.BookCategory.History)
            {
                return HistoryBookTemplate;
            }
            else if (cat == Book.BookCategory.ScienceTech)
            {
                return TechBookTemplate;
            }
            else if (cat == Book.BookCategory.Literature)
            {
                return LiteratureBookTemplate;
            }
            else
            {
                return TechBookTemplate;
            }
        }
    }
}
