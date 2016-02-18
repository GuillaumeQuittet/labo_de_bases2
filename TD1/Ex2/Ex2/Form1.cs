using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.MinimumSize = new Size(240, 240);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double a = 0, b = 0;
            if(numberVerification(double.TryParse(number1.Text, out a), double.TryParse(number2.Text, out b)))
            {
                double answer = a + b;
                MessageBox.Show("The answer is "+answer.ToString(), "Answer", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("Please enter real number.", "ERROR", MessageBoxButtons.OK);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double a = 0, b = 0;
            if (numberVerification(double.TryParse(number1.Text, out a), double.TryParse(number2.Text, out b)))
            {
                double answer = a - b;
                MessageBox.Show("The answer is " + answer.ToString(), "Answer", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("Please enter real number.", "ERROR", MessageBoxButtons.OK);
        }

        private Boolean numberVerification(Boolean b1, Boolean b2)
        {
            if (!b1)
            {
                number1.Clear();
                return false;
            }
            else if (!b2)
            {
                number2.Clear();
                return false;
            }
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
