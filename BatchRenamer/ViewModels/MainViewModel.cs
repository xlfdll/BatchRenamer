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

            this.FileHashSet = new HashSet<String>();
        }

        public ToolBarViewModel ToolBarViewModel { get; }
        public ContextMenuViewModel ContextMenuViewModel { get; }

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

        private CollectionViewSource _collectionViewSource;

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

        public void AddFiles(IEnumerable<String> fileNames)
        {
            List<String> checkedFileNames = new List<String>();

            // Manually check original file name here to avoid broken item reference issue when removing files later on
            foreach (String fileName in fileNames)
            {
                FileAttributes attributes = File.GetAttributes(fileName);

                if (attributes.HasFlag(FileAttributes.Directory))
                {
                    checkedFileNames.AddRange(Directory.GetFiles(fileName));
                }
                else
                {
                    checkedFileNames.Add(fileName);
                }
            }

            foreach (String fileName in checkedFileNames)
            {
                if (!this.FileHashSet.Contains(fileName))
                {
                    this.FileHashSet.Add(fileName);
                    this.Files.Add(new BatchFileInfo(fileName));
                }
            }
        }

        public void RemoveFiles(IEnumerable<BatchFileInfo> fileInfos)
        {
            this.Files.RemoveRange(fileInfos);

            foreach (BatchFileInfo fileInfo in fileInfos)
            {
                this.FileHashSet.Remove(fileInfo.OriginalFilePath);
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

        private HashSet<String> FileHashSet { get; }
    }
}