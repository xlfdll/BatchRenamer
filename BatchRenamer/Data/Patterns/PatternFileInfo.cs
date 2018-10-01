using System;

using Xlfdll;

namespace BatchRenamer.Patterns
{
    public class PatternFileInfo : ObservableObject
    {
        public PatternFileInfo(BatchFileInfo fileInfo)
        {
            this.FileInfo = fileInfo;
            this.PreviewFileName = String.Copy(this.FileInfo.NewFileName);
        }

        private String _previewFileName;

        public String PreviewFileName
        {
            get { return _previewFileName; }
            set { SetField(ref _previewFileName, value); }
        }

        public BatchFileInfo FileInfo { get; }
    }
}