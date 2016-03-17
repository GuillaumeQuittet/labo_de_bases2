using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet
{
    public partial class AddMovie : Form
    {
        public AddMovie()
        {
            InitializeComponent();
            InitializeCombobox();
        }

        private void InitializeCombobox()
        {
            // Add days and months to combobox
            for (int i = 1; i < 32; ++i)
            {
                if(i < 13)
                {
                    comboBoxDays.Items.Add(i);
                    comboBoxMonths.Items.Add(i);
                }
                else
                {
                    comboBoxDays.Items.Add(i);
                }
            }
            // Add the years to combobox
            int filmInvention = 1895;
            int computerCurrentYear = DateTime.Now.Year;
            for(int i = filmInvention; i <= computerCurrentYear; ++i)
            {
                comboBoxYears.Items.Add(i);
            }
            // Limit the visible items to 7 items
            int visibleItems = 7;
            comboBoxDays.DropDownHeight = comboBoxDays.ItemHeight * visibleItems;
            comboBoxMonths.DropDownHeight = comboBoxDays.ItemHeight * visibleItems;
            comboBoxYears.DropDownHeight = comboBoxDays.ItemHeight * visibleItems;
            // Set the default selected value
            comboBoxDays.SelectedItem = 1;
            comboBoxMonths.SelectedItem = 1;
            comboBoxYears.SelectedItem = 2000;
        }

        private void loadImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap|*.bmp|Other image format|*.*";
            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = new Bitmap(fileDialog.FileName);
            }
        }

        private int nbreDays(int i)
        {
            if (i == 0 || i == 2 || i == 4 || i == 6 || i == 7 || i == 9 || i == 11)
                return 31;
            else if (i == 3 || i == 5 || i == 8 || i == 10)
                return 30;
            else if (i == 1)
            {
                int year = (int) comboBoxYears.SelectedItem;
                if ((year%4 == 0 && year%100!=0) || (year%400==0))
                    return 29;
                else
                    return 28;
            }
            else
                return -1;
        }

        private void comboBoxClear(object sender, EventArgs e)
        {
            comboBoxDays.Items.Clear();
            comboBoxMonths_SelectedIndexChanged(sender, e);
        }

        private void comboBoxMonths_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nbreDeJour = nbreDays((int) comboBoxMonths.SelectedIndex);
            for (int i = 1; i <= nbreDeJour; ++i)
            {
                comboBoxDays.Items.Add(i);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            FileStream fileStream = new FileStream("movie.mvl", FileMode.Create);
            Movie movie = new Movie(textBox1.Text, (int)comboBoxDays.SelectedItem, (int)comboBoxMonths.SelectedItem, (int)comboBoxYears.SelectedItem, pictureBox1.ImageLocation, textBox2.Text);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, movie);
            fileStream.Close();
        }
    }
}
