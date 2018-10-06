using System;
using System.ComponentModel;
using System.IO;
using System.Windows;

using Microsoft.Win32;

using Xlfdll.Windows.Presentation;

using WinForms = System.Windows.Forms;

namespace BatchRenamer
{
    public static class BatchCommands
    {
        static BatchCommands()
        {
            BatchCommands.AddFileCommand = new RelayCommand<Object>
            (
                delegate
                {
                    OpenFileDialog dlg = new OpenFileDialog()
                    {
                        Filter = "All Files (*.*)|*.*",
                        Multiselect = true
                    };

                    if (dlg.ShowDialog() == true)
                    {
                        FileHelper.AddNewFiles(dlg.FileNames);
                    }
                }
            );

            BatchCommands.AddFolderCommand = new RelayCommand<Object>
            (
                delegate
                {
                    using (WinForms.FolderBrowserDialog dlg = new WinForms.FolderBrowserDialog()
                    {
                        Description = "Select a folder to add its files",
                        ShowNewFolderButton = false
                    })
                    {
                        if (dlg.ShowDialog() == WinForms.DialogResult.OK)
                        {
                            FileHelper.AddNewFiles(Directory.GetFiles(dlg.SelectedPath));
                        }
                    }
                }
            );

            BatchCommands.BatchRenameCommand = new RelayCommand<Object>
            (
                delegate
                {
                    MessageBoxResult result = MessageBox.Show(Application.Current.MainWindow,
                       "The files will be renamed according to the new file names."
                       + Environment.NewLine
                       + "This operation is NOT reversible."
                       + Environment.NewLine
                       + Environment.NewLine
                       + "Continue?",
                       Application.Current.MainWindow.Title,
                       MessageBoxButton.YesNo,
                       MessageBoxImage.Question,
                       MessageBoxResult.No);

                    if (result == MessageBoxResult.Yes)
                    {
                        using (BackgroundWorker backgroundWorker = new BackgroundWorker() { WorkerReportsProgress = true })
                        {
                            backgroundWorker.DoWork += Routines.BatchRenameBackgroundWorker_DoWork;
                            backgroundWorker.ProgressChanged += Routines.BatchRenameBackgroundWorker_ProgressChanged;
                            backgroundWorker.RunWorkerCompleted += Routines.BatchRenameBackgroundWorker_RunWorkerCompleted;

                            AppState.Current.IsBusy = true;

                            backgroundWorker.RunWorkerAsync();
                        }
                    }
                },
                delegate
                {
                    return AppState.Current?.Files.Count > 0
                        && AppState.Current.IsModified;
                }
            );
        }

        public static RelayCommand<Object> AddFileCommand { get; }
        public static RelayCommand<Object> AddFolderCommand { get; }
        public static RelayCommand<Object> BatchRenameCommand { get; }
    }
}