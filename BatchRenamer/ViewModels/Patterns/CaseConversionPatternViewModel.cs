using System;
using System.Globalization;
using System.Text;

using Xlfdll.Windows.Presentation;
using Xlfdll.Windows.Presentation.Dialogs;

namespace BatchRenamer.Patterns
{
    public class CaseConversionPatternViewModel : PatternViewModelBase
    {
        public CaseConversionPatternViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            this.SelectedModeIndex = 0;
        }

        public String[] Modes
            => CaseConversionPatternViewModel.ModeNames;

        private Int32 _selectedModeIndex;

        public Int32 SelectedModeIndex
        {
            get => _selectedModeIndex;
            set => SetField(ref _selectedModeIndex, value);
        }

        public RelayCommand<Object> PreviewCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    try
                    {
                        foreach (PatternFileInfo item in this.Files)
                        {
                            this.ConvertCase(item, this.SelectedModeIndex);
                        }

                        this.IsError = false;
                    }
                    catch (ArgumentException argumentEx)
                    {
                        ExceptionMessageBox.Show(FindAndReplacePatternViewModel.Title, "The following error occurred:", argumentEx);

                        this.IsError = true;
                    }
                }
            );

        public RelayCommand<Object> ApplyCommand
            => new RelayCommand<Object>
            (
                delegate
                {
                    PreviewCommand.Execute(null);

                    if (this.IsError)
                    {
                        return;
                    }

                    foreach (PatternFileInfo item in this.Files)
                    {
                        item.FileInfo.NewFileName = item.PreviewFileName;
                    }

                    this.Close();
                }
            );

        private void ConvertCase(PatternFileInfo item, Int32 mode)
        {
            switch (CaseConversionPatternViewModel.ModeNames[mode])
            {
                case "TitleCase":
                    {
                        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

                        item.PreviewFileName = textInfo.ToTitleCase(item.FileInfo.OriginalFileName.ToLowerInvariant());

                        break;
                    }
                case "Uppercase":
                    item.PreviewFileName = item.FileInfo.OriginalFileName.ToUpperInvariant();
                    break;
                case "Lowercase":
                    item.PreviewFileName = item.FileInfo.OriginalFileName.ToLowerInvariant();
                    break;
                case "Reverse":
                    {
                        StringBuilder sb = new StringBuilder(item.FileInfo.OriginalFileName);

                        for (Int32 i = 0; i < sb.Length; i++)
                        {
                            if (Char.IsLower(sb[i]))
                            {
                                sb[i] = Char.ToUpperInvariant(sb[i]);
                            }
                            else if (Char.IsUpper(sb[i]))
                            {
                                sb[i] = Char.ToLowerInvariant(sb[i]);
                            }

                            item.PreviewFileName = sb.ToString();
                        }

                        break;
                    }
            }
        }

        public const String Title = "Case Conversion";

        public static readonly String[] ModeNames = new String[]
        {
            "TitleCase",
            "Uppercase",
            "Lowercase",
            "Reverse"
        };
    }
}