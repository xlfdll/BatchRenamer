using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Data;

using Xlfdll.Collections;

namespace BatchRenamer
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            this.ToolBarViewModel = new ToolBarViewModel(this);
            this.ContextMenuViewModel = new ContextMenuViewModel(this);

            this.IsBusy = false;

            this.Files = new ObservableCollection<BatchFileInfo>();
            this.SelectedFiles = new ObservableCollection<BatchFileInfo>();
        }

        public ToolBarViewModel ToolBarViewModel { get; }
        public ContextMenuViewModel ContextMenuViewModel { get; }

        private Boolean _isBusy;
        private String _status;
        private CollectionViewSource _collectionViewSource;

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

        public CollectionViewSource CollectionViewSource
        {
            get
            {
                if (_collectionViewSource == null)
                {
                    _collectionViewSource = DataHelper.GetCollectionViewSource(this.Files, "Directory");
                }

                return _collectionViewSource;
            }
        }

        public void AddNewFiles(IEnumerable<String> fileNames)
        {
            List<String> files = new List<String>();

            // Manually check original file name here to avoid broken item reference issue when removing files later on
            foreach (String fileName in fileNames)
            {
                FileAttributes attributes = File.GetAttributes(fileName);

                if (attributes.HasFlag(FileAttributes.Directory))
                {
                    files.AddRange(Directory.GetFiles(fileName));
                }
                else
                {
                    files.Add(fileName);
                }
            }

            foreach (String file in files)
            {
                if (this.Files.Where(f => (f.OriginalFilePath == file)).Count() == 0)
                {
                    this.Files.Add(new BatchFileInfo(file));
                }
            }
        }

        public void UpdateSelection()
        {
            this.SelectedFiles.Clear();
            this.SelectedFiles.AddRange(this.Files.Where(f => f.IsSelected));
        }

        public void UpdateStatus(String text)
        {
            this.Status = text;
        }
    }
}