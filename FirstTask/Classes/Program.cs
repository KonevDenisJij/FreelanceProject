using FirstTask.Classes.Settings;
using System;
using System.Windows.Forms;

namespace FirstTask
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Settings.GetInstance().ReadSettings();
            CompSettings.GetInstance().ReadSettings();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartForm());
        }
    }
}
