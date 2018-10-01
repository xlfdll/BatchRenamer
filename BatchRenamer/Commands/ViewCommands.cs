using System;
using System.Linq;

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
                    ProcessHelper.Start(AppState.Current.SelectedFiles[0].OriginalFullPath, true);
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
        public static RelayCommand<Object> RevertSelectedCommand { get; }
        public static RelayCommand<Object> RemoveSelectedCommand { get; }
        public static RelayCommand<Object> SelectAllCommand { get; }
        public static RelayCommand<Object> DeselectAllCommand { get; }
    }
}