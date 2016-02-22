using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex1
{
    public partial class Form1 : Form
    {

        private Label label;

        private List<DataGridViewRow> rowsList;

        public Form1()
        {
            InitializeComponent();
            MinimumSize = new Size(500, 350);
            MaximumSize = MinimumSize;
            label = new Label();
            initCombobox();
            initDataGridView();
        }

        private void initCombobox()
        {
            comboBox1.Items.Add("or");
            comboBox1.Items.Add("nor");
            comboBox1.Items.Add("and");
            comboBox1.Items.Add("nand");
            comboBox1.Items.Add("xor");
            comboBox1.Items.Add("xnor");
            comboBox1.Items.Add("na");
            comboBox1.Items.Add("nb");
        }

        private void initDataGridView()
        {
            int nbrRow = 4;
            rowsList = new List<DataGridViewRow>();
            for (int i = 0; i < nbrRow; ++i)
            {
                DataGridViewRow row = new DataGridViewRow();
                rowsList.Add(row);
                dataGridView1.Rows.Add(row);
            }
            dataGridView1.Rows[0].Cells[0].Value = 0;
            dataGridView1.Rows[0].Cells[1].Value = 0;
            dataGridView1.Rows[1].Cells[0].Value = 0;
            dataGridView1.Rows[1].Cells[1].Value = 1;
            dataGridView1.Rows[2].Cells[1].Value = 0;
            dataGridView1.Rows[2].Cells[0].Value = 1;
            dataGridView1.Rows[3].Cells[0].Value = 1;
            dataGridView1.Rows[3].Cells[1].Value = 1;
        }

        private void input1CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            if(checkbox.Checked)
            {
                int i = 0;
                foreach (DataGridViewRow row in rowsList)
                {
                    dataGridView1[0, i].Selected = true;
                    ++i;
                }
            }
            else
            {
                int i = 0;
                foreach (DataGridViewRow row in rowsList)
                {
                    dataGridView1[0, i].Selected = false;
                    ++i;
                }
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            ComboBox combobox = (ComboBox)sender;
            if(combobox.Text.Equals("or"))
            {
                for(int i = 0; i < 4; ++i)
                {
                    dataGridView1.Rows[i].Cells[2].Value = Convert.ToInt32(Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) || Convert.ToBoolean(dataGridView1.Rows[i].Cells[1].Value));
                }
            }
            else if (combobox.Text.Equals("and"))
            {
                for (int i = 0; i < 4; ++i)
                {
                    dataGridView1.Rows[i].Cells[2].Value = Convert.ToInt32(Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) && Convert.ToBoolean(dataGridView1.Rows[i].Cells[1].Value));
                }
            }
            else if (combobox.Text.Equals("xor"))
            {
                for (int i = 0; i < 4; ++i)
                {
                    if (i != 3)
                        dataGridView1.Rows[i].Cells[2].Value = Convert.ToInt32(Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) || Convert.ToBoolean(dataGridView1.Rows[i].Cells[1].Value));
                    else
                        dataGridView1.Rows[i].Cells[2].Value = 0;
                }
            }
            else if (combobox.Text.Equals("nor"))
            {
                for (int i = 0; i < 4; ++i)
                {
                    dataGridView1.Rows[i].Cells[2].Value = Convert.ToInt32(!(Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) || Convert.ToBoolean(dataGridView1.Rows[i].Cells[1].Value)));
                }
            }
            else if (combobox.Text.Equals("nand"))
            {
                for (int i = 0; i < 4; ++i)
                {
                    dataGridView1.Rows[i].Cells[2].Value = Convert.ToInt32(!(Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) && Convert.ToBoolean(dataGridView1.Rows[i].Cells[1].Value)));
                }
            }
            else if (combobox.Text.Equals("xnor"))
            {
                for (int i = 0; i < 4; ++i)
                {
                    if (i != 3)
                        dataGridView1.Rows[i].Cells[2].Value = Convert.ToInt32(!(Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) || Convert.ToBoolean(dataGridView1.Rows[i].Cells[1].Value)));
                    else
                        dataGridView1.Rows[i].Cells[2].Value = 1;
                }
            }
            else if (combobox.Text.Equals("na"))
            {
                for (int i = 0; i < 4; ++i)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.Equals(0))
                        dataGridView1.Rows[i].Cells[2].Value = 1;
                    else
                        dataGridView1.Rows[i].Cells[2].Value = 0;
                }
            }
            else if (combobox.Text.Equals("nb"))
            {
                for (int i = 0; i < 4; ++i)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.Equals(0))
                        dataGridView1.Rows[i].Cells[2].Value = 1;
                    else
                        dataGridView1.Rows[i].Cells[2].Value = 0;
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int pictureWidth = pictureBox1.ClientSize.Width/3;
            int pictureHeight = pictureBox1.ClientSize.Height/3;
            
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Visible = false;
            label.BackColor = Color.Transparent;
            label.Font = new Font("Arial", 14, FontStyle.Regular);
            label.Dock = DockStyle.Fill;
            pictureBox1.Controls.Add(label);

            if (comboBox1.Text.Equals("or"))
            {
                Brush brush = new SolidBrush(Color.Yellow);
                e.Graphics.FillRectangle(brush, new Rectangle(0, 0, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height));
                e.Graphics.DrawString("OR", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureWidth, pictureHeight);
            }
            else if (comboBox1.Text.Equals("and"))
            {
                Brush brush = new SolidBrush(Color.Red);
                e.Graphics.FillRectangle(brush, new Rectangle(0, 0, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height));
                e.Graphics.DrawString("AND", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureWidth, pictureHeight);
            }
            else if (comboBox1.Text.Equals("xor"))
            {
                label.Visible = true;
                pictureBox1.Image = Ex1.Properties.Resources.blue;
                label.Text = "XOR";
            }
            else if (comboBox1.Text.Equals("nor"))
            {
                label.Visible = true;
                label.Text = "NOR";
                pictureBox1.Image = Ex1.Properties.Resources.green;
            }
            else if (comboBox1.Text.Equals("nand"))
            {
                label.Visible = true;
                label.Text = "NAND";
                pictureBox1.Image = Ex1.Properties.Resources.pink;
            }
            else if (comboBox1.Text.Equals("xnor"))
            {
                Brush brush = new SolidBrush(Color.BurlyWood);
                e.Graphics.FillRectangle(brush, new Rectangle(0, 0, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height));
                e.Graphics.DrawString("XNOR", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureWidth, pictureHeight);
            }
            else if (comboBox1.Text.Equals("na"))
            {
                Brush brush = new SolidBrush(Color.BlanchedAlmond);
                e.Graphics.FillRectangle(brush, new Rectangle(0, 0, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height));
                e.Graphics.DrawString("NA", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureWidth, pictureHeight);
            }
            else if (comboBox1.Text.Equals("nb"))
            {
                Brush brush = new SolidBrush(Color.Azure);
                e.Graphics.FillRectangle(brush, new Rectangle(0, 0, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height));
                e.Graphics.DrawString("NB", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureWidth, pictureHeight);
            }
        }
    }
}
