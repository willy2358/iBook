using iBooks.Models;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBooks.Notifications
{
    public class AddBookNotification : Confirmation
    {
        public Book AddedBook { set; get; }

        public AddBookNotification()
        {
            AddedBook = null;
        }
    }
}
