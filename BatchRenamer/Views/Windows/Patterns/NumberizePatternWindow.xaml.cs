using System;
using System.Windows;
using System.Windows.Controls;

namespace BatchRenamer.Patterns
{
    /// <summary>
    /// NumberingPatternWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class NumberizePatternWindow : Window
    {
        public NumberizePatternWindow()
        {
            InitializeComponent();

            NumberizePatternViewModel viewModel
                = new NumberizePatternViewModel(App.MainWindow.DataContext as MainViewModel);

            viewModel.RequestClose += delegate { this.Close(); };

            this.DataContext = viewModel;
        }

        private void NumberStartNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // This event may be fired before DataContext is set

            NumberizePatternViewModel viewModel = this.DataContext as NumberizePatternViewModel;

            if (viewModel != null)
            {
                viewModel.NumberStart = Convert.ToInt32(NumberStartNumericUpDown.Value);
            }
        }

        private void FilesListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LayoutHelper.AdjustListViewColumns(sender as ListView);
        }
    }
}