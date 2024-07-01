using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;

using Xlfdll;
using Xlfdll.Windows.Presentation;
using Xlfdll.Windows.Presentation.Dialogs;

namespace BatchRenamer.Patterns
{
    public class FindAndReplacePatternViewModel : ViewModelBase
    {
        public FindAndReplacePatternViewModel(MainViewModel mainViewModel)
        {
            this.MainViewModel = mainViewModel;

            this.FindWhat = String.Empty;
            this.ReplaceWith = String.Empty;

            this.Files = new ObservableCollection<PatternFileInfo>();
        }

        public MainViewModel MainViewModel { get; }

        private String _findWhat;
        private String _replaceWith;
        private Boolean _matchCase;
        private Boolean _useRegex;

        public String FindWhat
        {
            get { return _findWhat; }
            set { SetField(ref _findWhat, value); }
        }
        public String ReplaceWith
        {
            get { return _replaceWith; }
            set { SetField(ref _replaceWith, value); }
        }
        public Boolean MatchCase
        {
            get { return _matchCase; }
            set { SetField(ref _matchCase, value); }
        }
        public Boolean UseRegex
        {
            get { return _useRegex; }
            set { SetField(ref _useRegex, value); }
        }

        public ObservableCollection<PatternFileInfo> Files { get; }

        private CollectionViewSource _collectionViewSource;

        public CollectionViewSource CollectionViewSource
        {
            get
            {
                if (_collectionViewSource == null)
                {
                    _collectionViewSource = DataHelper.GetCollectionViewSource(this.Files);
                }

                return _collectionViewSource;
            }
        }

        public RelayCommand<Object> PreviewCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    try
                    {
                        foreach (PatternFileInfo item in this.Files)
                        {
                            if (!this.UseRegex)
                            {
                                item.PreviewFileName = item.FileInfo.NewFileName.Replace
                                    (this.FindWhat, this.ReplaceWith, !this.MatchCase);
                            }
                            else
                            {
                                item.PreviewFileName = Regex.Replace
                                    (item.FileInfo.NewFileName,
                                    this.FindWhat,
                                    this.ReplaceWith,
                                    !this.MatchCase ? RegexOptions.IgnoreCase : RegexOptions.None);
                            }
                        }

                        this.IsError = false;
                    }
                    catch (ArgumentException argumentEx)
                    {
                        ExceptionMessageBox.Show(FindAndReplacePatternViewModel.Title, "The following error occurred:", argumentEx);

                        this.IsError = true;
                    }
                },
                delegate
                {
                    return !String.IsNullOrEmpty(this?.FindWhat);
                }
            );

        public RelayCommand<Object> ApplyCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    PreviewCommand.Execute(null);

                    if (this.IsError)
                    {
                        return;
                    }

                    foreach (PatternFileInfo item in this.Files)
                    {
                        item.FileInfo.NewFileName = item.PreviewFileName;
                    }

                    this.Close();
                },
                delegate
                {
                    return !String.IsNullOrEmpty(this?.FindWhat);
                }
            );

        public event Action RequestClose;

        public void Close()
        {
            this.RequestClose?.Invoke();
        }

        public Boolean IsError { get; set; }

        public const String Title = "Find and Replace";
    }
}