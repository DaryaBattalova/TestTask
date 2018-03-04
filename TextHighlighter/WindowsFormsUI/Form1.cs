using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace WindowsFormsUI
{
    public partial class Form1 : Form
    {
        private string _textToFind;
        private PictureBox pb;

        public Form1()
        {
            InitializeComponent();
            pb = new PictureBox();
            PictureBoxControl();
            _textToFind = string.Empty;
        }

        /// <summary>
        /// Highlights a text entered by a user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button1_Click(object sender, EventArgs e)
        {
            _textToFind = textBox1.Text;

            HtmlParcer parcer = new HtmlParcer();
            var coordinates = parcer.GetCoordinates(_textToFind);

            Image image = new Bitmap(Path.Combine(GetPath(), @"..\..\Resources\document.tif"));
            using (var brush = new SolidBrush(Color.FromArgb(135, 255, 255, 0)))
            {
                using (var g = Graphics.FromImage(image))
                {
                    for (int i = 0; i < coordinates.Count; i = i+4)
                    {
                        g.FillRectangle(brush, Rectangle.FromLTRB(coordinates[i], coordinates[i+1], coordinates[i+2], coordinates[i+3]));
                        pb.Image = image;
                    }
               
                }
            }
        }

        /// <summary>
        /// Adds a pictureBox to a flowLayoutPanel.
        /// </summary>

        private void PictureBoxControl()
        {
            pb.Height = 1700;
            pb.Width = 2200;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            pb.Image = Image.FromFile(Path.Combine(GetPath(), @"..\..\Resources\document.tif"));

            flowLayoutPanel1.ScrollControlIntoView(pb);
            flowLayoutPanel1.Controls.Add(pb);
        }

        private string GetPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            path = Path.GetDirectoryName(path);
            return path;
        }
    }
}
