using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BatchRenamer.Helpers;

namespace BatchRenamer
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            includeSubfoldersCheckBox.Checked = VariableHelper.IncludeSubfolders;
            includeFolderItemsCheckBox.Checked = VariableHelper.IncludeFolderItems;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            VariableHelper.IncludeSubfolders = includeSubfoldersCheckBox.Checked;
            VariableHelper.IncludeFolderItems = includeFolderItemsCheckBox.Checked;

            this.Close();
        }
    }
}
