﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Data;

using Xlfdll.Windows.Presentation;

namespace BatchRenamer.Patterns
{
    public class InsertTextPatternViewModel : ViewModelBase
    {
        public InsertTextPatternViewModel(MainViewModel mainViewModel)
        {
            this.MainViewModel = mainViewModel;

            this.Text = String.Empty;

            this.Files = new ObservableCollection<PatternFileInfo>();
        }

        public MainViewModel MainViewModel { get; }

        private String _text;

        public String Text
        {
            get { return _text; }
            set { SetField(ref _text, value); }
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
                    foreach (PatternFileInfo item in this.Files)
                    {
                        Int32 startIndex = (this.Position > item.FileInfo.NewFileName.Length - 1 ? item.FileInfo.NewFileName.Length : this.Position);

                        item.PreviewFileName = item.FileInfo.NewFileName.Insert(startIndex, this.Text);
                    }
                },
                delegate
                {
                    return !String.IsNullOrEmpty(this?.Text.Trim());
                }
            );

        public RelayCommand<Object> ApplyCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    PreviewCommand.Execute(null);

                    foreach (PatternFileInfo item in this.Files)
                    {
                        item.FileInfo.NewFileName = item.PreviewFileName;
                    }

                    this.Close();
                },
                delegate
                {
                    return !String.IsNullOrEmpty(this?.Text.Trim());
                }
            );

        public Int32 Position { get; set; }

        public event Action RequestClose;

        public void Close()
        {
            this.RequestClose?.Invoke();
        }

        public const String Title = "Insert Text";
    }
}