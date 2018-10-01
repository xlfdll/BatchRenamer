using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BatchRenamer.Patterns
{
    /// <summary>
    /// FindAndReplacePatternWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class FindAndReplacePatternWindow : Window
    {
        private FindAndReplacePatternWindow()
        {
            InitializeComponent();

            FindAndReplacePatternViewState viewState = new FindAndReplacePatternViewState();

            viewState.RequestClose += delegate { this.Close(); };

            this.DataContext = viewState;
        }

        public FindAndReplacePatternWindow(IEnumerable<BatchFileInfo> files)
            : this()
        {
            foreach (BatchFileInfo item in files)
            {
                FindAndReplacePatternViewState.Current.Files.Add(new PatternFileInfo(item));
            }
        }

        private void FilesListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LayoutHelper.AdjustListViewColumns(sender as ListView);
        }
    }
}
