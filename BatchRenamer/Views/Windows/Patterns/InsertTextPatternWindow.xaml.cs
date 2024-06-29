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

            InsertTextPatternViewModel viewModel
                = new InsertTextPatternViewModel(App.MainWindow.DataContext as MainViewModel);

            viewModel.RequestClose += delegate { this.Close(); };

            this.DataContext = viewModel;
        }

        public InsertTextPatternWindow(IEnumerable<BatchFileInfo> files)
            : this()
        {
            InsertTextPatternViewModel viewModel = this.DataContext as InsertTextPatternViewModel;

            foreach (BatchFileInfo item in files)
            {
                viewModel?.Files.Add(new PatternFileInfo(item));
            }
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