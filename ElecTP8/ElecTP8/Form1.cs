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
        public Form1()
        {
            InitializeComponent();
            MinimumSize = new Size(800, 600);
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
            if(device == 3)
            {
                statusLabel.Text = "Carte " + device.ToString() + " connectée.";
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
    }
}
