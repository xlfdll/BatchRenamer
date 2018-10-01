using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xlfdll;
using Xlfdll.Collections;

namespace BatchRenamer
{
    public class AppState : ObservableObject
    {
        public AppState()
        {
            this.IsBusy = false;

            this.Files = new ObservableCollection<BatchFileInfo>();
            this.SelectedFiles = new ObservableCollection<BatchFileInfo>();

            AppState.Current = this;
        }

        private Boolean _isBusy;
        private String _status;

        public Boolean IsBusy
        {
            get { return _isBusy; }
            set { this.SetField(ref _isBusy, value); }
        }
        public String Status
        {
            get { return _status; }
            private set { this.SetField(ref _status, value); }
        }

        public ObservableCollection<BatchFileInfo> Files { get; }
        public ObservableCollection<BatchFileInfo> SelectedFiles { get; }

        public Boolean IsModified => this.Files.Any(f => f.NewFileName != f.OriginalFileName);

        public void UpdateSelection()
        {
            this.SelectedFiles.Clear();
            this.SelectedFiles.AddRange(this.Files.Where(f => f.IsSelected));
        }

        public void UpdateStatus(String text)
        {
            this.Status = text;
        }

        public static AppState Current { get; private set; }
    }
}