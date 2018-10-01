using System;
using System.Linq;

using Xlfdll.Windows.Presentation;

namespace BatchRenamer.Patterns
{
    public static class InsertTextPatternCommands
    {
        static InsertTextPatternCommands()
        {
            InsertTextPatternCommands.PreviewCommand = new RelayCommand<Object>
            (
                delegate
                {
                    foreach (PatternFileInfo item in InsertTextPatternViewState.Current.Files)
                    {
                        Int32 startIndex = (InsertTextPatternViewState.Current.Position > item.FileInfo.NewFileName.Length - 1 ? item.FileInfo.NewFileName.Length : InsertTextPatternViewState.Current.Position);

                        item.PreviewFileName = item.FileInfo.NewFileName.Insert(startIndex, InsertTextPatternViewState.Current.Text);
                    }
                },
                delegate
                {
                    return !String.IsNullOrEmpty(InsertTextPatternViewState.Current?.Text.Trim());
                }
            );

            InsertTextPatternCommands.ApplyCommand = new RelayCommand<Object>
            (
                delegate
                {
                    PreviewCommand.Execute(null);

                    foreach (PatternFileInfo item in InsertTextPatternViewState.Current.Files)
                    {
                        AppState.Current.Files.First(f => f.OriginalFullPath == item.FileInfo.OriginalFullPath).NewFileName = item.PreviewFileName;
                    }

                    InsertTextPatternViewState.Current.Close();
                },
                delegate
                {
                    return !String.IsNullOrEmpty(InsertTextPatternViewState.Current?.Text.Trim());
                }
            );
        }

        public static RelayCommand<Object> PreviewCommand { get; }
        public static RelayCommand<Object> ApplyCommand { get; }
    }
}