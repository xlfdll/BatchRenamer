using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

using Xlfdll.Windows.Presentation;

namespace BatchRenamer
{
    public static class PatternCommands
    {
        static PatternCommands()
        {
            PatternCommands.OpenPatternCommand = new RelayCommand<String>
            (
                delegate (String argument)
                {
                    IEnumerable<BatchFileInfo> files = AppState.Current.SelectedFiles.Count > 0 ? AppState.Current.SelectedFiles : AppState.Current.Files;

                    Type t = Type.GetType($"BatchRenamer.Patterns.{argument}PatternWindow, {Assembly.GetExecutingAssembly().FullName}");

                    Window patternWindow = Activator.CreateInstance(t, files) as Window;

                    patternWindow.Owner = App.MainWindow;
                    patternWindow.ShowDialog();
                },
                delegate
                {
                    return AppState.Current?.Files.Count > 0;
                }
            );
        }

        public static RelayCommand<String> OpenPatternCommand { get; }
    }
}