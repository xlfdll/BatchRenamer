using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml;

using Microsoft.Win32;

using Xlfdll.Text;
using Xlfdll.Windows.Presentation;
using Xlfdll.Windows.Presentation.Dialogs;

namespace BatchRenamer.Patterns
{
    public class WebMatchPatternViewModel : PatternViewModelBase
    {
        public WebMatchPatternViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            this.IsReady = true;

            this.WebURL = String.Empty;
            this.WebRegex = String.Empty;
            this.FileNameRegex = String.Empty;
            this.FileNameReplacement = String.Empty;

            this.Groups = new ObservableCollection<String>();
            this.Matches = new ObservableCollection<WebRegexMatch>();

            this.ResetGroups(true);
        }

        private Boolean _isReady;
        private String _webURL;
        private String _webRegex;
        private String _fileNameRegex;
        private String _fileNameReplacement;
        private Int32 _selectedGroupIndex;

        public Boolean IsReady
        {
            get { return _isReady; }
            set { SetField(ref _isReady, value); }
        }
        public String WebURL
        {
            get { return _webURL; }
            set { SetField(ref _webURL, value); }
        }
        public String WebRegex
        {
            get { return _webRegex; }
            set { SetField(ref _webRegex, value); }
        }
        public String FileNameRegex
        {
            get { return _fileNameRegex; }
            set { SetField(ref _fileNameRegex, value); }
        }
        public String FileNameReplacement
        {
            get { return _fileNameReplacement; }
            set { SetField(ref _fileNameReplacement, value); }
        }
        public Int32 SelectedGroupIndex
        {
            get { return _selectedGroupIndex; }
            set { SetField(ref _selectedGroupIndex, value); }
        }

        public ObservableCollection<String> Groups { get; }
        public ObservableCollection<WebRegexMatch> Matches { get; }

        public RelayCommand<Object> RetrieveCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    this.IsReady = false;

                    WebClient webClient = new WebClient()
                    {
                        Encoding = Encoding.UTF8
                    };

                    webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;

