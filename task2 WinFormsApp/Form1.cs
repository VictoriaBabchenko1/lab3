using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task2_WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Розв'язок початкової умови задачі (без графічного інтерфейсу)

            //string[] fileEntries = Directory.GetFiles("images");

            //Regex regexExtForImage = new Regex("^((.bmp)|(.gif)|(.tiff?)|(.jpe?g)|(.png))$", RegexOptions.IgnoreCase);

            //foreach (string fileName in fileEntries)
            //{
            //    if (regexExtForImage.IsMatch(Path.GetExtension(fileName)))
            //    {
            //        try
            //        {
            //            string newFileName = Path.GetFileNameWithoutExtension(fileName) + "-mirrored.gif";
            //            string outputPath = Path.Combine(@"mirrored images", newFileName);

            //            Image image = Image.FromFile(fileName);
            //            image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            //            image.Save(outputPath, ImageFormat.Gif);
            //        }
            //        catch (Exception)
            //        {
            //            MessageBox.Show($"Файл {fileName} не містить картинки, хоча, судячи з розширення, повинен.");
            //        }
            //    }
            //}

            //Щоб цей код працював необхідно видалити все, що знаходиться нижче

            string[] fileEntries = Directory.GetFiles("images");

            Regex regexExtForImage = new Regex("^((.bmp)|(.gif)|(.tiff?)|(.jpe?g)|(.png))$", RegexOptions.IgnoreCase);

            foreach (string fileName in fileEntries)
            {
                if (regexExtForImage.IsMatch(Path.GetExtension(fileName)))
                {
                    try
                    {
                        comboBox1.Items.Add(Path.GetFileName(fileName));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"Файл {fileName} не містить картинки, хоча, судячи з розширення, повинен.");
                    }
                }
            }

            ClearTheFolder("mirrored images");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedImage = comboBox1.SelectedItem as string;

            if (selectedImage != null)
            {
                string imagePath = Path.Combine("images", selectedImage);
                string newFileName = Path.GetFileNameWithoutExtension(selectedImage) + "-mirrored.gif";
                string outputPath = Path.Combine(@"mirrored images", newFileName);

                pictureBox1.Image = Image.FromFile(imagePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                Image image = Image.FromFile(imagePath);
                image.RotateFlip(RotateFlipType.RotateNoneFlipX);

                image.Save(outputPath, ImageFormat.Gif);

                pictureBox2.Image = image;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ClearTheFolder(string folderName)
        {
            string[] files = Directory.GetFiles(folderName);

            foreach (string file in files)
            {
                File.Delete(file);
            }
        }
    }
}
