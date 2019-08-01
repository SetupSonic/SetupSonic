using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SetupSonic.Commands;
using SetupSonic.Models;

namespace SetupSonic.ViewModels
{
    public class DownloadViewModel : BaseViewModel
    {
        private DownloadModel _model;
        private int _downloadPercent = 0;
        private string _progressText = "Downloading sonic with kolbot..";

        public event EventHandler InitializationCompleteEvent;
        public DelegateCommand CancelButtonCommand { get; }

        public DownloadViewModel()
        {
            _model = new DownloadModel(this);
            CancelButtonCommand = new DelegateCommand(OnCancel, CanCanel);

            _model.InitializationCompleteEvent += OnInitializationCompleteEvent;
        }

        public void StartDownload()
        {
            _model.StartDownload();
        }

        public void CancelDownload()
        {
            _model.CancelDownload();
        }

        public int DownloadPercent
        {
            get => _downloadPercent;
            set => SetProperty(ref _downloadPercent, value);
        }

        public string ProgressText
        {
            get => _progressText;
            set => SetProperty(ref _progressText, value);
        }

        private void OnCancel(object commandParameter)
        {
            CancelDownload();
        }

        private bool CanCanel(object commandParameter) => true;

        public void OnInitializationCompleteEvent(object sender, EventArgs e)
        {
            InitializationCompleteEvent?.Invoke(sender, e);
        }
    }
}
