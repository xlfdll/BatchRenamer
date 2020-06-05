using System;
using System.ComponentModel;

using Xlfdll.Windows.Presentation.Dialogs;

namespace BatchRenamer
{
	public static class Routines
	{
		public static void BatchRenameBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Int32 i = -1;
			BackgroundWorker worker = sender as BackgroundWorker;

			try
			{
				for (i = 0; i < AppState.Current.Files.Count; i++)
				{
					worker.ReportProgress(i);

					AppState.Current.Files[i].CommitRename();
				}
			}
			catch (Exception ex)
			{
				e.Result = new Object[] { ex, AppState.Current.Files[i] };
			}
		}

		public static void BatchRenameBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			AppState.Current.UpdateStatus($"({e.ProgressPercentage} / {AppState.Current.Files.Count}) Processing {AppState.Current.Files[e.ProgressPercentage].OriginalFileName} ...");
		}

		public static void BatchRenameBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Result == null)
			{
				AppState.Current.UpdateStatus("Done");
			}
			else
			{
				Object[] results = e.Result as Object[];
				Exception ex = results[0] as Exception;
				BatchFileInfo batchFileInfo = results[1] as BatchFileInfo;

				AppState.Current.UpdateStatus($"An error occurred: {ex.Message} ({batchFileInfo.OriginalFileName} => {batchFileInfo.NewFileName})");

				ExceptionMessageBox.Show(App.MainWindow.Title, $"The following error occurred when renaming the file {batchFileInfo.OriginalFileName} to {batchFileInfo.NewFileName}:", ex);
			}

			AppState.Current.IsBusy = false;
		}
	}
}