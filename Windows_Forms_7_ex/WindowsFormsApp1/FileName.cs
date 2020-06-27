using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FileName : Form
    {
        public string fileName = "";

        public FileName()
        {
            InitializeComponent();
        }

        private void FileName_FormClosing(object sender, FormClosingEventArgs e)
        {
            fileName = textBox1.Text;
        }
    }
}
