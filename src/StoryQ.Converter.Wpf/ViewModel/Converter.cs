using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using StoryQ.Converter.Wpf.Model;

namespace StoryQ.Converter.Wpf.ViewModel
{
    public class Converter : ViewModelBase
    {
        public event EventHandler TransitionApplied;


        private string plainText = "";
        private string convertedText;
        private bool castMethodsToActions;
        private bool outputExecutablesAsStrings;
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

        public bool CastMethodsToActions
        {
            get
            {
                return castMethodsToActions;
            }
            set
            {
                castMethodsToActions = value;
                FirePropertyChanged("CastMethodsToActions");
                Convert();
            }
        }

        public bool OutputExecutablesAsStrings
        {
            get
            {
                return outputExecutablesAsStrings;
            }
            set
            {
                outputExecutablesAsStrings = value;
                FirePropertyChanged("OutputExecutablesAsStrings");
                Convert();
            }
        }

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
                    .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Split(new[] {"=>"}, StringSplitOptions.RemoveEmptyEntries).First());

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
                foreach (MethodInfo info in Parser.GetOneStringMethods(parsed))
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
            var narratives = b.SelfAndAncestors().Select(x => x.Narrative).Reverse();

            var v = HeadAndTail(narratives,
                n => Indent(InitialIndent) + string.Format("new Story({0})", CamelContent(n)),
                n => Indent(InitialIndent + n.IndentLevel) + string.Format(".{0}({1})", camel(n.Prefix), CamelContent(n)));

            return string.Join(Environment.NewLine, v.ToArray()) + ";";
        }

        private string Indent(int level)
        {
            return OutputAnyIndent ? new string(' ', level * 2) : "";
        }

        private static IEnumerable<TResult> HeadAndTail<TResult, TSource>(IEnumerable<TSource> e, Func<TSource, TResult> headSelector, Func<TSource, TResult> tailSelector)
        {
            var enumerator = e.GetEnumerator();
            if (enumerator.MoveNext())
            {
                yield return headSelector(enumerator.Current);
            }
            while (enumerator.MoveNext())
            {
                yield return tailSelector(enumerator.Current);
            }
        }

        private string CamelContent(Narrative n)
        {
            if (n.IsExecutable && !OutputExecutablesAsStrings)
            {
                string s = n.Text;
                List<string> args = new List<string>();
                s = Regex.Replace(s, "\\$\\S*", match =>
                                                  {
                                                      args.Add(match.Value.Substring(1));
                                                      return "_";
                                                  });
                var v = args.Select(x => Regex.IsMatch(x, "^((true)|(false)|([0-9.]*))$") ? x : '"' + x + '"');
                s = camel(s);
                if (CastMethodsToActions && !args.Any())
                {
                    s = "(Action)" + s;
                }
                string[] strings = new[] { s };
                strings = strings.Concat(v).ToArray();

                return string.Join(", ", strings);
            }
            return string.Format("\"{0}\"", n.Text);
        }

        private string camel(string s)
        {
            return Regex.Replace(" " + s, " \\w|_", match => match.Value.Trim().ToUpperInvariant());
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
            if (h != null) h(this, EventArgs.Empty);
        }

    }
}
