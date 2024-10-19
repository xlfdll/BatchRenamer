using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BatchRenamer.Helpers
{
    public static class NameHelper
    {
        static NameHelper()
        {
            NameHelper.InvalidFileNameChars = new HashSet<Char>(Path.GetInvalidFileNameChars());
            NameHelper.InvalidPathChars = new HashSet<Char>(Path.GetInvalidPathChars());
        }

        public static String Clean(this String input)
        {
            StringBuilder sb = new StringBuilder(input);

            for (Int32 i = 0; i < sb.Length; i++)
            {
                if (NameHelper.InvalidFileNameChars.Contains(sb[i]))
                {
                    sb[i] = '_';
                }
            }

            return sb.ToString();
        }

        public static IReadOnlyCollection<Char> InvalidFileNameChars { get; }
        public static IReadOnlyCollection<Char> InvalidPathChars { get; }
    }
}