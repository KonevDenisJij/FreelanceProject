namespace FirstTask.Classes.Settings
{
    internal abstract class AbsSettings
    {
        public const string PATH = @"C:\Users\Public\AppData\";

        protected abstract string FileName { get; }

        public void ReadSettings()
        {
            try
            {
                ReadData();
            }
            catch
            {
                WriteSettings(GetDefaultSettings());
            }
        }

        protected abstract ISettingsData GetDefaultSettings();

        public abstract void WriteSettings(ISettingsData data);

        protected abstract void ReadData();

        protected string GetData(string line)
        {
            return line.Substring(line.IndexOf(':') + 2);
        }
    }
}
