using System;
using System.Collections.ObjectModel;

namespace BatchRenamer.Patterns
{
    public class WebRegexMatch
    {
        public WebRegexMatch(String key, String value)
        {
            this.Key = key;
            this.Value = value;
            this.Children = new ObservableCollection<WebRegexMatch>();
        }

        public String Key { get; }
        public String Value { get; }
        public ObservableCollection<WebRegexMatch> Children { get; }
    }
}