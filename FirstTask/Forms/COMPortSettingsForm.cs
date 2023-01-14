using FirstTask.Classes.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FirstTask.Forms
{
    public partial class COMPortSettingsForm : Form
    {
        public COMPortSettingsForm()
        {
            InitializeComponent();
        }

        private void COMPortSettings_Load(object sender, EventArgs e)
        {
            CompSettings.GetInstance().ReadSettings();
            var ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);

            comboBox1.Text = CompSettings.GetInstance().SettingsData.Port;
            comboBox2.Text = CompSettings.GetInstance().SettingsData.Baud.ToString();
            comboBox3.Text = CompSettings.GetInstance().SettingsData.Data.ToString();
            comboBox4.Text = CompSettings.GetInstance().SettingsData.Stop;
            comboBox5.Text = CompSettings.GetInstance().SettingsData.Parity;


            if (ports.Any() && CompSettings.GetInstance().SettingsData.Port == string.Empty)
            {
                comboBox1.Text = ports[0];
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var data = new CompSettingsData(comboBox1.Text, Int32.Parse(comboBox2.Text), Int32.Parse(comboBox3.Text), comboBox4.Text, comboBox5.Text);
            CompSettings.GetInstance().WriteSettings(data);
            this.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
