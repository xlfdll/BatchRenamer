using System;
using System.Collections.ObjectModel;

using Xlfdll;

namespace BatchRenamer.Patterns
{
    public class NumberizePatternViewState : ObservableObject
    {
        public NumberizePatternViewState()
        {
            this.IsNumber = true;
            this.NumberFormat = "D";
            this.NumberStart = 1;

            this.Files = new ObservableCollection<PatternFileInfo>();

            NumberizePatternViewState.Current = this;
        }

        private Boolean _isNumber;
        private String _numberFormat;

        public Boolean IsNumber
        {
            get { return _isNumber; }
            set { SetField(ref _isNumber, value); }
        }
        public String NumberFormat
        {
            get { return _numberFormat; }
            set { SetField(ref _numberFormat, value); }
        }

        public ObservableCollection<PatternFileInfo> Files { get; }

        public Int32 NumberStart { get; set; }

        public event Action RequestClose;

        public void Close()
        {
            this.RequestClose?.Invoke();

            NumberizePatternViewState.Current = null;
        }

        public Boolean IsError { get; set; }

        public static NumberizePatternViewState Current { get; private set; }
        public static readonly String Title = "Numberize";
    }
}