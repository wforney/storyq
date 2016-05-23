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
            this.Transitions = new ObservableCollection<Transition>();
            this.Settings = new ConversionSettings();
            this.SaveLibrariesCommand = new DelegateCommand(this.SaveLibraries);

            this.LanguagePacks = new ObservableCollection<LanguagePack>();

            foreach (var pack in languagePackProvider.GetLocalLanguagePacks())
            {
                this.LanguagePacks.Add(new LanguagePack(this, pack));
            }

            this.CurrentLanguagePack = this.LanguagePacks.First();

            foreach (var pack in languagePackProvider.GetRemoteLanguagePacks())
            {
                this.LanguagePacks.Add(new LanguagePack(this, pack));
            }


            this.Convert();
        }

        public LanguagePack CurrentLanguagePack
        {
            get
            {
                return this.currentLanguagePack;
            }
            set
            {
                this.currentLanguagePack = value;
                this.currentLanguagePack.SetCurrent();
                this.FirePropertyChanged("CurrentLanguagePack");
            }
        }

        private void SaveLibraries()
        {
            string directory = this.fileSavingService.PromptForDirectory("Where you would like to save the StoryQ files?");
            if (directory != null)
            {
                this.fileSavingService.CopyLibFiles(directory);
            }
        }

        public string PlainText
        {
            get
            {
                return this.plainText;
            }
            set
            {
                this.plainText = value;
                this.FirePropertyChanged("PlainText");
                this.Convert();
            }
        }


        public string ConvertedText
        {
            get
            {
                return this.convertedText;
            }
            set
            {
                this.convertedText = value;
                this.FirePropertyChanged("ConvertedText");
            }
        }

        public ObservableCollection<Transition> Transitions { get; private set; }

        public ConversionSettings Settings
        {
            get
            {
                return this.settings;
            }
            set
            {
                if (this.settings != null)
                {
                    this.settings.PropertyChanged -= this.SettingsOnPropertyChanged;
                }
                this.settings = value;
                if (this.settings != null)
                {
                    this.settings.PropertyChanged += this.SettingsOnPropertyChanged;
                }
                this.Convert();
                this.FirePropertyChanged("Settings");
            }
        }

        public ICommand SaveLibrariesCommand { get; private set; }

        public ObservableCollection<LanguagePack> LanguagePacks { get; set; }

        public object CurrentParserEntryPoint
        {
            get
            {
                return this.currentParserEntryPoint;
            }
            internal set
            {
                this.currentParserEntryPoint = value;
                this.Convert();
            }
        }

        private void SettingsOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            this.Convert();
        }

        private void Convert()
        {
            this.Transitions.Clear();

            if (this.CurrentParserEntryPoint == null)
            {
                this.ConvertedText = "Please wait while your language pack is retrieved";
                return;
            }

            try
            {
                // ignore anything after "=>", make all whitespace a single space
                var lines = this.PlainText
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries).First())
                    .Select(x => Regex.Replace(x, "\\s+", " ").Trim());

                // todo

                object parsed = Parser.Parse(lines, this.CurrentParserEntryPoint);
                if (parsed is IStepContainer)
                {
                    this.ConvertedText = this.Code((IStepContainer)parsed);
                }
                else
                {
                    this.ConvertedText = "Awaiting content";
                }


                foreach (MethodInfo info in Parser.GetMethods(parsed))
                {
                    this.Transitions.Add(new Transition(info, this));
                }
            }
            catch (Exception ex)
            {
                this.ConvertedText = ex.Message;
            }
        }

        private string Code(IStepContainer b)
        {
            CodeWriter writer = new CodeWriter();
            this.Settings.GetCodeGenerator().Generate(b.SelfAndAncestors().Reverse().ToList(), writer);
            return writer.ToString();
        }

        internal void ApplyTransition(MethodInfo info)
        {
            var s = this.PlainText.Trim();
            if (s != "")
            {
                s += Environment.NewLine;
            }
            s += info.Name.UnCamel() + " ";
            this.PlainText = s;

            this.Transitions.Clear();
            this.InvokeTransitionApplied();
        }

        private void InvokeTransitionApplied()
        {
            EventHandler h = this.TransitionApplied;
            if (h != null)
            {
                h(this, EventArgs.Empty);
            }
        }

    }
}
