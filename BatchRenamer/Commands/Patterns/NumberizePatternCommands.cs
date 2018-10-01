using System;
using System.Linq;

using Xlfdll.Windows.Presentation;
using Xlfdll.Windows.Presentation.Dialogs;

namespace BatchRenamer.Patterns
{
    public static class NumberizePatternCommands
    {
        static NumberizePatternCommands()
        {
            NumberizePatternCommands.PreviewCommand = new RelayCommand<Object>
            (
                delegate
                {
                    try
                    {
                        for (Int32 i = 0, j = NumberizePatternViewState.Current.NumberStart; i < NumberizePatternViewState.Current.Files.Count; i++, j++)
                        {
                            NumberizePatternViewState.Current.Files[i].PreviewFileName = j.ToString(NumberizePatternViewState.Current.NumberFormat);
                        }

                        NumberizePatternViewState.Current.IsError = false;
                    }
                    catch (ArgumentException argumentEx)
                    {
                        ExceptionMessageBox.Show(NumberizePatternViewState.Title, "The following error occurred:", argumentEx);

                        NumberizePatternViewState.Current.IsError = true;
                    }
                }
            );

            NumberizePatternCommands.ApplyCommand = new RelayCommand<Object>
            (
                delegate
                {
                    PreviewCommand.Execute(null);

                    if (NumberizePatternViewState.Current.IsError)
                    {
                        return;
                    }

                    foreach (PatternFileInfo item in NumberizePatternViewState.Current.Files)
                    {
                        AppState.Current.Files.First(f => f.OriginalFullPath == item.FileInfo.OriginalFullPath).NewFileName = item.PreviewFileName;
                    }

                    NumberizePatternViewState.Current.Close();
                }
            );
        }

        public static RelayCommand<Object> PreviewCommand { get; }
        public static RelayCommand<Object> ApplyCommand { get; }
    }
}