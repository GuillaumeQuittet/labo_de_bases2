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
        public Form1()
        {
            InitializeComponent();
            MinimumSize = new Size(375, 375);
            MaximumSize = new Size(375, 375);
            initRCombobox();
        }

        private void initRCombobox()
        {
            // Add elements to the R1 and R2 combobox.
            int i = 1000;
            while(i <= 5000)
            {
                r1Combobox.Items.Add(i);
                r2Combobox.Items.Add(i);
                resistanceCombobox.Items.Add(i);
                i += 200;
            }
            // Add element to the capacity combobox.
            capaciteCombobox.Items.Add("10µF");
            capaciteCombobox.Items.Add("15µF");
            capaciteCombobox.Items.Add("22µF");
            capaciteCombobox.Items.Add("27µF");
            capaciteCombobox.Items.Add("33µF");
            // Set the size and the drop down height.
            r1Combobox.ItemHeight = 13;
            r2Combobox.ItemHeight = 13;
            resistanceCombobox.ItemHeight = 13;
            capaciteCombobox.ItemHeight = 13;
            r1Combobox.DropDownHeight = 106;
            r2Combobox.DropDownHeight = 106;
            resistanceCombobox.DropDownHeight = 106;
            capaciteCombobox.DropDownHeight = 106;
        }

        private void error(int i)
        {
            if (i % 2 == 0)
                taddTextBox.Text.Remove(taddTextBox.Text.Length - 1);
            else
                taddTextBox.Text = null;
            MessageBox.Show("Arrête de jouer au con stp.", "You're mad bro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void diviseurDeTensionButton_Click(object sender, EventArgs e)
        {
            ddtGroupBox.Enabled = true;
            fdcGroupBox.Enabled = false;
            diviseurDeTensionButton.BackColor = Color.Green;
            diviseurDeTensionButton.Enabled = true;
            fdcButton.BackColor = Control.DefaultBackColor;
            fdcButton.UseVisualStyleBackColor = true;
            fdcButton.Enabled = false;
        }

        private void fdcButton_Click(object sender, EventArgs e)
        {
            fdcGroupBox.Enabled = true;
            ddtGroupBox.Enabled = false;
            fdcButton.BackColor = Color.Green;
            fdcButton.Enabled = true;
            diviseurDeTensionButton.BackColor = Control.DefaultBackColor;
            diviseurDeTensionButton.UseVisualStyleBackColor = true;
            diviseurDeTensionButton.Enabled = false;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ddtGroupBox.Enabled = false;
            fdcGroupBox.Enabled = false;
            diviseurDeTensionButton.BackColor = Control.DefaultBackColor;
            diviseurDeTensionButton.UseVisualStyleBackColor = true;
            diviseurDeTensionButton.Enabled = true;
            fdcButton.BackColor = Control.DefaultBackColor;
            fdcButton.UseVisualStyleBackColor = true;
            fdcButton.Enabled = true;
            taddTextBox.Text = null;
            r1Combobox.SelectedItem = null;
            r2Combobox.SelectedItem = null;
            tabdr2TextBox.Text = null;
            resistanceCombobox.SelectedItem = null;
            capaciteCombobox.SelectedItem = null;
            fdcTextBox.Text = null;
        }

        private void cltabdr2Button_Click(object sender, EventArgs e)
        {
            double ue = 0;
            double r1 = 0, r2 = 0;
            if (!(taddTextBox.Text.Equals("") || r2Combobox.Text.Equals("") || r1Combobox.Text.Equals("")))
            {
                if (double.TryParse(taddTextBox.Text, out ue) && double.TryParse(r1Combobox.Text, out r1) && double.TryParse(r2Combobox.Text, out r2))
                    tabdr2TextBox.Text = Convert.ToString(ue * (r1 / (r1 + r2)));
            }
            else if (!double.TryParse(taddTextBox.Text, out r1))
                error(0);
        }

        private void clfdcButton_Click(object sender, EventArgs e)
        {
            double r = 0, c = 0;
            string capaciteText = capaciteCombobox.Text;
            capaciteText = capaciteText.Remove(capaciteText.Length - 2);
            if (double.TryParse(resistanceCombobox.Text, out r) && double.TryParse(capaciteText, out c))
                fdcTextBox.Text = Convert.ToString(1/(2*Math.PI*r*c));
        }

        private void taddTextBox_TextChanged(object sender, EventArgs e)
        {
            double r1 = 0;
            if(!taddTextBox.Text.Equals(""))
                if (!double.TryParse(taddTextBox.Text, out r1))
                {
                    error(1);
                }
                    
                       
        }
    }
}
