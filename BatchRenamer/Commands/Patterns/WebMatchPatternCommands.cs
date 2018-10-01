using System;
using System.Collections.Generic;
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
    public static class WebMatchPatternCommands
    {
        static WebMatchPatternCommands()
        {
            WebMatchPatternCommands.RetrieveCommand = new RelayCommand<Object>
            (
                delegate
                {
                    WebMatchPatternViewState.Current.IsReady = false;

                    WebClient webClient = new WebClient()
                    {
                        Encoding = Encoding.UTF8
                    };

                    webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;

                    try
                    {
                        Uri uri = new Uri(WebMatchPatternViewState.Current.WebURL);

                        webClient.DownloadStringAsync(uri);
                    }
                    catch (UriFormatException uriFormatEx)
                    {
                        ExceptionMessageBox.Show(WebMatchPatternViewState.Title, "The format of the URL " +
                            $"{Environment.NewLine}{Environment.NewLine}{WebMatchPatternViewState.Current.WebURL}{Environment.NewLine}{Environment.NewLine}" +
                            " is not correct.", uriFormatEx);

                        WebMatchPatternViewState.Current.IsReady = true;
                    }
                    catch (Exception ex)
                    {
                        ExceptionMessageBox.Show(WebMatchPatternViewState.Title, "The following error occurred:", ex);

                        WebMatchPatternViewState.Current.IsReady = true;
                    }
                    finally
                    {
                        webClient.Dispose();
                    }
                },
                delegate
                {
                    return !String.IsNullOrEmpty(WebMatchPatternViewState.Current?.WebURL.Trim())
                    && !String.IsNullOrEmpty(WebMatchPatternViewState.Current?.WebRegex.Trim());
                }
            );

            WebMatchPatternCommands.OpenCommand = new RelayCommand<Object>
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

                        WebMatchPatternViewState.Current.WebURL = values[0];
                        WebMatchPatternViewState.Current.WebRegex = values[1];

                        WebMatchPatternViewState.Current.FileNameRegex = values[2];
                        WebMatchPatternViewState.Current.FileNameReplacement = values[3];
                    }
                }
            );

            WebMatchPatternCommands.SaveCommand = new RelayCommand<Object>
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
                                xmlWriter.WriteString(WebMatchPatternViewState.Current.WebURL);
                                xmlWriter.WriteEndElement();
                                xmlWriter.WriteStartElement("WebRegex");
                                xmlWriter.WriteString(WebMatchPatternViewState.Current.WebRegex);
                                xmlWriter.WriteEndElement();
                                xmlWriter.WriteStartElement("FileNameRegex");
                                xmlWriter.WriteString(WebMatchPatternViewState.Current.FileNameRegex);
                                xmlWriter.WriteEndElement();
                                xmlWriter.WriteStartElement("FileNameReplacement");
                                xmlWriter.WriteString(WebMatchPatternViewState.Current.FileNameReplacement);
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
                    return !String.IsNullOrEmpty(WebMatchPatternViewState.Current?.WebURL.Trim())
                    && !String.IsNullOrEmpty(WebMatchPatternViewState.Current?.WebRegex.Trim())
                    && !String.IsNullOrEmpty(WebMatchPatternViewState.Current?.FileNameRegex.Trim())
                    && !String.IsNullOrEmpty(WebMatchPatternViewState.Current?.FileNameReplacement.Trim());
                }
            );

            WebMatchPatternCommands.PreviewCommand = new RelayCommand<Object>
            (
                delegate
                {
                    String matchGroupName = WebMatchPatternViewState.Current.Groups[WebMatchPatternViewState.Current.SelectedGroupIndex];
                    Dictionary<String, PatternFileInfo> fileMatches = new Dictionary<String, PatternFileInfo>();

                    try
                    {
                        foreach (PatternFileInfo item in WebMatchPatternViewState.Current.Files)
                        {
                            Match match = Regex.Match(item.FileInfo.NewFileName, WebMatchPatternViewState.Current.FileNameRegex);

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
                        ExceptionMessageBox.Show(WebMatchPatternViewState.Title, "The format of the expression " +
                            $"{Environment.NewLine}{Environment.NewLine}{WebMatchPatternViewState.Current.FileNameRegex}{Environment.NewLine}{Environment.NewLine}" +
                            " is not correct.", argumentEx);

                        WebMatchPatternViewState.Current.IsError = true;
                    }

                    try
                    {
                        if (fileMatches.Count > 0)
                        {
                            foreach (KeyValuePair<String, PatternFileInfo> pair in fileMatches)
                            {
                                WebRegexMatch webMatch = WebMatchPatternViewState.Current.Matches.FirstOrDefault
                                    (m => (m.Children.FirstOrDefault(g => g.Key == matchGroupName).Value == pair.Key));

                                if (webMatch != null)
                                {
                                    pair.Value.PreviewFileName = Regex.Replace(webMatch.Value,
                                        WebMatchPatternViewState.Current.LastWebRegex,
                                        WebMatchPatternViewState.Current.FileNameReplacement);
                                }
                            }

                            WebMatchPatternViewState.Current.IsError = false;
                        }
                        else
                        {
                            MessageBox.Show($"The source expression does not have matched group name {matchGroupName}.", WebMatchPatternViewState.Title, MessageBoxButton.OK, MessageBoxImage.Warning);

                            WebMatchPatternViewState.Current.IsError = true;
                        }
                    }
                    catch (ArgumentException argumentEx)
                    {
                        ExceptionMessageBox.Show(WebMatchPatternViewState.Title, "The format of the replacement pattern " +
                            $"{Environment.NewLine}{Environment.NewLine}{WebMatchPatternViewState.Current.FileNameReplacement}{Environment.NewLine}{Environment.NewLine}" +
                            " is not correct.", argumentEx);

                        WebMatchPatternViewState.Current.IsError = true;
                    }
                },
                delegate
                {
                    return !String.IsNullOrEmpty(WebMatchPatternViewState.Current?.FileNameRegex.Trim())
                    && !String.IsNullOrEmpty(WebMatchPatternViewState.Current?.FileNameReplacement.Trim());
                }
            );

            WebMatchPatternCommands.ApplyCommand = new RelayCommand<Object>
            (
                delegate
                {
                    PreviewCommand.Execute(null);

                    if (WebMatchPatternViewState.Current.IsError)
                    {
                        return;
                    }

                    foreach (PatternFileInfo item in WebMatchPatternViewState.Current.Files)
                    {
                        AppState.Current.Files.First(f => f.OriginalFullPath == item.FileInfo.OriginalFullPath).NewFileName = item.PreviewFileName;
                    }

                    WebMatchPatternViewState.Current.Close();
                },
                delegate
                {
                    return !String.IsNullOrEmpty(WebMatchPatternViewState.Current?.WebURL.Trim())
                    && !String.IsNullOrEmpty(WebMatchPatternViewState.Current?.WebRegex.Trim())
                    && !String.IsNullOrEmpty(WebMatchPatternViewState.Current?.FileNameRegex.Trim())
                    && !String.IsNullOrEmpty(WebMatchPatternViewState.Current?.FileNameReplacement.Trim());
                }
            );
        }

        public static RelayCommand<Object> RetrieveCommand { get; }
        public static RelayCommand<Object> OpenCommand { get; }
        public static RelayCommand<Object> SaveCommand { get; }
        public static RelayCommand<Object> PreviewCommand { get; }
        public static RelayCommand<Object> ApplyCommand { get; }

        private static void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    MatchCollection matchCollection = Regex.Matches(e.Result, WebMatchPatternViewState.Current.WebRegex);

                    WebMatchPatternViewState.Current.Matches.Clear();
                    WebMatchPatternViewState.Current.Groups.Clear();

                    foreach (Match match in matchCollection)
                    {
                        WebRegexMatch webRegexMatch = new WebRegexMatch("Match", match.Value);

                        foreach (Group group in match.Groups)
                        {
                            if (group.Name != "0" && !WebMatchPatternViewState.Current.Groups.Contains(group.Name))
                            {
                                WebMatchPatternViewState.Current.Groups.Add(group.Name);
                            }

                            WebRegexMatch webGroupMatch = new WebRegexMatch(group.Name, group.Value);

                            webRegexMatch.Children.Add(webGroupMatch);
                        }

                        WebMatchPatternViewState.Current.Matches.Add(webRegexMatch);
                    }

                    WebMatchPatternViewState.Current.LastWebRegex = WebMatchPatternViewState.Current.WebRegex;
                }
                catch (ArgumentException argumentEx)
                {
                    ExceptionMessageBox.Show(WebMatchPatternViewState.Title, "The format of the expression " +
                        $"{Environment.NewLine}{Environment.NewLine}{WebMatchPatternViewState.Current.WebRegex}{Environment.NewLine}{Environment.NewLine}" +
                        " is not correct.", argumentEx);
                }
            }
            else
            {
                ExceptionMessageBox.Show(WebMatchPatternViewState.Title, $"The following error occurred when retrieving the content from {WebMatchPatternViewState.Current.WebURL}:", e.Error);
            }

            WebMatchPatternViewState.Current.IsReady = true;
            WebMatchPatternViewState.Current.SelectedGroupIndex = (WebMatchPatternViewState.Current.Groups.Count > 0) ? 0 : -1;
        }
    }
}