using System;
using System.Windows;
using System.Windows.Controls;

namespace BatchRenamer.Patterns
{
    /// <summary>
    /// InsertTextPatternWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class InsertTextPatternWindow : Window
    {
        public InsertTextPatternWindow()
        {
            InitializeComponent();

            InsertTextPatternViewModel viewModel
                = new InsertTextPatternViewModel(App.MainWindow.DataContext as MainViewModel);

            viewModel.RequestClose += delegate { this.Close(); };

            this.DataContext = viewModel;
        }

        private void PositionNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // This event may be fired before DataContext is set

            InsertTextPatternViewModel viewModel = this.DataContext as InsertTextPatternViewModel;

            if (viewModel != null)
            {
                viewModel.Position = Convert.ToInt32(PositionNumericUpDown.Value);
            }
        }

        private void FilesListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LayoutHelper.AdjustListViewColumns(sender as ListView);
        }
    }
}