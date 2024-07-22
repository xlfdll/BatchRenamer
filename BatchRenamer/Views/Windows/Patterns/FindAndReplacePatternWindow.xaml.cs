using System.Windows;
using System.Windows.Controls;

namespace BatchRenamer.Patterns
{
    /// <summary>
    /// FindAndReplacePatternWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class FindAndReplacePatternWindow : Window
    {
        public FindAndReplacePatternWindow()
        {
            InitializeComponent();

            FindAndReplacePatternViewModel viewModel
                = new FindAndReplacePatternViewModel(App.MainWindow.DataContext as MainViewModel);

            viewModel.RequestClose += delegate { this.Close(); };

            this.DataContext = viewModel;
        }

        private void FilesListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LayoutHelper.AdjustListViewColumns(sender as ListView);
        }
    }
}