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

        private List<DataGridViewRow> rowsList;

        public Form1()
        {
            InitializeComponent();
            initCombobox();
            initDataGridView();
        }

        private void initCombobox()
        {
            comboBox1.Items.Add("or");
            comboBox1.Items.Add("and");
            comboBox1.Items.Add("exor");
            comboBox1.Items.Add("nor");
            comboBox1.Items.Add("nand");
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
    }
}
