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

namespace StoryQ.Converter.Wpf.ViewModel
{
    public class Converter : ViewModelBase
    {
        readonly IFileSavingService fileSavingService;
        public event EventHandler TransitionApplied;

        private string plainText = "";
        private string convertedText;

        private ConversionSettings settings;

        public Converter():this(ServiceLocator.Resolve<IFileSavingService>()){}

        public Converter(IFileSavingService fileSavingService)
        {
            this.fileSavingService = fileSavingService;
            Transitions = new ObservableCollection<Transition>();
            Settings = new ConversionSettings();
            SaveLibrariesCommand = new DelegateCommand(SaveLibraries);
            Convert();
        }

        void SaveLibraries()
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
                if(settings != null)
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

        private void SettingsOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            Convert();
        }

        private void Convert()
        {
            try
            {
                object parsed = ParseText(PlainText);
                if (parsed is IStepContainer)
                {
                    ConvertedText = Code((IStepContainer)parsed);
                }
                else
                {
                    ConvertedText = "Awaiting content";
                }


                Transitions.Clear();
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

        private static object ParseText(string text)
        {
            //ignore anything that starts with =>

            var lines = text
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries).First())
                .Select(x=>Regex.Replace(x, "\\s+", " ").Trim());

            return Parser.Parse(lines, new StoryStarter());
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
