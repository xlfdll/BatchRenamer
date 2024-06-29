using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

using Xlfdll;
using Xlfdll.Windows.Presentation;
using Xlfdll.Windows.Presentation.Dialogs;

namespace BatchRenamer.Patterns
{
    public class NumberizePatternViewModel : ObservableObject
    {
        public NumberizePatternViewModel(MainViewModel mainViewModel)
        {
            this.MainViewModel = mainViewModel;

            this.IsNumber = true;
            this.NumberFormat = "{0:D1}";
            this.NumberStart = 1;

            this.Files = new ObservableCollection<PatternFileInfo>();
        }

        public MainViewModel MainViewModel { get; }

        private Boolean _isNumber;
        private String _numberFormat;
        private CollectionViewSource _collectionViewSource;

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
                        this.MainViewModel.Files.First(f => f.OriginalFilePath == item.FileInfo.OriginalFilePath).NewFileName = item.PreviewFileName;
                    }

                    this.Close();
                }
            );

        public Int32 NumberStart { get; set; }

        public event Action RequestClose;

        public void Close()
        {
            this.RequestClose?.Invoke();
        }

        public Boolean IsError { get; set; }

        public const String Title = "Numberize";
    }
}