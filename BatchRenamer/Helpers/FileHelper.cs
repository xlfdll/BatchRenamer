using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BatchRenamer
{
    public static class FileHelper
    {
        public static void AddNewFiles(IEnumerable<String> fileNames)
        {
            List<String> files = new List<String>();

            // Manually check original file name here to avoid broken item reference issue when removing files later on
            foreach (String fileName in fileNames)
            {
                FileAttributes attributes = File.GetAttributes(fileName);

                if (attributes.HasFlag(FileAttributes.Directory))
                {
                    files.AddRange(Directory.GetFiles(fileName));
                }
                else
                {
                    files.Add(fileName);
                }
            }

            foreach (String file in files)
            {
                if (AppState.Current.Files.Where(f => (f.OriginalFullPath == file)).Count() == 0)
                {
                    AppState.Current.Files.Add(new BatchFileInfo(file));
                }
            }
        }
    }
}