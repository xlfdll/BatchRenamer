using System.Collections.Generic;
using System.Windows.Controls;

using Xlfdll.Windows.Presentation;

namespace BatchRenamer
{
    public static class LayoutHelper
    {
        public static void AdjustListViewColumns(ListView listView)
        {
            GridView gridView = listView.View as GridView;

            if (gridView != null)
            {
                List<GridViewColumn> skippedGridViewColumns = new List<GridViewColumn>();

                if (listView.HasItems)
                {
                    skippedGridViewColumns.Add(gridView.Columns[gridView.Columns.Count - 2]);
                    skippedGridViewColumns.Add(gridView.Columns[gridView.Columns.Count - 1]);
                }

                gridView.UniformColumnWidth(listView.ActualWidth, skippedGridViewColumns);
            }
        }
    }
}