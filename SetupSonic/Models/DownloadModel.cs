using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using SetupSonic.ViewModels;

namespace SetupSonic.Models
{
    public class DownloadModel
    {
        private WebClient client;
        private readonly DownloadViewModel _vm;

        public event EventHandler InitializationCompleteEvent;

        public DownloadModel(DownloadViewModel vm)
        {
            _vm = vm;
        }
        
        public void StartDownload()
        {
            client = new WebClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            client.DownloadProgressChanged += OnDownloadProgressChanged;
            client.DownloadFileCompleted += OnDownloadFileCompleted;

            client.DownloadFileAsync(new Uri(Constants.DownloadUrl), $"{Constants.BotDirectory}.zip");
        }

        public void CancelDownload()
        {
            client.CancelAsync();
        }

        private void OnDownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Utility.OnCancelOrCloseDownload();
                return;
            }

            _vm.ProgressText = "Extracting sonic..";

            Utility.ExtractBot();
            OnInitializationComplete(EventArgs.Empty);  
        }

        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Dispatcher.CurrentDispatcher.Invoke(delegate
            {
                _vm.DownloadPercent = e.ProgressPercentage;
            });
        }

        public void OnInitializationComplete(EventArgs e)
        {
            InitializationCompleteEvent?.Invoke(this, e);
        }
    }
}
