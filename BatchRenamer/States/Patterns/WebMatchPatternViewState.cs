using System;
using System.Collections.ObjectModel;

using Xlfdll;

namespace BatchRenamer.Patterns
{
    public class WebMatchPatternViewState : ObservableObject
    {
        public WebMatchPatternViewState()
        {
            this.IsReady = true;

            this.WebURL = String.Empty;
            this.WebRegex = String.Empty;
            this.FileNameRegex = String.Empty;
            this.FileNameReplacement = String.Empty;

            this.SelectedGroupIndex = -1;

            this.Files = new ObservableCollection<PatternFileInfo>();
            this.Groups = new ObservableCollection<String>();
            this.Matches = new ObservableCollection<WebRegexMatch>();

            WebMatchPatternViewState.Current = this;
        }

        private Boolean _isReady;
        private String _webURL;
        private String _webRegex;
        private String _fileNameRegex;
        private String _fileNameReplacement;
        private Int32 _selectedGroupIndex;

        public Boolean IsReady
        {
            get { return _isReady; }
            set { SetField(ref _isReady, value); }
        }
        public String WebURL
        {
            get { return _webURL; }
            set { SetField(ref _webURL, value); }
        }
        public String WebRegex
        {
            get { return _webRegex; }
            set { SetField(ref _webRegex, value); }
        }
        public String FileNameRegex
        {
            get { return _fileNameRegex; }
            set { SetField(ref _fileNameRegex, value); }
        }
        public String FileNameReplacement
        {
            get { return _fileNameReplacement; }
            set { SetField(ref _fileNameReplacement, value); }
        }
        public Int32 SelectedGroupIndex
        {
            get { return _selectedGroupIndex; }
            set { SetField(ref _selectedGroupIndex, value); }
        }

        public ObservableCollection<PatternFileInfo> Files { get; }
        public ObservableCollection<String> Groups { get; }
        public ObservableCollection<WebRegexMatch> Matches { get; }

        public event Action RequestClose;

        public void Close()
        {
            this.RequestClose?.Invoke();

            WebMatchPatternViewState.Current = null;
        }

        public String LastWebRegex { get; set; }
        public Boolean IsError { get; set; }

        public static WebMatchPatternViewState Current { get; private set; }
        public static readonly String Title = "Web Match";
    }
}