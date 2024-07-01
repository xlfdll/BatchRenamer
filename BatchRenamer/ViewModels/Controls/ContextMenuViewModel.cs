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
    public class ContextMenuViewModel : ViewModelBase
    {
        public ContextMenuViewModel(MainViewModel mainViewModel)
        {
            this.MainViewModel = mainViewModel;
        }

        public MainViewModel MainViewModel { get; }

        public RelayCommand<Object> OpenCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    try
                    {
                        ProcessHelper.Start(this.MainViewModel.SelectedFiles[0].OriginalFilePath, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(App.Current.MainWindow,
                            $"An error occurred when opening the file {Environment.NewLine}" +
                            $"{this.MainViewModel.SelectedFiles[0].OriginalFilePath}" +
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
                    return this.MainViewModel.SelectedFiles.Count == 1;
                }
            );

        public RelayCommand<Object> OpenFolderCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    ProcessHelper.Start("explorer.exe", $"/select,\"{this.MainViewModel.SelectedFiles[0].OriginalFilePath}\"");
                },
                delegate
                {
                    return this.MainViewModel.SelectedFiles.Count == 1;
                }
            );

        public RelayCommand<String> CopyCommand
            => new RelayCommand<String>
            (
                (parameter) =>
                {
                    StringBuilder sb = new StringBuilder();

                    this.MainViewModel.SelectedFiles.ForEach
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
                                case "NewFilePath":
                                    {
                                        sb.AppendLine(item.NewFilePath);

                                        break;
                                    }
                                case "OriginalFilePath":
                                    {
                                        sb.AppendLine(item.OriginalFilePath);

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
                    return this.MainViewModel.SelectedFiles.Count > 0;
                }
            );

        public RelayCommand<Object> PasteCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    using (StringReader reader = new StringReader(Clipboard.GetText()))
                    {
                        Int32 i = 0;
                        String line = reader.ReadLine();

                        while (line != null && i < this.MainViewModel.SelectedFiles.Count)
                        {
                            line = line.Trim('"').Trim();

                            if (!String.IsNullOrEmpty(line))
                            {
                                this.MainViewModel.SelectedFiles[i++].NewFileName = Path.GetFileNameWithoutExtension(line);
                            }

                            line = reader.ReadLine();
                        }
                    }
                },
                delegate
                {
                    return this.MainViewModel.SelectedFiles.Count > 0;
                }
            );

        public RelayCommand<Object> RevertSelectedCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    this.MainViewModel.SelectedFiles.ForEach
                    (
                        (item) => { item.NewFileName = item.OriginalFileName; }
                    );
                },
                delegate
                {
                    return this.MainViewModel.SelectedFiles.Count > 0;
                }
            );

        public RelayCommand<Object> RemoveSelectedCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    this.MainViewModel.RemoveFiles(this.MainViewModel.SelectedFiles);
                },
                delegate
                {
                    return this.MainViewModel.SelectedFiles.Count > 0;
                }
            );

        public RelayCommand<Object> SelectAllCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    this.MainViewModel.Files.ForEach
                    (
                        (item) => { item.IsSelected = true; }
                    );
                },
                delegate
                {
                    return this.MainViewModel.Files.Count > 0
                        && this.MainViewModel.SelectedFiles.Count != this.MainViewModel.Files.Count();
                }
            );

        public RelayCommand<Object> DeselectAllCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    this.MainViewModel.Files.ForEach
                    (
                        (item) => { item.IsSelected = false; }
                    );
                },
                delegate
                {
                    return this.MainViewModel.SelectedFiles.Count > 0;
                }
            );
    }
}