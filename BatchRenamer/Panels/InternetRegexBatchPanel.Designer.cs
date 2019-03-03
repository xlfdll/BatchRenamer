namespace BatchRenamer
{
    partial class InternetRegexBatchPanel
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InternetRegexBatchPanel));
            this.fileNameRegexLlabel = new System.Windows.Forms.Label();
            this.fileNameRegexTextBox = new System.Windows.Forms.TextBox();
            this.internetRegexGroupBox = new System.Windows.Forms.GroupBox();
            this.internetRegexTextBox = new System.Windows.Forms.TextBox();
            this.internetURLTextBox = new System.Windows.Forms.TextBox();
            this.internetRegexLabel = new System.Windows.Forms.Label();
            this.internetURLLabel = new System.Windows.Forms.Label();
            this.batchButton = new System.Windows.Forms.Button();
            this.saveRegexButton = new System.Windows.Forms.Button();
            this.loadRegexButton = new System.Windows.Forms.Button();
            this.fileNameNewPatternTextBox = new System.Windows.Forms.TextBox();
            this.fileNameNewPatternLabel = new System.Windows.Forms.Label();
            this.fileNameGroupBox = new System.Windows.Forms.GroupBox();
            this.internetRegexGroupBox.SuspendLayout();
            this.fileNameGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileNameRegexLlabel
            // 
            resources.ApplyResources(this.fileNameRegexLlabel, "fileNameRegexLlabel");
            this.fileNameRegexLlabel.Name = "fileNameRegexLlabel";
            // 
            // fileNameRegexTextBox
            // 
            resources.ApplyResources(this.fileNameRegexTextBox, "fileNameRegexTextBox");
            this.fileNameRegexTextBox.Name = "fileNameRegexTextBox";
            // 
            // internetRegexGroupBox
            // 
            resources.ApplyResources(this.internetRegexGroupBox, "internetRegexGroupBox");
            this.internetRegexGroupBox.Controls.Add(this.internetRegexTextBox);
            this.internetRegexGroupBox.Controls.Add(this.internetURLTextBox);
            this.internetRegexGroupBox.Controls.Add(this.internetRegexLabel);
            this.internetRegexGroupBox.Controls.Add(this.internetURLLabel);
            this.internetRegexGroupBox.Name = "internetRegexGroupBox";
            this.internetRegexGroupBox.TabStop = false;
            // 
            // internetRegexTextBox
            // 
            resources.ApplyResources(this.internetRegexTextBox, "internetRegexTextBox");
            this.internetRegexTextBox.Name = "internetRegexTextBox";
            // 
            // internetURLTextBox
            // 
            resources.ApplyResources(this.internetURLTextBox, "internetURLTextBox");
            this.internetURLTextBox.Name = "internetURLTextBox";
            // 
            // internetRegexLabel
            // 
            resources.ApplyResources(this.internetRegexLabel, "internetRegexLabel");
            this.internetRegexLabel.Name = "internetRegexLabel";
            // 
            // internetURLLabel
            // 
            resources.ApplyResources(this.internetURLLabel, "internetURLLabel");
            this.internetURLLabel.Name = "internetURLLabel";
            // 
            // batchButton
            // 
            resources.ApplyResources(this.batchButton, "batchButton");
            this.batchButton.Name = "batchButton";
            this.batchButton.UseVisualStyleBackColor = true;
            this.batchButton.Click += new System.EventHandler(this.batchButton_Click);
            // 
            // saveRegexButton
            // 
            resources.ApplyResources(this.saveRegexButton, "saveRegexButton");
            this.saveRegexButton.Image = global::BatchRenamer.Properties.Resources.Save_Small;
            this.saveRegexButton.Name = "saveRegexButton";
            this.saveRegexButton.UseVisualStyleBackColor = true;
            this.saveRegexButton.Click += new System.EventHandler(this.saveRegexButton_Click);
            // 
            // loadRegexButton
            // 
            resources.ApplyResources(this.loadRegexButton, "loadRegexButton");
            this.loadRegexButton.Image = global::BatchRenamer.Properties.Resources.Open_Small;
            this.loadRegexButton.Name = "loadRegexButton";
            this.loadRegexButton.UseVisualStyleBackColor = true;
            this.loadRegexButton.Click += new System.EventHandler(this.loadRegexButton_Click);
            // 
            // fileNameNewPatternTextBox
            // 
            resources.ApplyResources(this.fileNameNewPatternTextBox, "fileNameNewPatternTextBox");
            this.fileNameNewPatternTextBox.Name = "fileNameNewPatternTextBox";
            // 
            // fileNameNewPatternLabel
            // 
            resources.ApplyResources(this.fileNameNewPatternLabel, "fileNameNewPatternLabel");
            this.fileNameNewPatternLabel.Name = "fileNameNewPatternLabel";
            // 
            // fileNameGroupBox
            // 
            resources.ApplyResources(this.fileNameGroupBox, "fileNameGroupBox");
            this.fileNameGroupBox.Controls.Add(this.fileNameRegexLlabel);
            this.fileNameGroupBox.Controls.Add(this.fileNameNewPatternLabel);
            this.fileNameGroupBox.Controls.Add(this.fileNameRegexTextBox);
            this.fileNameGroupBox.Controls.Add(this.fileNameNewPatternTextBox);
            this.fileNameGroupBox.Name = "fileNameGroupBox";
            this.fileNameGroupBox.TabStop = false;
            // 
            // InternetRegexBatchPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fileNameGroupBox);
            this.Controls.Add(this.batchButton);
            this.Controls.Add(this.saveRegexButton);
            this.Controls.Add(this.loadRegexButton);
            this.Controls.Add(this.internetRegexGroupBox);
            this.MinimumSize = new System.Drawing.Size(702, 135);
            this.Name = "InternetRegexBatchPanel";
            this.internetRegexGroupBox.ResumeLayout(false);
            this.internetRegexGroupBox.PerformLayout();
            this.fileNameGroupBox.ResumeLayout(false);
            this.fileNameGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label fileNameRegexLlabel;
        private System.Windows.Forms.TextBox fileNameRegexTextBox;
        private System.Windows.Forms.GroupBox internetRegexGroupBox;
        private System.Windows.Forms.TextBox internetRegexTextBox;
        private System.Windows.Forms.TextBox internetURLTextBox;
        private System.Windows.Forms.Label internetRegexLabel;
        private System.Windows.Forms.Label internetURLLabel;
        private System.Windows.Forms.Button loadRegexButton;
        private System.Windows.Forms.Button saveRegexButton;
        private System.Windows.Forms.Button batchButton;
        private System.Windows.Forms.TextBox fileNameNewPatternTextBox;
        private System.Windows.Forms.Label fileNameNewPatternLabel;
        private System.Windows.Forms.GroupBox fileNameGroupBox;
    }
}
