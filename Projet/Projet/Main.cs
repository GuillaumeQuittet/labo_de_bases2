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
            Movie movie = movieList[list.Items.IndexOf(list.SelectedItems[0])];
            AddMovie addMovie = new AddMovie(this, movie);
            addMovie.Show();
        }
    }
}
