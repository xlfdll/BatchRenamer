using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BatchRenamer.Patterns
{
    /// <summary>
    /// InsertTextPatternWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class InsertTextPatternWindow : Window
    {
        private InsertTextPatternWindow()
        {
            InitializeComponent();

            InsertTextPatternViewState viewState = new InsertTextPatternViewState();

            viewState.RequestClose += delegate { this.Close(); };

            this.DataContext = viewState;
        }

        public InsertTextPatternWindow(IEnumerable<BatchFileInfo> files)
            : this()
        {
            foreach (BatchFileInfo item in files)
            {
                InsertTextPatternViewState.Current.Files.Add(new PatternFileInfo(item));
            }
        }

        private void PositionNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // This event may be fired before DataContext is set

            if (InsertTextPatternViewState.Current != null)
            {
                InsertTextPatternViewState.Current.Position = Convert.ToInt32(PositionNumericUpDown.Value);
            }
        }

        private void FilesListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LayoutHelper.AdjustListViewColumns(sender as ListView);
        }
    }
}
