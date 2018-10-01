using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BatchRenamer.Patterns
{
    /// <summary>
    /// NumberingPatternWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class NumberizePatternWindow : Window
    {
        private NumberizePatternWindow()
        {
            InitializeComponent();

            NumberizePatternViewState viewState = new NumberizePatternViewState();

            viewState.RequestClose += delegate { this.Close(); };

            this.DataContext = viewState;
        }

        public NumberizePatternWindow(IEnumerable<BatchFileInfo> files)
            : this()
        {
            foreach (BatchFileInfo item in files)
            {
                NumberizePatternViewState.Current.Files.Add(new PatternFileInfo(item));
            }
        }

        private void NumberStartNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // This event may be fired before DataContext is set

            if (NumberizePatternViewState.Current != null)
            {
                NumberizePatternViewState.Current.NumberStart = Convert.ToInt32(NumberStartNumericUpDown.Value);
            }
        }

        private void FilesListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LayoutHelper.AdjustListViewColumns(sender as ListView);
        }
    }
}
