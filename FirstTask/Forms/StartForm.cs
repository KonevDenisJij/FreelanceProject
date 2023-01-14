using FirstTask.Forms;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FirstTask
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();

            wordsCounterLabel.Text = "Введенно слов 0 из " + Settings.GetInstance().SettingsData.EventsNumber;

        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            var lastName = textBox1.Text;
            var firstName = textBox2.Text;
            var middleName = textBox3.Text;
            
            var birthDate = dateTimePicker1.Value;

            if (UserData.SetUserData(lastName, firstName, middleName, birthDate) && UserData.Words.Count == Settings.GetInstance().SettingsData.EventsNumber)
            {
                var nextForm = new MainForm();

                this.Hide();
                nextForm.ShowDialog();
                this.Show();
                Close();

                return;
            }

            MessageBox.Show("Введите данные правильно!");
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {

            var nextForm = new SettingsForm();

            nextForm.ShowDialog();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var word = wordsTextBox.Text;

            var message = UserData.AddWord(word);

            if (message != string.Empty)
            {
                MessageBox.Show(message);
                return;
            }

            UpdateFields();
        }

        protected override void OnActivated(EventArgs e)
        {
            UpdateFields();
        }

        private void UpdateFields()
        {
            wordsCounterLabel.Text = "Введенно слов " + UserData.Words.Count + " из " + Settings.GetInstance().SettingsData.EventsNumber;
            wordsListBox.Items.Clear();
            wordsListBox.Items.AddRange(UserData.Words.ToArray());

            wordsTextBox.Text = string.Empty;
        }

        private void wordsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "[а-яё\b]") 
                || (wordsTextBox.Text.Length > 10 && !System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "[\b]"));
        }
    }
}
