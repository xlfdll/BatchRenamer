using System;

using Xlfdll.Windows.Presentation;

namespace BatchRenamer.Patterns
{
    public class InsertTextPatternViewModel : PatternViewModelBase
    {
        public InsertTextPatternViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            this.Text = String.Empty;
        }

        private String _text;

        public String Text
        {
            get { return _text; }
            set { SetField(ref _text, value); }
        }

        public RelayCommand<Object> PreviewCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    foreach (PatternFileInfo item in this.Files)
                    {
                        Int32 startIndex = (this.Position > item.FileInfo.NewFileName.Length - 1 ? item.FileInfo.NewFileName.Length : this.Position);

                        item.PreviewFileName = item.FileInfo.NewFileName.Insert(startIndex, this.Text);
                    }
                },
                delegate
                {
                    return !String.IsNullOrEmpty(this?.Text.Trim());
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
                },
                delegate
                {
                    return !String.IsNullOrEmpty(this?.Text.Trim());
                }
            );

        public Int32 Position { get; set; }

        public const String Title = "Insert Text";
    }
}