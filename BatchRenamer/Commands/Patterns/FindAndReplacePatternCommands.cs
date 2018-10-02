using System;
using System.Linq;
using System.Text.RegularExpressions;

using Xlfdll;
using Xlfdll.Windows.Presentation;
using Xlfdll.Windows.Presentation.Dialogs;

namespace BatchRenamer.Patterns
{
    public static class FindAndReplacePatternCommands
    {
        static FindAndReplacePatternCommands()
        {
            FindAndReplacePatternCommands.PreviewCommand = new RelayCommand<Object>
            (
                delegate
                {
                    try
                    {
                        foreach (PatternFileInfo item in FindAndReplacePatternViewState.Current.Files)
                        {
                            if (!FindAndReplacePatternViewState.Current.UseRegex)
                            {
                                item.PreviewFileName = item.FileInfo.NewFileName.Replace(FindAndReplacePatternViewState.Current.FindWhat,
                                    FindAndReplacePatternViewState.Current.ReplaceWith,
                                    !FindAndReplacePatternViewState.Current.MatchCase);
                            }
                            else
                            {
                                item.PreviewFileName = Regex.Replace(item.FileInfo.NewFileName,
                                    FindAndReplacePatternViewState.Current.FindWhat,
                                    FindAndReplacePatternViewState.Current.ReplaceWith,
                                    !FindAndReplacePatternViewState.Current.MatchCase ? RegexOptions.IgnoreCase : RegexOptions.None);
                            }
                        }

                        FindAndReplacePatternViewState.Current.IsError = false;
                    }
                    catch (ArgumentException argumentEx)
                    {
                        ExceptionMessageBox.Show(FindAndReplacePatternViewState.Title, "The following error occurred:", argumentEx);

                        FindAndReplacePatternViewState.Current.IsError = true;
                    }
                },
                delegate
                {
                    return !String.IsNullOrEmpty(FindAndReplacePatternViewState.Current?.FindWhat);
                }
            );

            FindAndReplacePatternCommands.ApplyCommand = new RelayCommand<Object>
            (
                delegate
                {
                    PreviewCommand.Execute(null);

                    if (FindAndReplacePatternViewState.Current.IsError)
                    {
                        return;
                    }

                    foreach (PatternFileInfo item in FindAndReplacePatternViewState.Current.Files)
                    {
                        AppState.Current.Files.First(f => f.OriginalFullPath == item.FileInfo.OriginalFullPath).NewFileName = item.PreviewFileName;
                    }

                    FindAndReplacePatternViewState.Current.Close();
                },
                delegate
                {
                    return !String.IsNullOrEmpty(FindAndReplacePatternViewState.Current?.FindWhat);
                }
            );
        }

        public static RelayCommand<Object> PreviewCommand { get; }
        public static RelayCommand<Object> ApplyCommand { get; }
    }
}