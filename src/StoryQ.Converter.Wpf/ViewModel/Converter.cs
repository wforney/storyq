using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using StoryQ.Converter.Wpf.Model;
using StoryQ.Converter.Wpf.Model.CodeGen;

namespace StoryQ.Converter.Wpf.ViewModel
{
    public class Converter : ViewModelBase
    {
        public event EventHandler TransitionApplied;

        private string plainText = "";
        private string convertedText;
        private bool outputAnyIndent = true;
        private int initialIndent;

        public Converter()
        {
            Transitions = new ObservableCollection<Transition>();
            Convert();
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

        public bool OutputAnyIndent
        {
            get
            {
                return outputAnyIndent;
            }
            set
            {
                outputAnyIndent = value;
                FirePropertyChanged("OutputAnyIndent");
                Convert();
            }
        }

        public int InitialIndent
        {
            get
            {
                return initialIndent;
            }
            set
            {
                initialIndent = value;
                FirePropertyChanged("InitialIndent");
                Convert();
            }
        }

        private void Convert()
        {
            try
            {
                StoryStarter root = new StoryStarter();
                var lines = PlainText
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries).First());

                object parsed = Parser.Parse(lines, root);
                if (parsed is FragmentBase)
                {
                    ConvertedText = Code((FragmentBase)parsed);
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

        private string Code(FragmentBase b)
        {
            CodeWriter writer = new CodeWriter
                {
                    IndentLevel = InitialIndent, 
                };

            ICodeGenerator generator = new StoryMethodGenerator(new StoryCodeGenerator(OutputAnyIndent));
            generator.Generate(b, writer);
            return writer.ToString();
        }

        internal void ApplyTransition(MethodInfo info)
        {
            var s = PlainText.Trim();
            if (s != "")
            {
                s += Environment.NewLine;
            }
            s += Parser.UnCamel(info.Name) + " ";
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
