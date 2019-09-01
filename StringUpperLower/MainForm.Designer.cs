namespace StringUpperLower
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.originalLabel = new System.Windows.Forms.Label();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.copyrightLabel = new System.Windows.Forms.Label();
            this.quitButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.originalComboBox = new System.Windows.Forms.ComboBox();
            this.autoCopyCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // originalLabel
            // 
            resources.ApplyResources(this.originalLabel, "originalLabel");
            this.originalLabel.Name = "originalLabel";
            // 
            // resultTextBox
            // 
            resources.ApplyResources(this.resultTextBox, "resultTextBox");
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.ReadOnly = true;
            // 
            // copyrightLabel
            // 
            resources.ApplyResources(this.copyrightLabel, "copyrightLabel");
            this.copyrightLabel.Name = "copyrightLabel";
            // 
            // quitButton
            // 
            resources.ApplyResources(this.quitButton, "quitButton");
            this.quitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.quitButton.Name = "quitButton";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // typeComboBox
            // 
            resources.ApplyResources(this.typeComboBox, "typeComboBox");
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            resources.GetString("typeComboBox.Items"),
            resources.GetString("typeComboBox.Items1"),
            resources.GetString("typeComboBox.Items2"),
            resources.GetString("typeComboBox.Items3")});
            this.typeComboBox.Name = "typeComboBox";
            // 
            // originalComboBox
            // 
            this.originalComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.originalComboBox, "originalComboBox");
            this.originalComboBox.Name = "originalComboBox";
            this.originalComboBox.SelectedIndexChanged += new System.EventHandler(this.originalComboBox_SelectedIndexChanged);
            // 
            // autoCopyCheckBox
            // 
            resources.ApplyResources(this.autoCopyCheckBox, "autoCopyCheckBox");
            this.autoCopyCheckBox.Checked = true;
            this.autoCopyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoCopyCheckBox.Name = "autoCopyCheckBox";
            this.autoCopyCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.quitButton;
            this.Controls.Add(this.autoCopyCheckBox);
            this.Controls.Add(this.originalComboBox);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.copyrightLabel);
            this.Controls.Add(this.resultTextBox);
            this.Controls.Add(this.originalLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label originalLabel;
        private System.Windows.Forms.TextBox resultTextBox;
        private System.Windows.Forms.Label copyrightLabel;
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.ComboBox originalComboBox;
        private System.Windows.Forms.CheckBox autoCopyCheckBox;
    }
}

