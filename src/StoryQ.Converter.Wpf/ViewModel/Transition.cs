using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;

using StoryQ.Converter.Wpf.Model;
using StoryQ.Infrastructure;

namespace StoryQ.Converter.Wpf.ViewModel
{
    public class Transition : ViewModelBase
    {

        private string text;
        private string description;

        public Transition(MethodInfo method, Converter parent)
        {
            Apply = new DelegateCommand(() => parent.ApplyTransition(method));

            string s = method.Name.UnCamel();
            Text = s.Insert(GetUniqueCharIndex(s, parent.Transitions.Select(x=>x.Text)), "_");
            DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(method, typeof(DescriptionAttribute));
            if (attribute != null)
            {
                Description = attribute.Description;
            }
        }

        private static int GetUniqueCharIndex(string s, IEnumerable<string> others)
        {
            var taken = others.Select(x => x.SkipWhile(c => c != '_').Skip(1).FirstOrDefault());
            char first = s.Except(taken).FirstOrDefault();
            int i = s.IndexOf(first);
            return Math.Max(0, i);
        }

        public ICommand Apply { get; private set; }

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                FirePropertyChanged("Text");
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                FirePropertyChanged("Description");
            }
        }
    }
}