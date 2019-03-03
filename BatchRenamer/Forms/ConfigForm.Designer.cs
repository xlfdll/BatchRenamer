namespace BatchRenamer
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.generalGroupBox = new System.Windows.Forms.GroupBox();
            this.includeFolderItemsCheckBox = new System.Windows.Forms.CheckBox();
            this.includeSubfoldersCheckBox = new System.Windows.Forms.CheckBox();
            this.generalGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // generalGroupBox
            // 
            resources.ApplyResources(this.generalGroupBox, "generalGroupBox");
            this.generalGroupBox.Controls.Add(this.includeFolderItemsCheckBox);
            this.generalGroupBox.Controls.Add(this.includeSubfoldersCheckBox);
            this.generalGroupBox.Name = "generalGroupBox";
            this.generalGroupBox.TabStop = false;
            // 
            // includeFolderItemsCheckBox
            // 
            resources.ApplyResources(this.includeFolderItemsCheckBox, "includeFolderItemsCheckBox");
            this.includeFolderItemsCheckBox.Name = "includeFolderItemsCheckBox";
            this.includeFolderItemsCheckBox.UseVisualStyleBackColor = true;
            // 
            // includeSubfoldersCheckBox
            // 
            resources.ApplyResources(this.includeSubfoldersCheckBox, "includeSubfoldersCheckBox");
            this.includeSubfoldersCheckBox.Name = "includeSubfoldersCheckBox";
            this.includeSubfoldersCheckBox.UseVisualStyleBackColor = true;
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.generalGroupBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.generalGroupBox.ResumeLayout(false);
            this.generalGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox generalGroupBox;
        private System.Windows.Forms.CheckBox includeSubfoldersCheckBox;
        private System.Windows.Forms.CheckBox includeFolderItemsCheckBox;
    }
}