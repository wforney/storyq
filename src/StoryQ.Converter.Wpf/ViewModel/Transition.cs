namespace StoryQ.Converter.Wpf.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Input;
    using StoryQ.Infrastructure;

    public class Transition : ViewModelBase
    {

        private string text;
        private string description;

        public Transition(MethodInfo method, Converter parent)
        {
            this.Apply = new DelegateCommand(() => parent.ApplyTransition(method));

            string s = method.Name.UnCamel();
            this.Text = s.Insert(GetUniqueCharIndex(s, parent.Transitions.Select(x => x.Text)), "_");
            DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(method, typeof(DescriptionAttribute));
            if (attribute != null)
            {
                this.Description = attribute.Description;
            }
        }

        private static int GetUniqueCharIndex(string s, IEnumerable<string> others)
        {
            var taken = others.Select(x => x.SkipWhile(c => c != '_').Skip(1).FirstOrDefault());
            var first = s.Except(taken).FirstOrDefault();
            var i = s.IndexOf(first);
            return Math.Max(0, i);
        }

        public ICommand Apply { get; private set; }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
                this.FirePropertyChanged("Text");
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
                this.FirePropertyChanged("Description");
            }
        }
    }
}