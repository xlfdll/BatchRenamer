using System;
using System.ComponentModel;

using Xlfdll.Windows.Presentation.Dialogs;

namespace BatchRenamer
{
    public static class WorkHelper
    {
        public static void PerformBatchRename(MainViewModel viewModel)
        {
            using (BackgroundWorker backgroundWorker = new BackgroundWorker() { WorkerReportsProgress = true })
            {
                backgroundWorker.DoWork += BatchRenameBackgroundWorker_DoWork;
                backgroundWorker.ProgressChanged += BatchRenameBackgroundWorker_ProgressChanged;
                backgroundWorker.RunWorkerCompleted += BatchRenameBackgroundWorker_RunWorkerCompleted;

                WorkHelper.MainViewModel = viewModel;
                WorkHelper.MainViewModel.IsBusy = true;

                backgroundWorker.RunWorkerAsync();
            }
        }

        private static void BatchRenameBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Int32 i = -1;
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                for (i = 0; i < WorkHelper.MainViewModel.Files.Count; i++)
                {
                    worker.ReportProgress(i);

                    WorkHelper.MainViewModel.Files[i].CommitRename();
                }
            }
            catch (Exception ex)
            {
                e.Result = new Object[] { ex, WorkHelper.MainViewModel.Files[i] };
            }
        }

        private static void BatchRenameBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            WorkHelper.MainViewModel.UpdateStatus($"({e.ProgressPercentage} / {WorkHelper.MainViewModel.Files.Count}) Processing {WorkHelper.MainViewModel.Files[e.ProgressPercentage].OriginalFileName} ...");
        }

        private static void BatchRenameBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null)
            {
                WorkHelper.MainViewModel.UpdateStatus("Done");
            }
            else
            {
                Object[] results = e.Result as Object[];
                Exception ex = results[0] as Exception;
                BatchFileInfo batchFileInfo = results[1] as BatchFileInfo;

                WorkHelper.MainViewModel.UpdateStatus($"An error occurred: {ex.Message} ({batchFileInfo.OriginalFileName} => {batchFileInfo.NewFileName})");

                ExceptionMessageBox.Show(App.MainWindow.Title, $"The following error occurred when renaming the file {batchFileInfo.OriginalFileName} to {batchFileInfo.NewFileName}:", ex);
            }

            WorkHelper.MainViewModel.IsBusy = false;
            WorkHelper.MainViewModel = null;
        }

        private static MainViewModel MainViewModel { get; set; }
    }
}