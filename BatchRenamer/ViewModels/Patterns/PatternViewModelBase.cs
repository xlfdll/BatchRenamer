using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace BatchRenamer.Patterns
{
    public abstract class PatternViewModelBase : ViewModelBase
    {
        public PatternViewModelBase(MainViewModel mainViewModel)
        {
            this.MainViewModel = mainViewModel;
            this.Files = new ObservableCollection<PatternFileInfo>();
            this.DoSelectAllFiles = !this.HasSelection;

            this.InitializeFiles();
        }

        public MainViewModel MainViewModel { get; }

        public ObservableCollection<PatternFileInfo> Files { get; }

        private CollectionViewSource _collectionViewSource;

        public CollectionViewSource CollectionViewSource
        {
            get
            {
                if (_collectionViewSource == null)
                {
                    _collectionViewSource = DataHelper.GetCollectionViewSource(this.Files);
                }

                return _collectionViewSource;
            }
        }

        private Boolean _doSelectAllFiles;

        public Boolean DoSelectAllFiles
        {
            get
            {
                return _doSelectAllFiles;
            }
            set
            {
                if (_doSelectAllFiles != value)
                {
                    SetField(ref _doSelectAllFiles, value);

                    this.InitializeFiles();
                }
            }
        }

        protected void InitializeFiles()
        {
            this.Files.Clear();

            IEnumerable<BatchFileInfo> files = this.DoSelectAllFiles ? this.MainViewModel.Files : this.MainViewModel.SelectedFiles;

            foreach (BatchFileInfo item in files)
            {
                this.Files.Add(new PatternFileInfo(item));
            }
        }

        public event Action RequestClose;

        public void Close()
        {
            this.RequestClose?.Invoke();
        }

        public Boolean IsError { get; protected set; }
        public Boolean HasSelection
            => this.MainViewModel.SelectedFiles.Count > 0;
    }
}