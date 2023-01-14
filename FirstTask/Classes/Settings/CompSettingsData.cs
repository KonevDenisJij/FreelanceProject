using System;

namespace FirstTask.Classes.Settings
{
    internal class CompSettingsData : ISettingsData
    {
        public CompSettingsData(string port, int baud, int data, string stop, string parity)
        {
            _port = port;
            _baud = baud;
            _data = data;
            _stop = stop;
            _parity = parity;
        }

        public string Port
        {
            get => _port;
            set
            {
                if (_port == string.Empty || _port == "")
                {
                    _port = value;
                    return;
                }
                throw new Exception("You can't change port if it is not empty");
            }
        }
        private string _port;

        public int Baud => _baud;
        private int _baud;

        public int Data => _data;
        private int _data;

        public string Stop => _stop;
        private string _stop;

        public string Parity => _parity;
        private string _parity;
    }
}
