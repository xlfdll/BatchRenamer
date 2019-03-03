using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BatchRenamer
{
    public partial class InternetRegexBatchPanel : UserControl
    {
        public InternetRegexBatchPanel()
        {
            InitializeComponent();
        }

        public InternetRegexBatchPanel(ListView.ListViewItemCollection listViewItemCollection)
            : this()
        {
            _listViewItemCollection = listViewItemCollection;
        }

        private void batchButton_Click(object sender, EventArgs e)
        {

        }

        private void loadRegexButton_Click(object sender, EventArgs e)
        {

        }

        private void saveRegexButton_Click(object sender, EventArgs e)
        {

        }

        private ListView.ListViewItemCollection _listViewItemCollection;

        public ListView.ListViewItemCollection ListViewItemCollection
        {
            get { return _listViewItemCollection; }
        }
    }
}
