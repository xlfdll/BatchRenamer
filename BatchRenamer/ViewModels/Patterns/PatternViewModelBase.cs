using System;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace BatchRenamer.Patterns
{
    public abstract class PatternViewModelBase : ViewModelBase
    {
        public PatternViewModelBase(MainViewModel mainViewModel)
        {
            this.MainViewModel = mainViewModel;
            this.Files = new ObservableCollection<PatternFileInfo>();
        }

        public MainViewModel MainViewModel { get; }

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

        public event Action RequestClose;

        public void Close()
        {
            this.RequestClose?.Invoke();
        }

        public Boolean IsError { get; protected set; }
    }
}