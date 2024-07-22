using System;

using Xlfdll.Windows.Presentation;
using Xlfdll.Windows.Presentation.Dialogs;

namespace BatchRenamer.Patterns
{
    public class NumberizePatternViewModel : PatternViewModelBase
    {
        public NumberizePatternViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            this.IsNumber = true;
            this.NumberFormat = "{0:D1}";
            this.NumberStart = 1;
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

        public RelayCommand<Object> PreviewCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    try
                    {
                        for (Int32 i = 0, j = this.NumberStart; i < this.Files.Count; i++, j++)
                        {
                            this.Files[i].PreviewFileName = String.Format(this.NumberFormat, j);
                        }

                        this.IsError = false;
                    }
                    catch (ArgumentException argumentEx)
                    {
                        ExceptionMessageBox.Show(NumberizePatternViewModel.Title, "The following error occurred:", argumentEx);

                        this.IsError = true;
                    }
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
                }
            );

        public Int32 NumberStart { get; set; }

        public const String Title = "Numberize";
    }
}