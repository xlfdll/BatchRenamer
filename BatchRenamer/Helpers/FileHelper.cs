using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BatchRenamer.Helpers
{
    internal static class FileHelper
    {
        internal static ReadOnlyCollection<String> GetFilesFromFolder(String folderName)
        {
            List<String> fileList = new List<String>();

            fileList.AddRange(Directory.GetFiles(folderName, "*.*", SearchOption.TopDirectoryOnly));

            foreach (String folder in Directory.GetDirectories(folderName, "*.*", SearchOption.TopDirectoryOnly))
            {
                if (VariableHelper.IncludeFolderItems)
                {
                    fileList.Add(folder);
                }

                if (VariableHelper.IncludeSubfolders)
                {
                    try
                    {
                        fileList.AddRange(GetFilesFromFolder(folder));
                    }
                    catch (UnauthorizedAccessException) { }
                }
            }

            return fileList.AsReadOnly();
        }

        internal static String GetFileNameHash(String fileName)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Byte b in hashProvider.ComputeHash(Encoding.UTF8.GetBytes(fileName)))
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        private static readonly HashAlgorithm hashProvider = SHA1.Create();
    }
}