using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
                    comboBox1.Items.Add(i);
                    comboBox2.Items.Add(i);
                }
                else
                {
                    comboBox1.Items.Add(i);
                }
            }
            // Add the years to combobox
            int filmInvention = 1895;
            int computerCurrentYear = DateTime.Now.Year;
            for(int i = filmInvention; i <= computerCurrentYear; ++i)
            {
                comboBox3.Items.Add(i);
            }
            // Limit the visible items to 7 items
            int visibleItems = 7;
            comboBox1.DropDownHeight = comboBox1.ItemHeight * visibleItems;
            comboBox2.DropDownHeight = comboBox1.ItemHeight * visibleItems;
            comboBox3.DropDownHeight = comboBox1.ItemHeight * visibleItems;
            // Set the default selected value
            comboBox1.SelectedItem = 1;
            comboBox2.SelectedItem = 1;
            comboBox3.SelectedItem = 2000;
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
    }
}
