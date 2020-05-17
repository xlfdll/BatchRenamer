using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

using Xlfdll.Collections;
using Xlfdll.Diagnostics;
using Xlfdll.Windows.Presentation;

namespace BatchRenamer
{
    public static class ViewCommands
    {
        static ViewCommands()
        {
            ViewCommands.OpenCommand = new RelayCommand<Object>
            (
                delegate
                {
                    try
                    {
                        ProcessHelper.Start(AppState.Current.SelectedFiles[0].OriginalFullPath, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(App.Current.MainWindow,
                            $"An error occurred when opening the file {Environment.NewLine}" +
                            $"{AppState.Current.SelectedFiles[0].OriginalFullPath}" +
                            $"{Environment.NewLine}" +
                            $"{Environment.NewLine}" +
                            $"{ex.Message}",
                            App.Current.MainWindow.Title,
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                },
                delegate
                {
                    return AppState.Current?.SelectedFiles.Count == 1;
                }
            );

            ViewCommands.OpenFolderCommand = new RelayCommand<Object>
            (
                delegate
                {
                    ProcessHelper.Start("explorer.exe", $"/select,\"{AppState.Current.SelectedFiles[0].OriginalFullPath}\"");
                },
                delegate
                {
                    return AppState.Current?.SelectedFiles.Count == 1;
                }
            );

            ViewCommands.CopyCommand = new RelayCommand<String>
            (
                (parameter) =>
                {
                    StringBuilder sb = new StringBuilder();

                    AppState.Current.SelectedFiles.ForEach
                    (
                        (item) =>
                        {
                            switch (parameter)
                            {
                                case "NewFileName":
                                    {
                                        sb.AppendLine(item.NewFileName);

                                        break;
                                    }
                                case "OriginalFileName":
                                    {
                                        sb.AppendLine(item.OriginalFileName);

                                        break;
                                    }
                                case "NewFullPath":
                                    {
                                        sb.AppendLine(item.NewFullPath);

                                        break;
                                    }
                                case "OriginalFullPath":
                                    {
                                        sb.AppendLine(item.OriginalFullPath);

                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                        }
                    );

                    Clipboard.SetText(sb.ToString());
                },
                (parameter) =>
                {
                    return AppState.Current?.SelectedFiles.Count > 0;
                }
            );

            ViewCommands.PasteCommand = new RelayCommand<Object>
            (
                delegate
                {
                    using (StringReader reader = new StringReader(Clipboard.GetText()))
                    {
                        Int32 i = 0;
                        String line = reader.ReadLine();

                        while (line != null && i < AppState.Current.SelectedFiles.Count)
                        {
                            line = line.Trim('"').Trim();

                            if (!String.IsNullOrEmpty(line))
                            {
                                AppState.Current.SelectedFiles[i++].NewFileName = Path.GetFileNameWithoutExtension(line);
                            }

                            line = reader.ReadLine();
                        }
                    }
                },
                delegate
                {
                    return AppState.Current?.SelectedFiles.Count > 0;
                }
            );

            ViewCommands.RevertSelectedCommand = new RelayCommand<Object>
            (
                delegate
                {
                    AppState.Current.SelectedFiles.ForEach
                    (
                        (item) => { item.NewFileName = item.OriginalFileName; }
                    );
                },
                delegate
                {
                    return AppState.Current?.SelectedFiles.Count > 0;
                }
            );

            ViewCommands.RemoveSelectedCommand = new RelayCommand<Object>
            (
                delegate
                {
                    AppState.Current.Files.RemoveRange(AppState.Current.SelectedFiles);
                },
                delegate
                {
                    return AppState.Current?.SelectedFiles.Count > 0;
                }
            );

            ViewCommands.SelectAllCommand = new RelayCommand<Object>
            (
                delegate
                {
                    AppState.Current.Files.ForEach
                    (
                        (item) => { item.IsSelected = true; }
                    );
                },
                delegate
                {
                    return AppState.Current?.Files.Count > 0
                        && AppState.Current.SelectedFiles.Count != AppState.Current.Files.Count();
                }
            );

            ViewCommands.DeselectAllCommand = new RelayCommand<Object>
            (
                delegate
                {
                    AppState.Current.Files.ForEach
                    (
                        (item) => { item.IsSelected = false; }
                    );
                },
                delegate
                {
                    return AppState.Current?.SelectedFiles.Count > 0;
                }
            );
        }

        public static RelayCommand<Object> OpenCommand { get; }
        public static RelayCommand<Object> OpenFolderCommand { get; }
        public static RelayCommand<String> CopyCommand { get; }
        public static RelayCommand<Object> PasteCommand { get; }
        public static RelayCommand<Object> RevertSelectedCommand { get; }
        public static RelayCommand<Object> RemoveSelectedCommand { get; }
        public static RelayCommand<Object> SelectAllCommand { get; }
        public static RelayCommand<Object> DeselectAllCommand { get; }
    }
}