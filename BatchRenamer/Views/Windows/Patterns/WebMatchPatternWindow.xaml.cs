using System.Windows;
using System.Windows.Controls;

namespace BatchRenamer.Patterns
{
    /// <summary>
    /// WebMatchPatternWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class WebMatchPatternWindow : Window
    {
        public WebMatchPatternWindow()
        {
            InitializeComponent();

            WebMatchPatternViewModel viewModel
                = new WebMatchPatternViewModel(App.MainWindow.DataContext as MainViewModel);

            viewModel.RequestClose += delegate { this.Close(); };

            this.DataContext = viewModel;
        }

        private void FilesListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LayoutHelper.AdjustListViewColumns(sender as ListView);
        }
    }
}