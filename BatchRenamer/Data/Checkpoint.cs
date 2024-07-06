using System;
using System.Linq;

using BatchRenamer.Patterns;

namespace BatchRenamer
{
    public class Checkpoint
    {
        public Checkpoint(String pattern, String description, PatternFileInfo[] affectedFiles)
        {
            this.Pattern = pattern;
            this.Description = description;
            this.AffectedFiles = affectedFiles.Select(f => f.FileInfo.Clone()).ToArray();
        }

        public String Pattern { get; }
        public String Description { get; }
        public BatchFileInfo[] AffectedFiles { get; }
    }
}