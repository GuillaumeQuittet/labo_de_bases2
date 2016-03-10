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
    }
}
