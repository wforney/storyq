namespace StoryQ.Converter.Wpf.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using StoryQ.Converter.Wpf.Model;
    using StoryQ.Converter.Wpf.Model.CodeGen;
    using StoryQ.Converter.Wpf.Services;
    using StoryQ.Infrastructure;

    public class Converter : ViewModelBase
    {
        private readonly IFileSavingService fileSavingService;
        private readonly ILanguagePackProvider languagePackProvider;
        public event EventHandler TransitionApplied;

        private string plainText = "";
        private string convertedText;

        private ConversionSettings settings;
        private LanguagePack currentLanguagePack;
        private object currentParserEntryPoint;

        public Converter() : this(ServiceLocator.Resolve<IFileSavingService>(), ServiceLocator.Resolve<ILanguagePackProvider>()) { }

        public Converter(IFileSavingService fileSavingService, ILanguagePackProvider languagePackProvider)
        {
            this.fileSavingService = fileSavingService;
            this.languagePackProvider = languagePackProvider;
            Transitions = new ObservableCollection<Transition>();
            Settings = new ConversionSettings();
            SaveLibrariesCommand = new DelegateCommand(SaveLibraries);

            LanguagePacks = new ObservableCollection<LanguagePack>();

            foreach (var pack in languagePackProvider.GetLocalLanguagePacks())
            {
                LanguagePacks.Add(new LanguagePack(this, pack));
            }

            CurrentLanguagePack = LanguagePacks.First();

            foreach (var pack in languagePackProvider.GetRemoteLanguagePacks())
            {
                LanguagePacks.Add(new LanguagePack(this, pack));
            }


            Convert();
        }

        public LanguagePack CurrentLanguagePack
        {
            get
            {
                return currentLanguagePack;
            }
            set
            {
                currentLanguagePack = value;
                currentLanguagePack.SetCurrent();
                FirePropertyChanged("CurrentLanguagePack");
            }
        }

        private void SaveLibraries()
        {
            string directory = fileSavingService.PromptForDirectory("Where you would like to save the StoryQ files?");
            if (directory != null)
            {
                fileSavingService.CopyLibFiles(directory);
            }
        }

        public string PlainText
        {
            get
            {
                return plainText;
            }
            set
            {
                plainText = value;
                FirePropertyChanged("PlainText");
                Convert();
            }
        }


        public string ConvertedText
        {
            get
            {
                return convertedText;
            }
            set
            {
                convertedText = value;
                FirePropertyChanged("ConvertedText");
            }
        }

        public ObservableCollection<Transition> Transitions { get; private set; }

        public ConversionSettings Settings
        {
            get
            {
                return settings;
            }
            set
            {
                if (settings != null)
                {
                    settings.PropertyChanged -= SettingsOnPropertyChanged;
                }
                settings = value;
                if (settings != null)
                {
                    settings.PropertyChanged += SettingsOnPropertyChanged;
                }
                Convert();
                FirePropertyChanged("Settings");
            }
        }

        public ICommand SaveLibrariesCommand { get; private set; }

        public ObservableCollection<LanguagePack> LanguagePacks { get; set; }

        public object CurrentParserEntryPoint
        {
            get
            {
                return currentParserEntryPoint;
            }
            internal set
            {
                currentParserEntryPoint = value;
                Convert();
            }
        }

        private void SettingsOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            Convert();
        }

        private void Convert()
        {
            Transitions.Clear();

            if (CurrentParserEntryPoint == null)
            {
                ConvertedText = "Please wait while your language pack is retrieved";
                return;
            }

            try
            {
                //ignore anything after "=>", make all whitespace a single space
                var lines = PlainText
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries).First())
                    .Select(x => Regex.Replace(x, "\\s+", " ").Trim());

                //todo

                object parsed = Parser.Parse(lines, CurrentParserEntryPoint);
                if (parsed is IStepContainer)
                {
                    ConvertedText = Code((IStepContainer)parsed);
                }
                else
                {
                    ConvertedText = "Awaiting content";
                }


                foreach (MethodInfo info in Parser.GetMethods(parsed))
                {
                    Transitions.Add(new Transition(info, this));
                }
            }
            catch (Exception ex)
            {
                ConvertedText = ex.Message;
            }
        }

        private string Code(IStepContainer b)
        {
            CodeWriter writer = new CodeWriter();
            Settings.GetCodeGenerator().Generate(b.SelfAndAncestors().Reverse().ToList(), writer);
            return writer.ToString();
        }

        internal void ApplyTransition(MethodInfo info)
        {
            var s = PlainText.Trim();
            if (s != "")
            {
                s += Environment.NewLine;
            }
            s += info.Name.UnCamel() + " ";
            PlainText = s;

            Transitions.Clear();
            InvokeTransitionApplied();
        }

        private void InvokeTransitionApplied()
        {
            EventHandler h = TransitionApplied;
            if (h != null)
            {
                h(this, EventArgs.Empty);
            }
        }

    }
}
