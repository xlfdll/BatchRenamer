using System;
using System.Windows;
using System.Windows.Controls;

namespace BatchRenamer.Patterns
{
    /// <summary>
    /// TrimTextPatternWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TrimTextPatternWindow : Window
    {
        public TrimTextPatternWindow()
        {
            InitializeComponent();

            TrimTextPatternViewModel viewModel
                = new TrimTextPatternViewModel(App.MainWindow.DataContext as MainViewModel);

            viewModel.RequestClose += delegate { this.Close(); };

            this.DataContext = viewModel;
        }

        private void PositionNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // This event may be fired before DataContext is set

            TrimTextPatternViewModel viewModel = this.DataContext as TrimTextPatternViewModel;

            if (viewModel != null)
            {
                viewModel.Position = Convert.ToInt32(PositionNumericUpDown.Value);
            }
        }

        private void CountNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // This event may be fired before DataContext is set

            TrimTextPatternViewModel viewModel = this.DataContext as TrimTextPatternViewModel;

            if (viewModel != null)
            {
                viewModel.Count = Convert.ToInt32(CountNumericUpDown.Value);
            }
        }

        private void TrimAllCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            TrimTextPatternViewModel viewModel = this.DataContext as TrimTextPatternViewModel;

            if (viewModel != null)
            {
                CountNumericUpDown.Enabled = !viewModel.DoesTrimAll;
            }
        }

        private void FilesListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LayoutHelper.AdjustListViewColumns(sender as ListView);
        }
    }
}