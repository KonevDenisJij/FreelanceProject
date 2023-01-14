using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

namespace FirstTask.Classes.Settings
{
    internal class CompSettings : AbsSettings
    {
        protected override string FileName => "ComPortSettings.txt";

        public CompSettingsData SettingsData { get; private set; }

        private CompSettings()
        {
        }

        private static CompSettings _instance;
        public static CompSettings GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CompSettings();
            }
            return _instance;
        }

        public override void WriteSettings(ISettingsData data)
        {
            var compData = data as CompSettingsData;
            SettingsData = compData;

            Directory.CreateDirectory(PATH);

            if (compData.Port == "" || compData.Port == string.Empty)
            {
                var ports = SerialPort.GetPortNames();

                compData.Port = ports.Any() ? ports[0] : string.Empty;
            }

            using (StreamWriter writetext = new StreamWriter(File.Create(PATH + FileName)))
            {
                writetext.WriteLine("COM Port: " + compData.Port);
                writetext.WriteLine("Baud Rate: " + compData.Baud);
                writetext.WriteLine("Data Bits: " + compData.Data);
                writetext.WriteLine("Stop Bits: " + compData.Stop);
                writetext.WriteLine("Parity Bits: " + compData.Parity);
            }
        }

        protected override ISettingsData GetDefaultSettings()
        {

            var ports = SerialPort.GetPortNames();
            if (ports.Any())
            {
                return new CompSettingsData(ports[0], 9000, 8, "One", "None");
            }

            MessageBox.Show("Не найден ни один COM порт", "Ошибка! Не найден порт", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return new CompSettingsData("", 9000, 8, "One", "None");
        }

        protected override void ReadData()
        {
            string[] lines = File.ReadAllLines(PATH + FileName);

            var port = GetData(lines[0]);
            var baud = int.Parse(GetData(lines[1]));
            var data = int.Parse(GetData(lines[2]));
            var stop = GetData(lines[3]);
            var parity = GetData(lines[4]);

            SettingsData = new CompSettingsData(port, baud, data, stop, parity);
        }
    }
}
