using System;

namespace BatchRenamer.Helpers
{
    internal static class VariableHelper
    {
        static VariableHelper()
        {
            _includeSubfolders = false;
            _includeFolderItems = false;
        }

        private static Boolean _includeSubfolders;
        private static Boolean _includeFolderItems;

        internal static Boolean IncludeSubfolders
        {
            get { return _includeSubfolders; }
            set { _includeSubfolders = value; }
        }

        internal static Boolean IncludeFolderItems
        {
            get { return _includeFolderItems; }
            set { _includeFolderItems = value; }
        }
    }
}