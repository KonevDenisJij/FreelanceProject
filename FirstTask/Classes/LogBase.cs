using FirstTask.Classes.Settings;
using System.Collections.Generic;
using System.IO;

namespace FirstTask
{
    internal class LogBase
    {
        private List<string> _text;
        private string _fileName;

        public LogBase(string fileName)
        {
            _text = new List<string>();
            _fileName = fileName;
        }

        public void AddLogText(string text)
        {
            _text.Add(text);
        }

        public void CreateLogFile()
        {
            try
            {
                Directory.CreateDirectory(Settings.GetInstance().SettingsData.LogPath);
            }
            catch
            {
                Directory.CreateDirectory(AbsSettings.PATH);
            }

            using (StreamWriter writetext = new StreamWriter(File.Create(Settings.GetInstance().SettingsData.LogPath + _fileName)))
            {
                foreach(string line in _text)
                {
                    writetext.WriteLine(line);
                }
            }
        }
    }
}
