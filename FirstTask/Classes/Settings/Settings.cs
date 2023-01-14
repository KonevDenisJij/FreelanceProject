using FirstTask.Classes.Settings;
using System.IO;
using System.Linq;

namespace FirstTask
{
    internal class Settings : AbsSettings
    {
        protected override string FileName => "Settings.txt";
        public SettingsData SettingsData { get; private set; }

        private Settings()
        {
        }

        private static Settings _instance;
        public static Settings GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Settings();
            }
            return _instance;
        }

        public override void WriteSettings(ISettingsData data)
        {
            var compData = data as SettingsData;
            SettingsData = compData;

            Directory.CreateDirectory(PATH);

            using (StreamWriter writetext = new StreamWriter(File.Create(PATH + FileName)))
            {
                writetext.WriteLine("Number of events: " + SettingsData.EventsNumber);
                writetext.WriteLine("Number of repeats: " + SettingsData.RepeatsNumber);
                writetext.WriteLine("Log path: " + SettingsData.LogPath);
            }
        }

        protected override ISettingsData GetDefaultSettings()
        {
            return new SettingsData(30, 2, PATH);
        }

        protected override void ReadData()
        {
            string[] lines = File.ReadAllLines(PATH + FileName);

            var eventsNumber = int.Parse(GetData(lines[0]));
            var repeatsNumber = int.Parse(GetData(lines[1]));
            var logPath = GetData(lines[2]).Trim();

            if (logPath.Last() != '\\')
            {
                logPath += '\\';
            }

            SettingsData = new SettingsData(eventsNumber, repeatsNumber, logPath);
        }
    }
}
