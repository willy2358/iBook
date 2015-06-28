using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iBooks.Controls
{
    public partial class PdfReaderControl : UserControl
    {
        public PdfReaderControl(string fileName)
        {
            InitializeComponent();

            this.axAcroPDF1.LoadFile(fileName);
        }
    }
}
