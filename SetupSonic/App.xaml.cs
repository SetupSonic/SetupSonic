using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SetupSonic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private DownloadWindow dl_;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (Utility.BotDirectoryExists())
            {
                StartMainWindow();

                return;
            }

            var result = MessageBox.Show("It appears this is your first time running Setup Sonic.\nDo you want to download sonic for d2bot/kolbot?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                StartDownloadWindow();
            }
        }

        private void StartMainWindow()
        {
            // Not changing D2Bot# config for now
            //if (Utility.IsD2botRunning())
            //{
            //    // MessageBox.Show("D2Bot# seems to be running.\nPlease close D2Bot# otherwise some settings may be corrupted.", "Please close D2Bot#", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //}

            var main = new MainWindow();
            main.Show();
        }

        private void StartDownloadWindow()
        {
            dl_ = new DownloadWindow();

            dl_.Show();
            dl_.StartDownload();

            dl_.Closed += DownloadWindow_Closed;
            dl_.InitializationCompleteEvent += OnInitializationCompleteEvent;
        }

        // Probably shouldn't work like this.
        // When the download window finishes successfully we pragmatically call close().
        // This means the logic here (deleting artifacts, closing app) gets triggered even when the action completed successfully.
        // It doesn't seem to cause any bad side affects at the moment, so :shrug:
        private void DownloadWindow_Closed(object sender, EventArgs e)
        {
            dl_.CancelDownload();
        }

        public void OnInitializationCompleteEvent(object sender, EventArgs e)
        {
            Dispatcher.Invoke(delegate
            {
                StartMainWindow();
                dl_.Close();
            });
        }
    }
}
