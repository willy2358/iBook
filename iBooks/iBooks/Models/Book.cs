using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBooks.Models
{
    public class Book
    {
        public enum BookCategory { ScienceTech, History, Literature, Other }

        static public BookCategory ParseBookCategory(string category)
        {
            if (category == BookCategory.History.ToString())
            {
                return BookCategory.History;
            }
            else if (category == BookCategory.ScienceTech.ToString())
            {
                return BookCategory.ScienceTech;
            }
            else if (category == BookCategory.Literature.ToString())
            {
                return BookCategory.Literature;
            }
            else
            {
                return BookCategory.Other;
            }
        }

        public Book()
        {

        }

        public Book(string bookName, BookCategory category, string coverImg, string mediaFile)
        {
            this.BookName = bookName;
            this.CoverImg = coverImg;
            this.Category = category;
            this.MediaFile = mediaFile;
        }

        public string BookName
        {
            set;
            get;
        }

        public string CoverImg
        {
            set;
            get;
        }

        public string MediaFile
        {
            set;
            get;
        }

        public BookCategory Category
        {
            get;
            set;
        }

        public string BookUri
        {
            get;
            set;
        }
    }
}
