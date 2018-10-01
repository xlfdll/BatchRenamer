using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

using Xlfdll.IO;

namespace BatchRenamer
{
    public class FileSizeValueConverter : MarkupExtension, IValueConverter
    {
        public override Object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            Int64 fileSize = System.Convert.ToInt64(value);

            return FileInfoExtensions.GetHumanReadableSizeString(fileSize, 0);
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}