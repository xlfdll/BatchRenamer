using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

using WinForms = System.Windows.Forms;

using Microsoft.Win32;

using Xlfdll.Windows.Presentation;
using Xlfdll.Windows.Presentation.Dialogs;

namespace BatchRenamer
{
    public class ToolBarViewModel : ViewModelBase
    {
        public ToolBarViewModel(MainViewModel mainViewModel)
        {
            this.MainViewModel = mainViewModel;
        }

        public MainViewModel MainViewModel { get; }

        public RelayCommand<Object> AddFileCommand
            => new RelayCommand<Object>
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
                        this.MainViewModel.AddFiles(dlg.FileNames);
                    }
                }
            );

        public RelayCommand<Object> AddFolderCommand
            => new RelayCommand<Object>
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
                            this.MainViewModel.AddFiles(Directory.GetFiles(dlg.SelectedPath));
                        }
                    }
                }
            );

        public RelayCommand<String> OpenPatternCommand
            => new RelayCommand<String>
            (
                delegate (String argument)
                {
                    Type t = Type.GetType($"BatchRenamer.Patterns.{argument}PatternWindow, {Assembly.GetExecutingAssembly().FullName}");
                    Window patternWindow = Activator.CreateInstance(t) as Window;

                    patternWindow.Owner = App.MainWindow;
                    patternWindow.ShowDialog();
                },
                delegate
                {
                    return this.MainViewModel.Files.Count > 0;
                }
            );

        public RelayCommand<Object> BatchRenameCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    MessageBoxResult result = MessageBox.Show(App.Current.MainWindow,
                       "The files will be renamed according to the new file names."
                       + Environment.NewLine
                       + "This operation is NOT reversible."
                       + Environment.NewLine
                       + Environment.NewLine
                       + "Continue?",
                       App.Current.MainWindow.Title,
                       MessageBoxButton.YesNo,
                       MessageBoxImage.Question,
                       MessageBoxResult.No);

                    if (result == MessageBoxResult.Yes)
                    {
                        WorkHelper.PerformBatchRename(this.MainViewModel);
                    }
                },
                delegate
                {
                    return this.MainViewModel.Files.Count > 0
                        && this.MainViewModel.Files.Count(f => f.IsModified) > 0;
                }
            );

        public RelayCommand<Object> AboutCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    AboutWindow aboutWindow = new AboutWindow
                    (App.MainWindow, App.Metadata,
                    new ApplicationPackUri("/Images/BatchRenamer.png"));

                    aboutWindow.ShowDialog();
                }
            );
    }
}