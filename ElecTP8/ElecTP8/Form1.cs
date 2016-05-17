using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElecTP8
{
    public partial class Form1 : Form
    {
        private int speedBlinking;
        private bool onThreading;
        private int ledID, ledState, buttonState;

        public Form1()
        {
            InitializeComponent();
            MinimumSize = new Size(800, 600);
            speedBlinking = 2000;
            double vitesseLabel = (1 / (double)speedBlinking * 2) * 250;
            label2.Text = "Vitesse : " + vitesseLabel + " c/s";
            label3.Text = "Période : " + speedBlinking * 2 + "ms";
            onThreading = false;
            oldTimer.Interval = speedBlinking;
            ledID = 1;
            ledState = 0;
            buttonState = 0;
        }

        [DllImport("k8055d.dll")]
        public static extern int OpenDevice(int CardAddress);

        [DllImport("k8055d.dll")]
        public static extern void CloseDevice();

        [DllImport("k8055d.dll")]
        public static extern int ReadAnalogChannel(int Channel);

        [DllImport("k8055d.dll")]
        public static extern void ReadAllAnalog(ref int Data1, ref int Data2);

        [DllImport("k8055d.dll")]
        public static extern void OutputAnalogChannel(int Channel, int Data);

        [DllImport("k8055d.dll")]
        public static extern void OutputAllAnalog(int Data1, int Data2);

        [DllImport("k8055d.dll")]
        public static extern void ClearAnalogChannel(int Channel);

        [DllImport("k8055d.dll")]
        public static extern void SetAllAnalog();

        [DllImport("k8055d.dll")]
        public static extern void ClearAllAnalog();

        [DllImport("k8055d.dll")]
        public static extern void SetAnalogChannel(int Channel);

        [DllImport("k8055d.dll")]
        public static extern void WriteAllDigital(int Data);

        [DllImport("k8055d.dll")]
        public static extern void ClearDigitalChannel(int Channel);

        [DllImport("k8055d.dll")]
        public static extern void ClearAllDigital();

        [DllImport("k8055d.dll")]
        public static extern void SetDigitalChannel(int Channel);

        [DllImport("k8055d.dll")]
        public static extern void SetAllDigital();

        [DllImport("k8055d.dll")]
        public static extern bool ReadDigitalChannel(int Channel);

        [DllImport("k8055d.dll")]
        public static extern int ReadAllDigital();

        [DllImport("k8055d.dll")]
        public static extern int ReadCounter(int CounterNr);

        [DllImport("k8055d.dll")]
        public static extern void ResetCounter(int CounterNr);

        [DllImport("k8055d.dll")]
        public static extern void SetCounterDebounceTime(int CounterNr, int DebounceTime);

        [DllImport("k8055d.dll")]
        public static extern int Version();

        [DllImport("k8055d.dll")]
        public static extern int SearchDevices();

        [DllImport("k8055d.dll")]
        public static extern int SetCurrentDevice(int lngCardAddress);

        private void connexionButton_Click(object sender, EventArgs e)
        {
            int cardAddress = 3 - (Convert.ToInt32(add0Checkbox.Checked) + 
                                   Convert.ToInt32(add1CheckBox.Checked) * 2);
            int device = OpenDevice(cardAddress);
            if(device >= 0 && device < 4)
            {
                statusLabel.Text = "Carte " + device.ToString() + " connectée.";
                connexionButton.Enabled = false;
                add0Checkbox.Enabled = false;
                add1CheckBox.Enabled = false;
                connexionButton.Text = "Connecté";
                timer.Enabled = true;
            }
            else
                statusLabel.Text = "Carte " + device.ToString() + " non trouvée.";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            int i = ReadAllDigital();
            input1CheckBox.Checked = (i & 1) > 0;
            input2CheckBox.Checked = (i & 2) > 0;
            input3CheckBox.Checked = (i & 4) > 0;
            input4CheckBox.Checked = (i & 8) > 0;
            input5CheckBox.Checked = (i & 16) > 0;
            timer.Enabled = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Enabled = false;
            oldTimer.Enabled = false;
            allOff();
            CloseDevice();
        }

        private void allOnButton_Click(object sender, EventArgs e)
        {
            SetAllDigital();
            output1Checkbox.Checked = true;
            output2Checkbox.Checked = true;
            output3Checkbox.Checked = true;
            output4Checkbox.Checked = true;
            output5Checkbox.Checked = true;
            output6Checkbox.Checked = true;
            output7Checkbox.Checked = true;
            output8Checkbox.Checked = true;
        }

        private void allOffButton_Click(object sender, EventArgs e)
        {
            allOff();
        }

        private void input1CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!onThreading)
            {
                if (input1CheckBox.Checked)
                {
                    allOffButton.Enabled = false;
                    allOnButton.Enabled = false;
                    ledState = 1;
                    oldTimer.Enabled = true;
                    switch (ledID)
                    {
                        case 1:
                            output1Checkbox.Checked = true;
                            SetDigitalChannel(1);
                            break;
                        case 2:
                            output2Checkbox.Checked = true;
                            SetDigitalChannel(2);
                            break;
                        case 3:
                            output3Checkbox.Checked = true;
                            SetDigitalChannel(3);
                            break;
                        case 4:
                            output4Checkbox.Checked = true;
                            SetDigitalChannel(4);
                            break;
                        case 5:
                            output5Checkbox.Checked = true;
                            SetDigitalChannel(5);
                            break;
                        case 6:
                            output6Checkbox.Checked = true;
                            SetDigitalChannel(6);
                            break;
                        case 7:
                            output7Checkbox.Checked = true;
                            SetDigitalChannel(7);
                            break;
                        case 8:
                            output8Checkbox.Checked = true;
                            SetDigitalChannel(8);
                            break;
                        default:
                            break;
                    }
                }
                onThreading = true;
                output1Checkbox.Enabled = false;
                output2Checkbox.Enabled = false;
                output3Checkbox.Enabled = false;
                output4Checkbox.Enabled = false;
                output5Checkbox.Enabled = false;
                output6Checkbox.Enabled = false;
                output7Checkbox.Enabled = false;
                output8Checkbox.Enabled = false;
            }
            if (input1CheckBox.Checked)
                buttonState = 1;
            else
                buttonState = 0;
            pictureBox11.Refresh();
        }

        private void output1Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (output1Checkbox.Checked)
            {
                SetDigitalChannel(1);
                ledState = 1;
            }
            else
            {
                ClearDigitalChannel(1);
                ledState = 0;
            }
            pictureBox1.Refresh();
        }

        private void output2Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (output2Checkbox.Checked)
            {
                SetDigitalChannel(2);
                ledState = 1;
            }
            else
            {
                ClearDigitalChannel(2);
                ledState = 0;
            }
            pictureBox2.Refresh();
        }

        private void output3Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (output3Checkbox.Checked)
            {
                SetDigitalChannel(3);
                ledState = 1;
            }
            else
            {
                ClearDigitalChannel(3);
                ledState = 0;
            }
            pictureBox3.Refresh();
        }

        private void output4Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (output4Checkbox.Checked)
            {
                SetDigitalChannel(4);
                ledState = 1;
            }
            else
            {
                ClearDigitalChannel(4);
                ledState = 0;
            }
            pictureBox4.Refresh();
        }

        private void output5Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (output5Checkbox.Checked)
            {
                SetDigitalChannel(5);
                ledState = 1;
            }
            else
            {
                ClearDigitalChannel(5);
                ledState = 0;
            }
            pictureBox5.Refresh();
        }

        private void output6Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (output6Checkbox.Checked)
            {
                SetDigitalChannel(6);
                ledState = 1;
            }
            else
            {
                ClearDigitalChannel(6);
                ledState = 0;
            }
            pictureBox6.Refresh();
        }

        private void output7Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (output7Checkbox.Checked)
            {
                SetDigitalChannel(7);
                ledState = 1;
            }
            else
            {
                ClearDigitalChannel(7);
                ledState = 0;
            }
            pictureBox7.Refresh();
        }

        private void output8Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (output8Checkbox.Checked)
            {
                SetDigitalChannel(8);
                ledState = 1;
            }
            else
            {
                ClearDigitalChannel(8);
                ledState = 0;
            }
            pictureBox8.Refresh();
        }

        private void allOff()
        {
            ClearAllDigital();
            output1Checkbox.Checked = false;
            output2Checkbox.Checked = false;
            output3Checkbox.Checked = false;
            output4Checkbox.Checked = false;
            output5Checkbox.Checked = false;
            output6Checkbox.Checked = false;
            output7Checkbox.Checked = false;
            output8Checkbox.Checked = false;
        }

        private void input4CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            oldTimer.Enabled = false;
            allOff();
            ledState = 0;
            refreshPictureBox();
            output1Checkbox.Enabled = true;
            output2Checkbox.Enabled = true;
            output3Checkbox.Enabled = true;
            output4Checkbox.Enabled = true;
            output5Checkbox.Enabled = true;
            output6Checkbox.Enabled = true;
            output7Checkbox.Enabled = true;
            output8Checkbox.Enabled = true;
            allOffButton.Enabled = true;
            allOnButton.Enabled = true;
            onThreading = false;
            if (input4CheckBox.Checked)
            {
                buttonState = 1;
            }
            else
                buttonState = 0;
            pictureBox12.Refresh();
            
        }

        private void updateSpeed()
        {
            double vitesseLabel = (1/(double)speedBlinking*2)*250;
            label2.Text = "Vitesse : " + vitesseLabel + " c/s";
            label3.Text = "Période : " + speedBlinking * 2 + "ms";
            oldTimer.Interval = speedBlinking;
        }

        private void input2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (input2CheckBox.Checked)
            {
                if (speedBlinking > 100)
                    speedBlinking -= 100;
                else
                {
                    if (speedBlinking > 50)
                        speedBlinking -= 50;
                }
                updateSpeed();
                buttonState = 1;
            }
            else
                buttonState = 0;
            pictureBox10.Refresh();
        }

        private void input3CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (input3CheckBox.Checked)
            {
                if (speedBlinking >= 100)
                    speedBlinking += 100;
                else
                {
                    if (speedBlinking >= 50)
                        speedBlinking += 50;
                }
                updateSpeed();
                buttonState = 1;
            }
            else
                buttonState = 0;
            pictureBox9.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            drawLedBackgroundColor(e);
            e.Graphics.DrawString("1", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureBox1.ClientSize.Width / 2, pictureBox1.ClientSize.Height/2);
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            drawLedBackgroundColor(e);
            e.Graphics.DrawString("2", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureBox1.ClientSize.Width / 2, pictureBox1.ClientSize.Height / 2);
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            drawLedBackgroundColor(e);
            e.Graphics.DrawString("3", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureBox1.ClientSize.Width / 2, pictureBox1.ClientSize.Height / 2);
        }

        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            drawLedBackgroundColor(e);
            e.Graphics.DrawString("4", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureBox1.ClientSize.Width / 2, pictureBox1.ClientSize.Height / 2);
        }

        private void pictureBox5_Paint(object sender, PaintEventArgs e)
        {
            drawLedBackgroundColor(e);
            e.Graphics.DrawString("5", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureBox1.ClientSize.Width / 2, pictureBox1.ClientSize.Height / 2);
        }

        private void pictureBox6_Paint(object sender, PaintEventArgs e)
        {
            drawLedBackgroundColor(e);
            e.Graphics.DrawString("6", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureBox1.ClientSize.Width / 2, pictureBox1.ClientSize.Height / 2);
        }

        private void pictureBox7_Paint(object sender, PaintEventArgs e)
        {
            drawLedBackgroundColor(e);
            e.Graphics.DrawString("7", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureBox1.ClientSize.Width / 2, pictureBox1.ClientSize.Height / 2);
        }

        private void pictureBox8_Paint(object sender, PaintEventArgs e)
        {
            drawLedBackgroundColor(e);
            e.Graphics.DrawString("8", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureBox1.ClientSize.Width / 2, pictureBox1.ClientSize.Height / 2);
        }

        private void drawLedBackgroundColor(PaintEventArgs e)
        {
            if (ledState % 2 == 1)
            {
                Brush brush = new SolidBrush(Color.Green);
                e.Graphics.FillRectangle(brush, new Rectangle(0, 0, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height));
            }
            else
            {
                Brush brush = new SolidBrush(Color.Gray);
                e.Graphics.FillRectangle(brush, new Rectangle(0, 0, pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height));
            }
        }

        private void drawButtonBackgroundColor(PaintEventArgs e)
        {
            if (buttonState % 2 == 1)
            {
                Brush brush = new SolidBrush(Color.Red);
                e.Graphics.FillRectangle(brush, new Rectangle(0, 0, pictureBox11.ClientSize.Width, pictureBox11.ClientSize.Height));
            }
            else
            {
                Brush brush = new SolidBrush(Color.Gray);
                e.Graphics.FillRectangle(brush, new Rectangle(0, 0, pictureBox11.ClientSize.Width, pictureBox11.ClientSize.Height));
            }
        }

        private void input5CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (input5CheckBox.Checked)
            {
                if (ledID > 0 && ledID < 8)
                {
                    ledID++;
                }
                else
                {
                    ledID = 1;
                }
                buttonState = 1;
            }
            else
                buttonState = 0;
            pictureBox13.Refresh();
        }

        private void oldTimer_Tick(object sender, EventArgs e)
        {
            if(ledState == 0)
            {
                ledState = 1;
                switch(ledID)
                {
                    case 1:
                        output1Checkbox.Checked = true;
                        break;
                    case 2:
                        output2Checkbox.Checked = true;
                        break;
                    case 3:
                        output3Checkbox.Checked = true;
                        break;
                    case 4:
                        output4Checkbox.Checked = true;
                        break;
                    case 5:
                        output5Checkbox.Checked = true;
                        break;
                    case 6:
                        output6Checkbox.Checked = true;
                        break;
                    case 7:
                        output7Checkbox.Checked = true;
                        break;
                    case 8:
                        output8Checkbox.Checked = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                ledState = 0;
                allOff();
            }
        }

        private void pictureBox11_Paint(object sender, PaintEventArgs e)
        {
            drawButtonBackgroundColor(e);
            e.Graphics.DrawString("1", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureBox11.ClientSize.Width / 2, pictureBox11.ClientSize.Height / 2);
        }

        private void pictureBox10_Paint(object sender, PaintEventArgs e)
        {
            drawButtonBackgroundColor(e);
            e.Graphics.DrawString("2", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureBox11.ClientSize.Width / 2, pictureBox11.ClientSize.Height / 2);
        }

        private void pictureBox9_Paint(object sender, PaintEventArgs e)
        {
            drawButtonBackgroundColor(e);
            e.Graphics.DrawString("3", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureBox11.ClientSize.Width / 2, pictureBox11.ClientSize.Height / 2);
        }

        private void pictureBox12_Paint(object sender, PaintEventArgs e)
        {
            drawButtonBackgroundColor(e);
            e.Graphics.DrawString("4", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureBox11.ClientSize.Width / 2, pictureBox11.ClientSize.Height / 2);
        }

        private void pictureBox13_Paint(object sender, PaintEventArgs e)
        {
            drawButtonBackgroundColor(e);
            e.Graphics.DrawString("5", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, pictureBox11.ClientSize.Width / 2, pictureBox11.ClientSize.Height / 2);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            ledState = 0;
            buttonState = 0;
            refreshPictureBox();
            pictureBox9.Refresh();
            pictureBox10.Refresh();
            pictureBox11.Refresh();
            pictureBox12.Refresh();
            pictureBox13.Refresh();
        }

        private void refreshPictureBox()
        {
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            pictureBox3.Refresh();
            pictureBox4.Refresh();
            pictureBox5.Refresh();
            pictureBox6.Refresh();
            pictureBox7.Refresh();
            pictureBox8.Refresh();
        }
    }
}
