using FirstTask.Classes.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstTask.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            numericUpDown1.Value = Settings.GetInstance().SettingsData.EventsNumber;
            numericUpDown2.Value = Settings.GetInstance().SettingsData.RepeatsNumber;
            textBox3.Text = Settings.GetInstance().SettingsData.LogPath;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var data = new SettingsData((int)numericUpDown1.Value, (int)numericUpDown2.Value, textBox3.Text);
            Settings.GetInstance().WriteSettings(data);
            this.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void COMPortButton_Click(object sender, EventArgs e)
        {
            var nextForm = new COMPortSettingsForm();

            nextForm.ShowDialog();
        }
    }
}
