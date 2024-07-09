﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BatchRenamer.Patterns
{
    /// <summary>
    /// WebMatchPatternWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class WebMatchPatternWindow : Window
    {
        private WebMatchPatternWindow()
        {
            InitializeComponent();

            WebMatchPatternViewModel viewModel
                = new WebMatchPatternViewModel(App.MainWindow.DataContext as MainViewModel);

            viewModel.RequestClose += delegate { this.Close(); };

            this.DataContext = viewModel;
        }

        public WebMatchPatternWindow(IEnumerable<BatchFileInfo> files)
            : this()
        {
            WebMatchPatternViewModel viewModel = this.DataContext as WebMatchPatternViewModel;

            foreach (BatchFileInfo item in files)
            {
                viewModel?.Files.Add(new PatternFileInfo(item));
            }
        }

        private void FilesListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LayoutHelper.AdjustListViewColumns(sender as ListView);
        }
    }
}