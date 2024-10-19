using System;

using Xlfdll.Windows.Presentation;

namespace BatchRenamer.Patterns
{
    public class TrimTextPatternViewModel : PatternViewModelBase
    {
        public TrimTextPatternViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            this.DoesTrimAll = true;
        }

        private Boolean _doesTrimAll;

        public Boolean DoesTrimAll
        {
            get => _doesTrimAll;
            set => SetField(ref _doesTrimAll, value);
        }

        public RelayCommand<Object> PreviewCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    foreach (PatternFileInfo item in this.Files)
                    {
                        Int32 startIndex = (this.Position > item.FileInfo.NewFileName.Length - 1 ? item.FileInfo.NewFileName.Length : this.Position);

                        if (this.DoesTrimAll)
                        {
                            item.PreviewFileName = item.FileInfo.NewFileName.Remove(startIndex);
                        }
                        else
                        {
                            Int32 count = (this.Count > item.FileInfo.NewFileName.Length ? item.FileInfo.NewFileName.Length : this.Count);

                            item.PreviewFileName = item.FileInfo.NewFileName.Remove(startIndex, count);
                        }
                    }
                }
            );

        public RelayCommand<Object> ApplyCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    PreviewCommand.Execute(null);

                    foreach (PatternFileInfo item in this.Files)
                    {
                        item.FileInfo.NewFileName = item.PreviewFileName;
                    }

                    this.Close();
                }
            );

        public Int32 Position { get; set; }
        public Int32 Count { get; set; }

        public const String Title = "Trim Text";
    }
}