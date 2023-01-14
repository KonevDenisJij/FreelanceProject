using FirstTask.Classes.Settings;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace FirstTask.Forms
{
    public partial class MainForm : Form
    {
        private bool _firstClick = true;
        private bool _spaceClicked = false;
        private DateTime _startTime;
        private System.Windows.Forms.Timer _wordsTimer;
        private int _wordsCounter = 0;
        private int _spaceClicks = 0;
        private LogBase _timeLog;
        private LogBase _serialLog;

        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            label1.Location = new Point((this.Width - label1.Width) / 2 + 5, this.Height / 2 - label1.Height - 10);

            _wordsTimer = new System.Windows.Forms.Timer();

            _timeLog = new LogBase(UserData.LastName + " "
                + UserData.FirstName + " "
                + UserData.MiddleName + " "
                + DateTime.Now.ToString("dd-MM-yy") + "_TimeLog.txt");

            _serialLog = new LogBase(UserData.LastName + " "
                + UserData.FirstName + " "
                + UserData.MiddleName + " "
                + DateTime.Now.ToString("dd-MM-yy") + "_OrderLog.txt");

            string timeLogStart = UserData.LastName + " " +
                UserData.FirstName + " " +
                UserData.MiddleName + " " +
                UserData.BirthDate.Date + " " + "\n" +
                "Начало тестирования " + DateTime.Now.ToString() + "\n" +
                "-------";

            _timeLog.AddLogText(timeLogStart);

            _startTime = DateTime.Now;

            try
            {
                CompSettings.GetInstance().ReadSettings();

                serialPort1.PortName = CompSettings.GetInstance().SettingsData.Port;
                serialPort1.BaudRate = CompSettings.GetInstance().SettingsData.Baud;
                serialPort1.DataBits = CompSettings.GetInstance().SettingsData.Data;
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), CompSettings.GetInstance().SettingsData.Stop);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), CompSettings.GetInstance().SettingsData.Parity);
                serialPort1.WriteTimeout = 3000;

                serialPort1.Open();
            }
            catch
            {
                MessageBox.Show("Не удалось открыть порт");
            }
        }

        private void ShowSquare()
        {
            var myBrush = new Pen(Color.Black,2);
            var formGraphics = this.CreateGraphics();
            formGraphics.DrawRectangle(myBrush, new Rectangle(this.Width/2 - 160, this.Height/2 - 50, 321, 53));
            myBrush.Dispose();
            formGraphics.Dispose();       
        }

        private void HideSquare()
        {
            var formGraphics = this.CreateGraphics();

            formGraphics.Clear(this.BackColor);
            formGraphics.Dispose();
        }

        private void ShowWord(Object myObject, EventArgs myEventArgs)
        {
            _wordsTimer.Stop();

            ShowSquare();
            string newWord = UserData.GetRandomWord();
            label1.Text = newWord;
            _wordsCounter++;

            string timeLogText = (DateTime.Now - _startTime).ToString(@"hh\:mm\:ss\.fff") + " -- " +
                DateTime.Now.ToString() + " -- " +
                "\"вопрос\"" + " -- " +
                "100";
            _timeLog.AddLogText(timeLogText);

            string wordLogText = _wordsCounter + ". " + newWord;
            _serialLog.AddLogText(wordLogText);


            label1.Location = new Point((this.Width - label1.Width) / 2 + 5, this.Height / 2 - label1.Height - 10);
            _firstClick = false;
            _spaceClicked = false;
        }

        private void HideWord()
        {
            HideSquare();

            string timeLogText = (DateTime.Now - _startTime).ToString(@"hh\:mm\:ss\.fff") + " -- " +
                DateTime.Now.ToString() + " -- " +
                "\"ответ\"" + " -- " +
                "190";
            _timeLog.AddLogText(timeLogText);

            label1.Hide();
        }

        private void ShowPoint(Object myObject, EventArgs myEventArgs)
        {
            _wordsTimer.Stop();

            label1.Show();
            label1.Text = "o";
            label1.Location = new Point((this.Width - label1.Width) / 2 + 5, this.Height / 2 - label1.Height - 10);

            _wordsTimer.Tick += new EventHandler(ShowWord);
            _wordsTimer.Tick -= new EventHandler(ShowPoint);
            _wordsTimer.Interval = 350;
            _wordsTimer.Start();
        }

        private void FillWordsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (_spaceClicked)
                {
                    return;
                }

                _spaceClicks++;
                _spaceClicked = true;

                if (serialPort1.IsOpen)
                {
                    try
                    {
                        serialPort1.WriteLine("Space click " + _spaceClicks + "   ");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + " Попыток послать данные вновь больше не будет.", "Ошибка! Не удалось передать инфу по порту");
                        serialPort1.Close();
                    }
                }


                if (_wordsCounter == Settings.GetInstance().SettingsData.EventsNumber * Settings.GetInstance().SettingsData.RepeatsNumber)
                {
                    HideWord();
                    StopApp();
                }

                if (_firstClick)
                {
                    ShowWord(null, null);
                    e.Handled = true;
                    return;
                }

                HideWord();

                _wordsTimer.Tick += new EventHandler(ShowPoint);
                _wordsTimer.Tick -= new EventHandler(ShowWord);
                _wordsTimer.Interval = 500;
                _wordsTimer.Start();

                e.Handled = true;
                return;
            }
        }

        private void StopApp()
        {
            _timeLog.CreateLogFile();
            _serialLog.CreateLogFile();

            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }

            this.Close();
        }
    }
}
