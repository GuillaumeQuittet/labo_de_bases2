using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet
{
    public partial class AddMovie : Form
    {
        private Main main;
        private Image imageMovie;
        private Movie movie;
        private int saveMethod = 0;
        private int editMode = 0;

        public AddMovie(Main main)
        {
            InitializeComponent();
            InitializeCombobox();
            this.main = main;
            deleteButton.Enabled = false;
            editButton.Enabled = false;
        }

        public AddMovie(Main main, Movie movie)
        {
            InitializeComponent();
            InitializeCombobox();
            setMovieInContent(movie);
            this.main = main;
            this.movie = movie;
            saveMethod = 1;
        }

        private void setMovieInContent(Movie movie)
        {
            comboBoxDays.SelectedItem = movie.getDay();
            comboBoxMonths.SelectedItem = movie.getMonth();
            comboBoxYears.SelectedItem = movie.getYear();
            comboBoxDays.Enabled = false;
            comboBoxMonths.Enabled = false;
            comboBoxYears.Enabled = false;
            textBox1.Text = movie.getTitle();
            textBox2.Text = movie.getDescription();
            textBox3.Text = movie.getAuthor();
            textBox4.Text = movie.getCategory();
            pictureBox1.Image = movie.getImage();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            loadImageButton.Enabled = false;
            saveButton.Enabled = false;
        }

        private void InitializeCombobox()
        {
            // Add days and months to combobox
            for (int i = 1; i < 32; ++i)
            {
                if (i < 13)
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
            for (int i = filmInvention; i <= computerCurrentYear; ++i)
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
            fileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|GIF Image|*.gif|BMP Image|*.bmp|All files|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = new Bitmap(fileDialog.FileName);
                string fileExtension = Path.GetExtension(fileDialog.FileName);
                imageMovie = Image.FromFile(fileDialog.FileName);
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
                int year = (int)comboBoxYears.SelectedItem;
                if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
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
            resetComboboxDays();
        }

        private void resetComboboxDays()
        {
            int nbreDeJour = nbreDays((int)comboBoxMonths.SelectedIndex);
            for (int i = 1; i <= nbreDeJour; ++i)
            {
                comboBoxDays.Items.Add(i);
            }
            comboBoxDays.SelectedIndex = 0;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if(editMode == 0)
                Dispose();
            else
            {
                enableEditMode(0);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (canSave())
            {
                if(saveMethod == 1)
                {
                    File.Delete(@"Ressources/Movies/" + this.movie.getTitle() + this.movie.getDay() + this.movie.getMonth() + this.movie.getYear() + this.movie.getAuthor() + this.movie.getCategory() + ".mvl");
                }
                Directory.CreateDirectory(@"Ressources/Movies");
                FileStream fileStream = null;
                try
                {
                    fileStream = new FileStream(@"Ressources/Movies/" + textBox1.Text + comboBoxDays.Text + comboBoxMonths.Text + comboBoxYears.Text + textBox3.Text + textBox4.Text + ".mvl", FileMode.OpenOrCreate);
                }
                catch (IOException io)
                {
                    MessageBox.Show(io.Message, "Error : NullReferenceException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Movie movie = null;
                try
                {
                    movie = new Movie(textBox1.Text, (int)comboBoxDays.SelectedItem, (int)comboBoxMonths.SelectedItem, (int)comboBoxYears.SelectedItem, textBox3.Text, textBox4.Text, pictureBox1.Image, textBox2.Text);
                }
                catch (NullReferenceException nre)
                {
                    MessageBox.Show(nre.Message, "Error : NullReferenceException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (fileStream != null && movie != null)
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    try
                    {
                        binaryFormatter.Serialize(fileStream, movie);
                        fileStream.Close();
                    }
                    catch (SerializationException se)
                    {
                        MessageBox.Show(se.Message, "Error : SerializationException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                main.initLibrary();
                Dispose();
            }
            else
            {
                MessageBox.Show("Please complete all the textbox and add an image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool canSave()
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                if (!String.IsNullOrEmpty(textBox2.Text))
                {
                    if (!String.IsNullOrEmpty(textBox3.Text))
                    {
                        if (!String.IsNullOrEmpty(textBox4.Text))
                        {
                            if (pictureBox1.Image != null)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            File.Delete(@"Ressources/Movies/" + textBox1.Text + comboBoxDays.Text + comboBoxMonths.Text + comboBoxYears.Text + textBox3.Text + textBox4.Text + ".mvl");
            main.initLibrary();
            Dispose();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            enableEditMode(1);
        }

        private void enableEditMode(int x)
        {
            if(x == 1)
            {
                saveButton.Enabled = true;
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
                comboBoxDays.Enabled = true;
                comboBoxMonths.Enabled = true;
                comboBoxYears.Enabled = true;
                loadImageButton.Enabled = true;
                deleteButton.Enabled = false;
                editButton.Enabled = false;
                editMode = 1;
                cancelButton.Text = "Cancel modification(s)";
            }
            else
            {
                saveButton.Enabled = false;
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                comboBoxDays.Enabled = false;
                comboBoxMonths.Enabled = false;
                comboBoxYears.Enabled = false;
                loadImageButton.Enabled = false;
                deleteButton.Enabled = true;
                editButton.Enabled = true;
                editMode = 0;
                setMovieInContent(movie);
                cancelButton.Text = "Cancel";
            }
        }
    }
}
