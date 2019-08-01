using System;
using System.Windows;
using SetupSonic.ViewModels;

namespace SetupSonic
{
    /// <summary>
    /// Interaction logic for DownloadWindow.xaml
    /// </summary>
    public partial class DownloadWindow : Window
    {
        public event EventHandler InitializationCompleteEvent;

        public DownloadWindow()
        {
            InitializeComponent();
            ConfigureEvents();
        }

        public void StartDownload()
        {
            GetInstnace().StartDownload();
        }

        public void CancelDownload()
        {
            GetInstnace().CancelDownload();
        }

        private void ConfigureEvents()
        {
            var instance = GetInstnace();

            instance.InitializationCompleteEvent += OnInitializationCompleteEvent;
        }

        public void OnInitializationCompleteEvent(object sender, EventArgs e)
        {
            InitializationCompleteEvent?.Invoke(sender, e);
        }

        private DownloadViewModel GetInstnace()
        {
            return FindResource("DownloadInstance") as DownloadViewModel;
        }
    }
}
