using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StringUpperLower
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            typeComboBox.SelectedIndex = 0;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            originalComboBox.Items.Add(originalComboBox.Text);

            processInputString();
        }

        private void originalComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            processInputString();
        }

        private void processInputString()
        {
            try
            {
                switch (typeComboBox.SelectedItem.ToString())
                {
                    case "HeadUp":
                        {
                            StringBuilder sb = new StringBuilder(originalComboBox.Text.ToLowerInvariant());
                            Boolean result = false;

                            if (!Char.IsWhiteSpace(sb[0]))
                            {
                                for (Int32 i = 0; i < sb.Length; i++)
                                {
                                    result = (i == 0 || (!Char.IsLetterOrDigit(sb[i - 1]) && !sb[i - 1].Equals('\'')));

                                    if (result)
                                    {
                                        sb[i] = Char.ToUpperInvariant(sb[i]);
                                    }
                                }
                            }

                            resultTextBox.Text = sb.ToString();
                        }

                        break;
                    case "Upper": resultTextBox.Text = originalComboBox.Text.ToUpperInvariant();
                        break;
                    case "Lower": resultTextBox.Text = originalComboBox.Text.ToLowerInvariant();
                        break;
                    case "Opposite":
                        {
                            StringBuilder sb = new StringBuilder(originalComboBox.Text);

                            for (Int32 i = 0; i < sb.Length; i++)
                            {
                                if (Char.IsLower(sb[i]))
                                {
                                    sb[i] = Char.ToUpperInvariant(sb[i]);
                                }
                                else if (Char.IsUpper(sb[i]))
                                {
                                    sb[i] = Char.ToLowerInvariant(sb[i]);
                                }

                                resultTextBox.Text = sb.ToString();
                            }
                        }

                        break;
                }

                resultTextBox.SelectAll();

                if (autoCopyCheckBox.Checked)
                {
                    resultTextBox.Copy();
                }
            }
            catch
            {
                resultTextBox.Text = String.Empty;
            }
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
