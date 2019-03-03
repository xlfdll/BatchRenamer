using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using BatchRenamer.Helpers;

namespace BatchRenamer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void mainToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == addFileToolStripButton)
            {
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Title = "Select files";
                    dlg.Filter = "All Files (*.*)|*.*";
                    dlg.Multiselect = true;

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        FormHelper.AddFilesToListView(mainListView, dlg.FileNames);

                        batchOperationsToolStripDropDownButton.Enabled = (mainListView.Items.Count > 0);
                        renameToolStripButton.Enabled = (mainListView.Items.Count > 0);
                    }
                }
            }
            else if (e.ClickedItem == addFolderToolStripButton)
            {
                using (FolderBrowserDialog dlg = new FolderBrowserDialog())
                {
                    dlg.Description = "Select a folder to search files";
                    dlg.ShowNewFolderButton = false;

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        FormHelper.AddFilesToListView(mainListView, FileHelper.GetFilesFromFolder(dlg.SelectedPath));

                        batchOperationsToolStripDropDownButton.Enabled = (mainListView.Items.Count > 0);
                        renameToolStripButton.Enabled = (mainListView.Items.Count > 0);
                    }
                }
            }
            else if (e.ClickedItem == removeToolStripButton)
            {
                while (mainListView.SelectedItems.Count > 0)
                {
                    ListViewItem item = mainListView.SelectedItems[mainListView.SelectedItems.Count - 1];

                    // Once removed from group's item collection, the Group property will be set to null

                    if (item.Group.Items.Count == 1)
                    {
                        mainListView.Groups.Remove(item.Group);
                    }

                    item.Group.Items.Remove(item);

                    mainListView.Items.Remove(item);
                }

                batchOperationsToolStripDropDownButton.Enabled = (mainListView.Items.Count > 0);
                renameToolStripButton.Enabled = (mainListView.Items.Count > 0);
            }
            else if (e.ClickedItem == renameToolStripButton)
            {
                DialogResult result = MessageBox.Show("Are you sure to rename all the file(s)?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem item in mainListView.Items)
                    {
                        String folder = item.Group.Header;
                        String oldFileName = item.SubItems[1].Text;
                        String newFileName = item.SubItems[0].Text;

                        File.Move(Path.Combine(folder, oldFileName), Path.Combine(folder, newFileName));
                    }

                    MessageBox.Show("All operations successfully completed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (e.ClickedItem == configToolStripButton)
            {
                using (ConfigForm configForm = new ConfigForm())
                {
                    configForm.ShowDialog(this);
                }
            }
            else if (e.ClickedItem == aboutToolStripButton)
            {
                using (AboutBox aboutBox = new AboutBox())
                {
                    aboutBox.ShowDialog(this);
                }
            }
        }

        private void batchOperationsToolStripDropDownButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == internetToolStripMenuItem)
            {
                if (mainSplitContainer.Panel2.Controls.Count > 0)
                {
                    mainSplitContainer.Panel2.Controls.RemoveAt(0);
                }

                InternetRegexBatchPanel internetRegexBatchPanel = new InternetRegexBatchPanel(mainListView.Items);

                mainSplitContainer.SplitterDistance = mainSplitContainer.Height - internetRegexBatchPanel.Height - 10;

                mainSplitContainer.Panel2.Controls.Add(internetRegexBatchPanel);
                mainSplitContainer.Panel2.Controls[0].Dock = DockStyle.Fill;
            }
        }

        private void mainListView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void mainListView_DragDrop(object sender, DragEventArgs e)
        {
            String[] fileNames = e.Data.GetData(DataFormats.FileDrop) as String[];

            if (fileNames != null && fileNames.Length > 0)
            {
                foreach (String fileName in fileNames)
                {
                    if ((File.GetAttributes(fileName) & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        FormHelper.AddFilesToListView(mainListView, FileHelper.GetFilesFromFolder(fileName));
                    }
                    else
                    {
                        FormHelper.AddFilesToListView(mainListView, new String[] { fileName });
                    }
                }

                batchOperationsToolStripDropDownButton.Enabled = (mainListView.Items.Count > 0);
                renameToolStripButton.Enabled = (mainListView.Items.Count > 0);
            }
        }

        private void mainListView_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            FormHelper.SelectTextForListViewLabelEdit(mainListView, 0, Path.GetFileNameWithoutExtension(mainListView.Items[e.Item].Text).Length);
        }

        private void mainListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            removeToolStripButton.Enabled = (mainListView.SelectedItems.Count > 0);
        }

        private void mainListViewContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            openToolStripMenuItem.Enabled = (mainListView.SelectedItems.Count > 0);
            openFolderToolStripMenuItem.Enabled = (mainListView.SelectedItems.Count > 0);

            undoToolStripMenuItem.Enabled = (mainListView.SelectedItems.Count > 0);

            selectAllToolStripMenuItem.Enabled = (mainListView.SelectedItems.Count != mainListView.Items.Count);
            deselectAllToolStripMenuItem.Enabled = (mainListView.Items.Count > 0) && (mainListView.SelectedItems.Count == mainListView.Items.Count);

            removeToolStripMenuItem.Enabled = (mainListView.SelectedItems.Count > 0);
            clearToolStripMenuItem.Enabled = (mainListView.Items.Count > 0);
        }

        private void mainListViewContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            mainListViewContextMenuStrip.Hide();

            if (e.ClickedItem == openToolStripMenuItem)
            {
                // Caution: http://msdn.microsoft.com/en-us/library/53ezey2s(v=vs.80).aspx
                // When explorer.exe being called, return value will be null (instead of a new Process object)

                foreach (ListViewItem item in mainListView.SelectedItems)
                {
                    Process process = Process.Start(Path.Combine(item.Group.Header, item.SubItems[1].Text));

                    if (process != null)
                    {
                        process.Dispose();
                    }
                }
            }
            else if (e.ClickedItem == openFolderToolStripMenuItem)
            {
                foreach (ListViewItem item in mainListView.SelectedItems)
                {
                    Process.Start(item.Group.Header);
                }
            }
            else if (e.ClickedItem == undoToolStripMenuItem)
            {
                foreach (ListViewItem item in mainListView.SelectedItems)
                {
                    item.SubItems[0].Text = item.SubItems[1].Text;
                }
            }
            else if (e.ClickedItem == selectAllToolStripMenuItem)
            {
                foreach (ListViewItem item in mainListView.Items)
                {
                    item.Selected = true;
                }
            }
            else if (e.ClickedItem == deselectAllToolStripMenuItem)
            {
                foreach (ListViewItem item in mainListView.Items)
                {
                    item.Selected = false;
                }
            }
            else if (e.ClickedItem == removeToolStripMenuItem)
            {
                removeToolStripButton.PerformClick();
            }
            else if (e.ClickedItem == clearToolStripMenuItem)
            {
                mainListView.Items.Clear();
                mainListView.Groups.Clear();

                removeToolStripButton.Enabled = false;
                batchOperationsToolStripDropDownButton.Enabled = (mainListView.Items.Count > 0);
                renameToolStripButton.Enabled = false;
            }
        }

        private void mainSplitContainer_Panel2_ControlRemoved(object sender, ControlEventArgs e)
        {
            e.Control.Dispose();
        }
    }
}