                    try
                    {
                        Uri uri = new Uri(this.WebURL);

                        webClient.DownloadStringAsync(uri);
                    }
                    catch (UriFormatException uriFormatEx)
                    {
                        ExceptionMessageBox.Show(WebMatchPatternViewModel.Title, "The format of the URL " +
                            $"{Environment.NewLine}{Environment.NewLine}{this.WebURL}{Environment.NewLine}{Environment.NewLine}" +
                            " is not correct.", uriFormatEx);

                        this.IsReady = true;
                    }
                    catch (Exception ex)
                    {
                        ExceptionMessageBox.Show(WebMatchPatternViewModel.Title, "The following error occurred:", ex);

                        this.IsReady = true;
                    }
                    finally
                    {
                        webClient.Dispose();
                    }
                },
                delegate
                {
                    return !String.IsNullOrEmpty(this.WebURL.Trim())
                    && !String.IsNullOrEmpty(this.WebRegex.Trim());
                }
            );

        public RelayCommand<Object> OpenCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    OpenFileDialog dlg = new OpenFileDialog()
                    {
                        Filter = "Web Match Files (*.webmatch)|*.webmatch|All Files (*.*)|*.*"
                    };

                    if (dlg.ShowDialog() == true)
                    {
                        Int32 i = 0;
                        String[] values = new String[5];

                        using (StreamReader streamReader = new StreamReader(dlg.FileName, AdditionalEncodings.UTF8WithoutBOM))
                        {
                            using (XmlReader xmlReader = XmlReader.Create(streamReader))
                            {
                                while (xmlReader.Read())
                                {
                                    if (xmlReader.NodeType == XmlNodeType.Text)
                                    {
                                        values[i++] = xmlReader.Value;
                                    }
                                }
                            }
                        }

                        this.WebURL = values[0];
                        this.WebRegex = values[1];

                        this.FileNameRegex = values[2];
                        this.FileNameReplacement = values[3];
                    }
                }
            );

        public RelayCommand<Object> SaveCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    SaveFileDialog dlg = new SaveFileDialog()
                    {
                        Filter = "Web Match Files (*.webmatch)|*.webmatch|All Files (*.*)|*.*"
                    };

                    if (dlg.ShowDialog() == true)
                    {
                        using (StreamWriter streamWriter = new StreamWriter(dlg.FileName, false, AdditionalEncodings.UTF8WithoutBOM))
                        {
                            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
                            {
                                Indent = true,
                                IndentChars = "\t",
                                NewLineOnAttributes = true
                            };

                            using (XmlWriter xmlWriter = XmlWriter.Create(streamWriter, xmlWriterSettings))
                            {
                                xmlWriter.WriteStartDocument();
                                xmlWriter.WriteStartElement("BatchRenamer");
                                xmlWriter.WriteStartElement("WebMatch");

                                xmlWriter.WriteStartElement("WebURL");
                                xmlWriter.WriteString(this.WebURL);
                                xmlWriter.WriteEndElement();
                                xmlWriter.WriteStartElement("WebRegex");
                                xmlWriter.WriteString(this.WebRegex);
                                xmlWriter.WriteEndElement();
                                xmlWriter.WriteStartElement("FileNameRegex");
                                xmlWriter.WriteString(this.FileNameRegex);
                                xmlWriter.WriteEndElement();
                                xmlWriter.WriteStartElement("FileNameReplacement");
                                xmlWriter.WriteString(this.FileNameReplacement);
                                xmlWriter.WriteEndElement();

                                xmlWriter.WriteEndElement();
                                xmlWriter.WriteEndElement();
                                xmlWriter.WriteEndDocument();
                            }
                        }
                    }
                },
                delegate
                {
                    return !String.IsNullOrEmpty(this.WebURL.Trim())
                        && !String.IsNullOrEmpty(this.WebRegex.Trim())
                        && !String.IsNullOrEmpty(this.FileNameRegex.Trim())
                        && !String.IsNullOrEmpty(this.FileNameReplacement.Trim());
                }
            );

        public RelayCommand<Object> PreviewCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    String matchGroupName = this.Groups[this.SelectedGroupIndex];
                    Dictionary<String, PatternFileInfo> fileMatches = new Dictionary<String, PatternFileInfo>();

                    try
                    {
                        foreach (PatternFileInfo item in this.Files)
                        {
                            Match match = Regex.Match(item.FileInfo.NewFileName, this.FileNameRegex);

                            foreach (Group group in match.Groups)
                            {
                                if (group.Name == matchGroupName
                                    && !fileMatches.ContainsKey(group.Value))
                                {
                                    fileMatches.Add(group.Value, item);
                                }
                            }
                        }
                    }
                    catch (ArgumentException argumentEx)
                    {
                        ExceptionMessageBox.Show(WebMatchPatternViewModel.Title, "The format of the expression " +
                            $"{Environment.NewLine}{Environment.NewLine}{this.FileNameRegex}{Environment.NewLine}{Environment.NewLine}" +
                            " is not correct.", argumentEx);

                        this.IsError = true;
                    }

                    try
                    {
                        if (fileMatches.Count > 0)
                        {
                            foreach (KeyValuePair<String, PatternFileInfo> pair in fileMatches)
                            {
                                WebRegexMatch webMatch = this.Matches.FirstOrDefault
                                    (m => (m.Children.FirstOrDefault(g => g.Key == matchGroupName).Value == pair.Key));

                                if (webMatch != null)
                                {
                                    pair.Value.PreviewFileName = Regex.Replace(webMatch.Value,
                                        this.LastWebRegex,
                                        this.FileNameReplacement);
                                }
                            }

                            this.IsError = false;
                        }
                        else
                        {
                            MessageBox.Show($"The source expression does not have matched group name {matchGroupName}.", WebMatchPatternViewModel.Title, MessageBoxButton.OK, MessageBoxImage.Warning);

                            this.IsError = true;
                        }
                    }
                    catch (ArgumentException argumentEx)
                    {
                        ExceptionMessageBox.Show(WebMatchPatternViewModel.Title, "The format of the replacement pattern " +
                            $"{Environment.NewLine}{Environment.NewLine}{this.FileNameReplacement}{Environment.NewLine}{Environment.NewLine}" +
                            " is not correct.", argumentEx);

                        this.IsError = true;
                    }
                },
                delegate
                {
                    return !String.IsNullOrEmpty(this?.FileNameRegex.Trim())
                    && !String.IsNullOrEmpty(this?.FileNameReplacement.Trim());
                }
            );

        public RelayCommand<Object> ApplyCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    PreviewCommand.Execute(null);

                    if (this.IsError)
                    {
                        return;
                    }

                    foreach (PatternFileInfo item in this.Files)
                    {
                        item.FileInfo.NewFileName = item.PreviewFileName;
                    }

                    this.Close();
                },
                delegate
                {
                    return !String.IsNullOrEmpty(this?.WebURL.Trim())
                    && !String.IsNullOrEmpty(this?.WebRegex.Trim())
                    && !String.IsNullOrEmpty(this?.FileNameRegex.Trim())
                    && !String.IsNullOrEmpty(this?.FileNameReplacement.Trim());
                }
            );

        private void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    MatchCollection matchCollection = Regex.Matches(e.Result, this.WebRegex);

                    this.Matches.Clear();
                    this.Groups.Clear();

                    foreach (Match match in matchCollection)
                    {
                        WebRegexMatch webRegexMatch = new WebRegexMatch("Match", match.Value);

                        foreach (Group group in match.Groups)
                        {
                            if (group.Name != "0" && !this.Groups.Contains(group.Name))
                            {
                                this.Groups.Add(group.Name);
                            }

                            WebRegexMatch webGroupMatch = new WebRegexMatch(group.Name, group.Value);

                            webRegexMatch.Children.Add(webGroupMatch);
                        }

                        this.Matches.Add(webRegexMatch);
                    }

                    this.LastWebRegex = this.WebRegex;
                }
                catch (ArgumentException argumentEx)
                {
                    ExceptionMessageBox.Show(WebMatchPatternViewModel.Title, "The format of the expression " +
                        $"{Environment.NewLine}{Environment.NewLine}{this.WebRegex}{Environment.NewLine}{Environment.NewLine}" +
                        " is not correct.", argumentEx);
                }
            }
            else
            {
                ExceptionMessageBox.Show(WebMatchPatternViewModel.Title, $"The following error occurred when retrieving the content from {this.WebURL}:", e.Error);
            }

            this.IsReady = true;

            this.ResetGroups(false);
        }

        private void ResetGroups(Boolean isForced)
        {
            if (isForced)
            {
                this.Groups.Clear();
            }

            if (this.Groups.Count == 0)
            {
                this.Groups.Add("-- Retrieve Web Source to get Match Groups --");
            }

            this.SelectedGroupIndex = 0;
        }

        public String LastWebRegex { get; set; }

        public const String Title = "Web Match";
    }
}