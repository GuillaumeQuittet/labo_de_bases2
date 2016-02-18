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
    public partial class TP2 : Form
    {

        private int isPressVirgKey, minusKey, operation;
        private double a, b;

        public TP2()
        {
            InitializeComponent();
            MinimumSize = new Size(350, 300);
            isPressVirgKey = minusKey = 0;
            a = b = operation = 0;
        }

        private void TP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("1"))
                button1.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (!textBox.Text.Equals("0")) {
                textBox.AppendText(button.Text);
            }
            else
            {
                if (!button.Text.Equals("0"))
                    textBox.Text = button.Text;
            }
        }

        private void buttonVirg_Click(object sender, EventArgs e)
        {
            if (!textBox.Text.Equals("") && isPressVirgKey == 0)
            {
                textBox.AppendText(".");
                isPressVirgKey = 1;
            }
        }

        // Set the number to a positive number or negative number when you press on the button
        private void buttonMinus_Click(object sender, EventArgs e)
        {
            if(!textBox.Text.Equals("") && !textBox.Text.Equals("0"))
            {
                string textTemp = textBox.Text;
                if(minusKey%2 == 0)
                {
                    textBox.Text = "-" + textTemp;
                    ++minusKey;
                }
                else
                {
                    textBox.Text = textTemp.Remove(0, 1);
                    ++minusKey;
                }
                // Empêche de faire les 2 cas d'un coup en appuyant vite
                buttonMinus.Enabled = false;
                System.Threading.Thread.Sleep(50);
                buttonMinus.Enabled = true;
            }
        }

        // Delete the last element of a TextBox
        private void undoButton_Click(object sender, EventArgs e)
        {
            // Si la chaine est vide on ne doit rien supprimer
            if(!textBox.Text.Equals(""))
            {
                // Si on supprime la virgule, il faut la réactiver
                if (textBox.Text[textBox.TextLength - 1].ToString().Equals("."))
                    isPressVirgKey = 0;
                // Supprime le dernier caractère de la chaine
                textBox.Text = textBox.Text.Remove(textBox.TextLength - 1);
            }
        }

        private void sinButton_Click(object sender, EventArgs e)
        {
            double number = 0;
            if(!textBox.Text.Equals(""))
            {
                if(double.TryParse(textBox.Text, out number))
                {
                    number *= Math.PI / 180;
                    number = Math.Sin(number);
                    textBox.Text = Convert.ToString(number);
                }
            }
            else
            {
                MessageBox.Show("Please enter a number.", "Error", MessageBoxButtons.OK);
            }
        }

        private void cosButton_Click(object sender, EventArgs e)
        {
            double number = 0;
            if (!textBox.Text.Equals(""))
            {
                if (double.TryParse(textBox.Text, out number))
                {
                    number *= Math.PI / 180;
                    number = Math.Cos(number);
                    textBox.Text = Convert.ToString(number);
                }
            }
        }

        private void tanButton_Click(object sender, EventArgs e)
        {
            double number = 0;
            if (!textBox.Text.Equals(""))
            {
                if (double.TryParse(textBox.Text, out number))
                {
                    number *= Math.PI / 180;
                    number = Math.Tan(number);
                    textBox.Text = Convert.ToString(number);
                }
            }
        }

        private void equalButton_Click(object sender, EventArgs e)
        {
            if((operation != 0) && double.TryParse(textBox.Text, out b))
            {
                switch(operation)
                {
                    case 1:
                        if(b != 0)
                        {
                            a /= b;
                            textBox.Text = Convert.ToString(a);
                            b = 0;
                        }
                        else
                        {
                            textBox.Text = "∞";
                        }
                        break;
                    case 2:
                        a *= b;
                        textBox.Text = Convert.ToString(a);
                        b = 0;
                        break;
                    case 3:
                        a -= b;
                        textBox.Text = Convert.ToString(a);
                        b = 0;
                        break;
                    case 4:
                        a += b;
                        textBox.Text = Convert.ToString(a);
                        b = 0;
                        break;
                }
            }
        }

        private void buttonIntToBin_Click(object sender, EventArgs e)
        {
            int number;
            if(int.TryParse(textBox.Text, out number))
                textBox.Text = Convert.ToString(number, 2);
        }

        private void buttonBinToInt_Click(object sender, EventArgs e)
        {
            //pass
        }

        private void buttonOperation_Click(object sender, EventArgs e)
        {
            Button buttonOperation = (Button)sender;
            if(!textBox.Text.Equals(""))
            {
                if(!double.TryParse(textBox.Text, out a))
                {
                    MessageBox.Show("L'utilisateur est trop con que pour rentrer un nombre correct.", "Erreur : Connard !", MessageBoxButtons.OK);
                }
                else
                {
                    if (buttonOperation.Text.Equals("/"))
                        operation = 1;
                    else if (buttonOperation.Text.Equals("*"))
                        operation = 2;
                    else if (buttonOperation.Text.Equals("─"))
                        operation = 3;
                    else if (buttonOperation.Text.Equals("+"))
                        operation = 4;
                    isPressVirgKey = 0;
                    textBox.Clear();
                }
            }
        }

        private void inverseButton_Click(object sender, EventArgs e)
        {
            a = 1;
            operation = 1;
            equalButton_Click(sender, e);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            isPressVirgKey = 0;
            textBox.Clear();
        }
    }
}
