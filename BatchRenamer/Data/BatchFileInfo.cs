using System;
using System.IO;

using Xlfdll;

using BatchRenamer.Helpers;

namespace BatchRenamer
{
    public class BatchFileInfo : ObservableObject
    {
        public BatchFileInfo(FileInfo fileInfo)
        {
            this.OriginalFileInfo = fileInfo;
            this.NewFileName = this.OriginalFileName;
        }
        public BatchFileInfo(String fileName)
            : this(new FileInfo(fileName)) { }

        private FileInfo OriginalFileInfo { get; }

        private String _originalFileName;
        private String _newFileName;
        private Boolean _isSelected;

        public String OriginalFileName
        {
            get
            {
                if (String.IsNullOrEmpty(_originalFileName))
                {
                    _originalFileName = Path.GetFileNameWithoutExtension(this.OriginalFileInfo.Name);
                }

                return _originalFileName;
            }
            private set
            {
                SetField(ref _originalFileName, value);

                OnPropertyChanged(nameof(this.IsModified));
            }
        }

        public String NewFileName
        {
            get
            {
                return _newFileName;
            }
            set
            {
                SetField(ref _newFileName, value);

                OnPropertyChanged(nameof(this.IsModified));
            }
        }

        public Boolean IsSelected
        {
            get { return _isSelected; }
            set { SetField(ref _isSelected, value); }
        }

        public Boolean IsModified => (this.NewFileName != this.OriginalFileName);

        public String Extension => this.OriginalFileInfo.Extension;
        public Int64 Size => this.OriginalFileInfo.Length;
        public String Directory => this.OriginalFileInfo.DirectoryName;

        public String OriginalFilePath => this.OriginalFileInfo.FullName;
        public String NewFilePath => Path.Combine(this.OriginalFileInfo.DirectoryName, this.NewFileName + this.Extension);

        public void CommitRename()
        {
            if (this.IsModified)
            {
                this.NewFileName = this.NewFileName.Clean();

                this.OriginalFileInfo.MoveTo(this.NewFilePath);

                this.OnPropertyChanged();
            }

            this.OriginalFileName = this.NewFileName;
        }
    }
}