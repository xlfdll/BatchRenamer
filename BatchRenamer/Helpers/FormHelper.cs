using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace BatchRenamer.Helpers
{
    internal static class FormHelper
    {
        internal static Int32 AddFilesToListView(ListView listView, IEnumerable<String> fileNames)
        {
            Int32 fileNameCount = 0;
            Dictionary<String, ListViewGroup> listViewGroupDictionary = new Dictionary<String, ListViewGroup>();

            foreach (String fileName in fileNames)
            {
                // Items should be added into the list view, and for each item set its Group property to the desired group.

                String fileHash = FileHelper.GetFileNameHash(fileName);

                if (!listView.Items.ContainsKey(fileHash))
                {
                    String folderName = Path.GetDirectoryName(fileName);
                    String folderHash = FileHelper.GetFileNameHash(folderName);

                    if (!listViewGroupDictionary.ContainsKey(folderHash))
                    {
                        listViewGroupDictionary.Add(folderHash, new ListViewGroup(folderHash, folderName));

                        listView.Groups.Add(listViewGroupDictionary[folderHash]);
                    }

                    String file = Path.GetFileName(fileName);
                    ListViewItem listViewItem = new ListViewItem(new String[] { file, file });

                    listViewItem.Name = fileHash;
                    listViewGroupDictionary[folderHash].Items.Add(listViewItem);

                    listViewItem.Name = fileHash;
                    listViewItem.Group = listViewGroupDictionary[folderHash];
                    listView.Items.Add(listViewItem);

                    fileNameCount++;
                }
            }

            return fileNameCount;
        }

        internal static void SelectTextForListViewLabelEdit(ListView listView, Int32 start, Int32 length)
        {
            IntPtr editWnd = WinAPIHelper.SendMessage(listView.Handle, WinAPIHelper.LVM_GETEDITCONTROL, IntPtr.Zero, IntPtr.Zero);

            WinAPIHelper.SendMessage(editWnd, WinAPIHelper.EM_SETSEL, (IntPtr)start, (IntPtr)length);
        }
    }
}