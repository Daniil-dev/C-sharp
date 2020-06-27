using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string extension = ".rtf";
        string filePath = "";
        bool needSave = false;

        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.FileOk += (s , e) =>  WriteToFile(saveFileDialog1.FileName);
            saveFileDialog1.Filter = "Txt files (*.txt)|*.txt|Rich text (*.rtf)|*.rtf|All files (*.*)|*.*";
            selectAllToolStripMenuItem.Click += (s, e) => richTextBox1.SelectAll();
        }

        private void WriteToFile(string fileName)
        {
            if (Path.GetExtension(fileName) == extension)
            {
                richTextBox1.SaveFile(fileName);
            }
            else
            {
                try
                {
                    var output = new StreamWriter(File.OpenWrite(fileName));
                    output.Write(richTextBox1.Text);
                    output.Close();
                }
                catch(Exception){}
            }
            filePath = fileName;
            needSave = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                richTextBox1.Clear();
                if (Path.GetExtension(filePath) == extension)
                {
                    richTextBox1.LoadFile(filePath);
                }
                else
                {
                    var input = File.OpenText(openFileDialog1.FileName);
                    richTextBox1.Text = input.ReadToEnd();
                    input.Close();
                }
                richTextBox1.Enabled = true;
                needSave = false;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (needSave == true)
            {
                
                saveFileDialog1.FileName = filePath;
                saveFileDialog1.ShowDialog();
            }
            else
            {
                WriteToFile(filePath);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var NewFileDialog = new FileName();
            if (NewFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                richTextBox1.Enabled = true;
                filePath = NewFileDialog.fileName;
                needSave = true;
                richTextBox1.Clear();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo)
            {
                richTextBox1.Undo();
            }
        }

        private void redoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanRedo)
            {
                richTextBox1.Redo();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog1.Font;
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog1.Color;
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pDoc = new PrintDocument();
            pDoc.DocumentName = filePath;
            pDoc.PrintPage += PrinerHandler;
            printDialog1.Document = pDoc;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDialog1.Document.Print();
            }
            
        }

        private void PrinerHandler(object sender, PrintPageEventArgs e)
        {
            int count = 0;
            Font printFont = fontDialog1.Font;
            Brush printBrish = new SolidBrush(colorDialog1.Color);
            foreach (var line in richTextBox1.Lines)
            {
                float yPos = e.MarginBounds.Top + (count * printFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(line, printFont, printBrish, e.MarginBounds.Left, yPos, new StringFormat());
                count++;
            }
        }

        private void CursorPosition(object sender, EventArgs e)
        {
            int posx = richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine() + 1;
            int posy = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart) + 1;
            toolStripStatusLabel1.Text = posx.ToString() + " / " + posy.ToString();
        }

        private void CursorPosition(object sender, KeyPressEventArgs e)
        {
            int posx = richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine() + 1;
            int posy = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart) + 1;
            toolStripStatusLabel1.Text = posx.ToString() + " / " + posy.ToString();
        }

        private void CursorPosition(object sender, KeyEventArgs e)
        {
            int posx = richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine() + 1;
            int posy = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart) + 1;
            toolStripStatusLabel1.Text = posx.ToString() + " / " + posy.ToString();
        }
    }
}
