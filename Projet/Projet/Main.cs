using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Projet
{
    public partial class Main : Form
    {

        List<Movie> movieList = new List<Movie>();

        public Main()
        {
            InitializeComponent();
            MinimumSize = new Size(500, 300);
            initLibrary();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMovie addMovie = new AddMovie(this);
            addMovie.Show();
        }

        public void initLibrary()
        {
            imageList1.Images.Clear();
            listView1.Clear();
            movieList.Clear();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            string[] filesList = Directory.GetFiles(Directory.GetCurrentDirectory()+@"/Ressources/Movies/", "*.mvl");
            int i = 0;
            foreach(string file in filesList)
            {
                try
                {
                    if (file.Length < 100000)
                    {
                        FileStream fileStream = new FileStream(file, FileMode.Open);
                        Movie movie = (Movie)binaryFormatter.Deserialize(fileStream);
                        movieList.Add(movie);
                        fileStream.Close();
                        imageList1.Images.Add(movie.getImage());
                        listView1.View = View.LargeIcon;
                        imageList1.ImageSize = new Size(128, 128);
                        listView1.LargeImageList = imageList1;
                        ListViewItem listViewItem = new ListViewItem(movie.getTitle()+ " — " + movie.getAuthor()+" ("+movie.getYear()+")");
                        listViewItem.ImageIndex = i;
                        listView1.Items.Add(listViewItem);
                        ++i;
                    }
                }
                catch (UnauthorizedAccessException UnAuthFile)
                {
                    Console.WriteLine("UnAuthFile: {0}", UnAuthFile.Message);
                }
                catch (IOException ioe)
                {
                    Console.WriteLine("IOException: {0}", ioe.Message);
                }
            }
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            ListView list = (ListView)sender;
            Movie movie;
            try
            {
                movie = movieList[list.Items.IndexOf(list.SelectedItems[0])];
                AddMovie addMovie = new AddMovie(this, movie);
                addMovie.Show();
            }
            catch(ArgumentOutOfRangeException iobe)
            {
                Console.WriteLine("Error: {0}", iobe.Message);
            } 
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.BackColor = SystemColors.Window;
            menuStrip1.BackColor = SystemColors.Control;
            menuStrip1.ForeColor = SystemColors.ControlText;
            statusStrip1.BackColor = SystemColors.Control;
            tableLayoutPanel1.BackColor = SystemColors.Control;
            listView1.ForeColor = SystemColors.WindowText;
        }

        private void lightBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.BackColor = Color.LightBlue;
            menuStrip1.BackColor = Color.Blue;
            menuStrip1.ForeColor = Color.White;
            statusStrip1.BackColor = Color.Blue;
            tableLayoutPanel1.BackColor = Color.Blue;
            listView1.ForeColor = SystemColors.WindowText;
        }

        private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.BackColor = Color.LightGray;
            menuStrip1.BackColor = Color.Gold;
            menuStrip1.ForeColor = SystemColors.ControlText;
            statusStrip1.BackColor = Color.Gold;
            tableLayoutPanel1.BackColor = Color.Gold;
            listView1.ForeColor = SystemColors.WindowText;
        }

        private void pornHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.BackColor = Color.Black;
            menuStrip1.BackColor = Color.Black;
            menuStrip1.ForeColor = Color.White;
            statusStrip1.BackColor = Color.Gold;
            tableLayoutPanel1.BackColor = Color.Gold;
            listView1.ForeColor = Color.White;
        }

        private void youPornToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.BackColor = Color.HotPink;
            menuStrip1.BackColor = Color.Black;
            menuStrip1.ForeColor = Color.White;
            statusStrip1.BackColor = Color.Black;
            tableLayoutPanel1.BackColor = Color.MediumVioletRed;
            listView1.ForeColor = SystemColors.WindowText;
        }

        private void redtubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.BackColor = Color.Red;
            menuStrip1.BackColor = Color.Black;
            menuStrip1.ForeColor = Color.White;
            statusStrip1.BackColor = Color.Black;
            tableLayoutPanel1.BackColor = Color.Red;
            listView1.ForeColor = SystemColors.WindowText;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to delete these movies ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dr.Equals(DialogResult.Yes))
            {
                // Movie movie = movieList[listView1.Items.IndexOf(listView1.SelectedItems)];
                listView1.SelectedItems.Clear();
                Console.WriteLine(listView1.SelectedItems);
            }
        }
    }
}
