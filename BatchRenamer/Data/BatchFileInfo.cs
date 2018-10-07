using System;
using System.IO;

using Xlfdll;

namespace BatchRenamer
{
    public class BatchFileInfo : ObservableObject
    {
        public BatchFileInfo(FileInfo fileInfo)
        {
            this.OriginalFileInfo = fileInfo;
            this.OriginalFileName = Path.GetFileNameWithoutExtension(this.OriginalFileInfo.Name);
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

        public String OriginalFullPath => this.OriginalFileInfo.FullName;
        public String NewFullPath => Path.Combine(this.OriginalFileInfo.DirectoryName, this.NewFileName + this.Extension);

        public void CommitRename()
        {
            if (this.IsModified)
            {
                this.OriginalFileInfo.MoveTo(this.NewFullPath);
                this.OnPropertyChanged();
            }

            this.OriginalFileName = this.NewFileName;
        }
    }
}