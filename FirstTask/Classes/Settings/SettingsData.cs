using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask.Classes.Settings
{
    internal class SettingsData : ISettingsData
    {
        public SettingsData(int eventsNumber, int repeatsNumber, string logPath)
        {
            _eventsNumber = eventsNumber;
            _repeatsNumber = repeatsNumber;
            _logPath = logPath;
        }

        public int EventsNumber => _eventsNumber;
        private int _eventsNumber;

        public int RepeatsNumber => _repeatsNumber;
        private int _repeatsNumber;

        public string LogPath => _logPath;
        private string _logPath;
    }
}
