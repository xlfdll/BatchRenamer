using System;
using System.Collections.ObjectModel;

using Xlfdll;

namespace BatchRenamer.Patterns
{
    public class InsertTextPatternViewState : ObservableObject
    {
        public InsertTextPatternViewState()
        {
            this.Text = String.Empty;

            this.Files = new ObservableCollection<PatternFileInfo>();

            InsertTextPatternViewState.Current = this;
        }

        private String _text;

        public String Text
        {
            get { return _text; }
            set { SetField(ref _text, value); }
        }

        public ObservableCollection<PatternFileInfo> Files { get; }

        public Int32 Position { get; set; }

        public event Action RequestClose;

        public void Close()
        {
            this.RequestClose?.Invoke();

            InsertTextPatternViewState.Current = null;
        }

        public static InsertTextPatternViewState Current { get; private set; }
        public const String Title = "Insert Text";
    }
}