// Name: Eduard Povazhnyi
//Class: H48W35-HNDCOMSD-F242A-L
//Project description: App for Powder Coatings Services
//Date: 05/2025
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowderCoatingApp
{
    public partial class ImageGalleryForm : Form
    {
        private List<string> imagePaths;
        private int currentIndex;

        public ImageGalleryForm(List<string> paths, int startIndex = 0)
        {
            InitializeComponent();
            this.imagePaths = paths;
            this.currentIndex = startIndex;
            ShowImage();

            // Клік по картинці — наступна
            pictureBox1.Click += PictureBox1_Click;

            // Escape для закриття
            this.KeyPreview = true;
            this.KeyDown += ImageGalleryForm_KeyDown;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            if (imagePaths == null || imagePaths.Count == 0) return;

            // Spin in a circle
            currentIndex = (currentIndex + 1) % imagePaths.Count;
            ShowImage();
        }

        private void ImageGalleryForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
            // ← попереднє, → наступне
            else if (e.KeyCode == Keys.Left)
            {
                if (imagePaths.Count == 0) return;
                currentIndex = (currentIndex - 1 + imagePaths.Count) % imagePaths.Count;
                ShowImage();
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (imagePaths.Count == 0) return;
                currentIndex = (currentIndex + 1) % imagePaths.Count;
                ShowImage();
            }
        }

        private void ShowImage()
        {
            if (imagePaths.Count == 0)
            {
                pictureBox1.Image = null;
                this.Text = "No images";
                return;
            }

            string relPath = (imagePaths[currentIndex] ?? "").Trim().Replace("\n", "").Replace("\r", "");
            string fullPath = Path.Combine(Application.StartupPath, relPath);

            if (File.Exists(fullPath))
            {
                // Avoid file locking
                using (var bmpTemp = new Bitmap(fullPath))
                {
                    pictureBox1.Image = new Bitmap(bmpTemp);
                }
                this.Text = $"Photo {currentIndex + 1} of {imagePaths.Count}";
            }
            else
            {
                pictureBox1.Image = null;
                this.Text = $"Photo not found ({currentIndex + 1} of {imagePaths.Count})";
            }
        }

        //private void pictureBox1_Click_1(object sender, EventArgs e)
        //{
            //if (imagePaths == null || imagePaths.Count == 0) return;

            // Spin in a circle
            //currentIndex = (currentIndex + 1) % imagePaths.Count;
            //ShowImage();
        //}
    }
}
