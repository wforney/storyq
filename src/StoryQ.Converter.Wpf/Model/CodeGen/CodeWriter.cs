using System;
using System.Text;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    /// <summary>
    /// Helper class to print indented code to a stringbuilder
    /// </summary>
    internal class CodeWriter
    {
        private readonly StringBuilder sb = new StringBuilder();

        public CodeWriter()
        {
            IndentText = "    ";
        }

        public string IndentText { get; set; }

        public int IndentLevel { get; set; }

        public void WriteLine(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                sb.AppendLine();
                return;
            }
            for (int i = 0; i < IndentLevel; i++)
            {
                sb.Append(IndentText);
            }
            sb.AppendLine(s);
        }

        public IDisposable IncreaseIndent(int level)
        {
            IndentLevel += level;
            return new ActionDisposable(() => IndentLevel -= level);
        }

        public IDisposable CodeBlock()
        {
            WriteLine("{");
            IndentLevel++;
            return new ActionDisposable(() => { IndentLevel--; WriteLine("}"); });
        }

        public override string ToString()
        {
            return sb.ToString();
        }

        class ActionDisposable:IDisposable
        {
            private Action action;

            public ActionDisposable(Action action)
            {
                this.action = action;
            }

            public void Dispose()
            {
                action();
            }
        }
    }
}