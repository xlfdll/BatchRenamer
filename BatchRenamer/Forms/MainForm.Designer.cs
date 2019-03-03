namespace BatchRenamer
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.mainListView = new System.Windows.Forms.ListView();
            this.newFileNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.originalFileNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mainListViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.firstToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.addFileToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.addFolderToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.addToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.removeToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.batchOperationsToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.internetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.renameToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.renameToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.configToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.configToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.lastToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mainToolStripContainer.ContentPanel.SuspendLayout();
            this.mainToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.mainToolStripContainer.SuspendLayout();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.mainListViewContextMenuStrip.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainToolStripContainer
            // 
            this.mainToolStripContainer.BottomToolStripPanelVisible = false;
            // 
            // mainToolStripContainer.ContentPanel
            // 
            this.mainToolStripContainer.ContentPanel.Controls.Add(this.mainSplitContainer);
            resources.ApplyResources(this.mainToolStripContainer.ContentPanel, "mainToolStripContainer.ContentPanel");
            resources.ApplyResources(this.mainToolStripContainer, "mainToolStripContainer");
            this.mainToolStripContainer.LeftToolStripPanelVisible = false;
            this.mainToolStripContainer.Name = "mainToolStripContainer";
            this.mainToolStripContainer.RightToolStripPanelVisible = false;
            // 
            // mainToolStripContainer.TopToolStripPanel
            // 
            this.mainToolStripContainer.TopToolStripPanel.Controls.Add(this.mainToolStrip);
            // 
            // mainSplitContainer
            // 
            resources.ApplyResources(this.mainSplitContainer, "mainSplitContainer");
            this.mainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.mainListView);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.mainSplitContainer.Panel2.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.mainSplitContainer_Panel2_ControlRemoved);
            // 
            // mainListView
            // 
            this.mainListView.AllowDrop = true;
            this.mainListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.newFileNameColumnHeader,
            this.originalFileNameColumnHeader});
            this.mainListView.ContextMenuStrip = this.mainListViewContextMenuStrip;
            resources.ApplyResources(this.mainListView, "mainListView");
            this.mainListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.mainListView.HideSelection = false;
            this.mainListView.LabelEdit = true;
            this.mainListView.Name = "mainListView";
            this.mainListView.UseCompatibleStateImageBehavior = false;
            this.mainListView.View = System.Windows.Forms.View.Details;
            this.mainListView.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.mainListView_BeforeLabelEdit);
            this.mainListView.SelectedIndexChanged += new System.EventHandler(this.mainListView_SelectedIndexChanged);
            this.mainListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.mainListView_DragDrop);
            this.mainListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.mainListView_DragEnter);
            // 
            // newFileNameColumnHeader
            // 
            resources.ApplyResources(this.newFileNameColumnHeader, "newFileNameColumnHeader");
            // 
            // originalFileNameColumnHeader
            // 
            resources.ApplyResources(this.originalFileNameColumnHeader, "originalFileNameColumnHeader");
            // 
            // mainListViewContextMenuStrip
            // 
            this.mainListViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openFolderToolStripMenuItem,
            this.openToolStripSeparator,
            this.undoToolStripMenuItem,
            this.undoToolStripSeparator,
            this.selectAllToolStripMenuItem,
            this.deselectAllToolStripMenuItem,
            this.selectToolStripSeparator,
            this.removeToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.mainListViewContextMenuStrip.Name = "mainListViewContextMenuStrip";
            resources.ApplyResources(this.mainListViewContextMenuStrip, "mainListViewContextMenuStrip");
            this.mainListViewContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.mainListViewContextMenuStrip_Opening);
            this.mainListViewContextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mainListViewContextMenuStrip_ItemClicked);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            resources.ApplyResources(this.openFolderToolStripMenuItem, "openFolderToolStripMenuItem");
            // 
            // openToolStripSeparator
            // 
            this.openToolStripSeparator.Name = "openToolStripSeparator";
            resources.ApplyResources(this.openToolStripSeparator, "openToolStripSeparator");
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = global::BatchRenamer.Properties.Resources.Revert;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            resources.ApplyResources(this.undoToolStripMenuItem, "undoToolStripMenuItem");
            // 
            // undoToolStripSeparator
            // 
            this.undoToolStripSeparator.Name = "undoToolStripSeparator";
            resources.ApplyResources(this.undoToolStripSeparator, "undoToolStripSeparator");
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            resources.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
            // 
            // deselectAllToolStripMenuItem
            // 
            this.deselectAllToolStripMenuItem.Name = "deselectAllToolStripMenuItem";
            resources.ApplyResources(this.deselectAllToolStripMenuItem, "deselectAllToolStripMenuItem");
            // 
            // selectToolStripSeparator
            // 
            this.selectToolStripSeparator.Name = "selectToolStripSeparator";
            resources.ApplyResources(this.selectToolStripSeparator, "selectToolStripSeparator");
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Image = global::BatchRenamer.Properties.Resources.Remove;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            resources.ApplyResources(this.removeToolStripMenuItem, "removeToolStripMenuItem");
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            resources.ApplyResources(this.clearToolStripMenuItem, "clearToolStripMenuItem");
            // 
            // mainToolStrip
            // 
            resources.ApplyResources(this.mainToolStrip, "mainToolStrip");
            this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.firstToolStripSeparator,
            this.addFileToolStripButton,
            this.addFolderToolStripButton,
            this.addToolStripSeparator,
            this.removeToolStripButton,
            this.removeToolStripSeparator,
            this.batchOperationsToolStripDropDownButton,
            this.batchToolStripSeparator,
            this.renameToolStripButton,
            this.renameToolStripSeparator,
            this.configToolStripButton,
            this.configToolStripSeparator,
            this.aboutToolStripButton,
            this.lastToolStripSeparator});
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Stretch = true;
            this.mainToolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mainToolStrip_ItemClicked);
            // 
            // firstToolStripSeparator
            // 
            this.firstToolStripSeparator.Name = "firstToolStripSeparator";
            resources.ApplyResources(this.firstToolStripSeparator, "firstToolStripSeparator");
            // 
            // addFileToolStripButton
            // 
            this.addFileToolStripButton.Image = global::BatchRenamer.Properties.Resources.File;
            resources.ApplyResources(this.addFileToolStripButton, "addFileToolStripButton");
            this.addFileToolStripButton.Name = "addFileToolStripButton";
            // 
            // addFolderToolStripButton
            // 
            this.addFolderToolStripButton.Image = global::BatchRenamer.Properties.Resources.Folder;
            resources.ApplyResources(this.addFolderToolStripButton, "addFolderToolStripButton");
            this.addFolderToolStripButton.Name = "addFolderToolStripButton";
            // 
            // addToolStripSeparator
            // 
            this.addToolStripSeparator.Name = "addToolStripSeparator";
            resources.ApplyResources(this.addToolStripSeparator, "addToolStripSeparator");
            // 
            // removeToolStripButton
            // 
            resources.ApplyResources(this.removeToolStripButton, "removeToolStripButton");
            this.removeToolStripButton.Image = global::BatchRenamer.Properties.Resources.Remove;
            this.removeToolStripButton.Name = "removeToolStripButton";
            // 
            // removeToolStripSeparator
            // 
            this.removeToolStripSeparator.Name = "removeToolStripSeparator";
            resources.ApplyResources(this.removeToolStripSeparator, "removeToolStripSeparator");
            // 
            // batchOperationsToolStripDropDownButton
            // 
            this.batchOperationsToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.internetToolStripMenuItem});
            resources.ApplyResources(this.batchOperationsToolStripDropDownButton, "batchOperationsToolStripDropDownButton");
            this.batchOperationsToolStripDropDownButton.Image = global::BatchRenamer.Properties.Resources.Batch;
            this.batchOperationsToolStripDropDownButton.Name = "batchOperationsToolStripDropDownButton";
            this.batchOperationsToolStripDropDownButton.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.batchOperationsToolStripDropDownButton_DropDownItemClicked);
            // 
            // internetToolStripMenuItem
            // 
            this.internetToolStripMenuItem.Image = global::BatchRenamer.Properties.Resources.Internet;
            resources.ApplyResources(this.internetToolStripMenuItem, "internetToolStripMenuItem");
            this.internetToolStripMenuItem.Name = "internetToolStripMenuItem";
            // 
            // batchToolStripSeparator
            // 
            this.batchToolStripSeparator.Name = "batchToolStripSeparator";
            resources.ApplyResources(this.batchToolStripSeparator, "batchToolStripSeparator");
            // 
            // renameToolStripButton
            // 
            resources.ApplyResources(this.renameToolStripButton, "renameToolStripButton");
            this.renameToolStripButton.Image = global::BatchRenamer.Properties.Resources.Run;
            this.renameToolStripButton.Name = "renameToolStripButton";
            // 
            // renameToolStripSeparator
            // 
            this.renameToolStripSeparator.Name = "renameToolStripSeparator";
            resources.ApplyResources(this.renameToolStripSeparator, "renameToolStripSeparator");
            // 
            // configToolStripButton
            // 
            this.configToolStripButton.Image = global::BatchRenamer.Properties.Resources.Options;
            resources.ApplyResources(this.configToolStripButton, "configToolStripButton");
            this.configToolStripButton.Name = "configToolStripButton";
            // 
            // configToolStripSeparator
            // 
            this.configToolStripSeparator.Name = "configToolStripSeparator";
            resources.ApplyResources(this.configToolStripSeparator, "configToolStripSeparator");
            // 
            // aboutToolStripButton
            // 
            this.aboutToolStripButton.Image = global::BatchRenamer.Properties.Resources.About;
            resources.ApplyResources(this.aboutToolStripButton, "aboutToolStripButton");
            this.aboutToolStripButton.Name = "aboutToolStripButton";
            // 
            // lastToolStripSeparator
            // 
            this.lastToolStripSeparator.Name = "lastToolStripSeparator";
            resources.ApplyResources(this.lastToolStripSeparator, "lastToolStripSeparator");
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainToolStripContainer);
            this.Name = "MainForm";
            this.mainToolStripContainer.ContentPanel.ResumeLayout(false);
            this.mainToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.mainToolStripContainer.TopToolStripPanel.PerformLayout();
            this.mainToolStripContainer.ResumeLayout(false);
            this.mainToolStripContainer.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.ResumeLayout(false);
            this.mainListViewContextMenuStrip.ResumeLayout(false);
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripSeparator firstToolStripSeparator;
        private System.Windows.Forms.ToolStripButton addFileToolStripButton;
        private System.Windows.Forms.ToolStripButton addFolderToolStripButton;
        private System.Windows.Forms.ToolStripSeparator addToolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator removeToolStripSeparator;
        private System.Windows.Forms.ToolStripDropDownButton batchOperationsToolStripDropDownButton;
        private System.Windows.Forms.ToolStripSeparator batchToolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator renameToolStripSeparator;
        private System.Windows.Forms.ToolStripButton configToolStripButton;
        private System.Windows.Forms.ListView mainListView;
        private System.Windows.Forms.ToolStripSeparator configToolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator lastToolStripSeparator;
        private System.Windows.Forms.ToolStripContainer mainToolStripContainer;
        private System.Windows.Forms.ColumnHeader newFileNameColumnHeader;
        private System.Windows.Forms.ColumnHeader originalFileNameColumnHeader;
        private System.Windows.Forms.ToolStripButton removeToolStripButton;
        private System.Windows.Forms.ToolStripButton renameToolStripButton;
        private System.Windows.Forms.ToolStripButton aboutToolStripButton;
        private System.Windows.Forms.ContextMenuStrip mainListViewContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator openToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deselectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator selectToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator undoToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internetToolStripMenuItem;
    }
}

