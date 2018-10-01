using System;
using System.Collections.Generic;
using System.Linq;

namespace BatchRenamer
{
    public static class FileHelper
    {
        public static void AddNewFiles(IEnumerable<String> fileNames)
        {
            // Manually check original file name here to avoid broken item reference issue when removing files later on
            foreach (String fileName in fileNames)
            {
                if (AppState.Current.Files.Where(f => (f.OriginalFullPath == fileName)).Count() == 0)
                {
                    AppState.Current.Files.Add(new BatchFileInfo(fileName));
                }
            }
        }
    }
}