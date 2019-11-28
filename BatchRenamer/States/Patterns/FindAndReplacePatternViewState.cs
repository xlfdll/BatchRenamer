using System;
using System.Collections.ObjectModel;

using Xlfdll;

namespace BatchRenamer.Patterns
{
    public class FindAndReplacePatternViewState : ObservableObject
    {
        public FindAndReplacePatternViewState()
        {
            this.FindWhat = String.Empty;
            this.ReplaceWith = String.Empty;

            this.Files = new ObservableCollection<PatternFileInfo>();

            FindAndReplacePatternViewState.Current = this;
        }

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

        public event Action RequestClose;

        public void Close()
        {
            this.RequestClose?.Invoke();

            FindAndReplacePatternViewState.Current = null;
        }

        public Boolean IsError { get; set; }

        public static FindAndReplacePatternViewState Current { get; private set; }
        public const String Title = "Find and Replace";
    }
}