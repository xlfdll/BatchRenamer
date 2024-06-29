using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace BatchRenamer
{
    public static class DataHelper
    {
        public static CollectionViewSource GetCollectionViewSource<T>(IEnumerable<T> values, String groupPropertyName = null)
        {
            CollectionViewSource collectionViewSource = new CollectionViewSource()
            {
                Source = values
            };

            if (!String.IsNullOrEmpty(groupPropertyName))
            {
                collectionViewSource.GroupDescriptions.Add(new PropertyGroupDescription(groupPropertyName));
            }

            return collectionViewSource;
        }
    }
}