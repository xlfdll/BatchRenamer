using System.Windows;
using System.Windows.Controls;

namespace BatchRenamer.Patterns
{
    /// <summary>
    /// CaseConversionPatternWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class CaseConversionPatternWindow : Window
    {
        public CaseConversionPatternWindow()
        {
            InitializeComponent();

            CaseConversionPatternViewModel viewModel
                = new CaseConversionPatternViewModel(App.MainWindow.DataContext as MainViewModel);

            viewModel.RequestClose += delegate { this.Close(); };

            this.DataContext = viewModel;
        }

        private void FilesListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LayoutHelper.AdjustListViewColumns(sender as ListView);
        }
    }
}