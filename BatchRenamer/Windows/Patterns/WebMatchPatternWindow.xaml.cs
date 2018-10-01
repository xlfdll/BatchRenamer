using System.Collections.Generic;
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

            WebMatchPatternViewState viewState = new WebMatchPatternViewState();

            viewState.RequestClose += delegate { this.Close(); };

            this.DataContext = viewState;
        }

        public WebMatchPatternWindow(IEnumerable<BatchFileInfo> files)
            : this()
        {
            foreach (BatchFileInfo item in files)
            {
                WebMatchPatternViewState.Current.Files.Add(new PatternFileInfo(item));
            }
        }

        private void FilesListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LayoutHelper.AdjustListViewColumns(sender as ListView);
        }
    }
}