namespace StoryQ.Converter.Wpf.Model.CodeGen
{
    using System;
    using System.Text;

    /// <summary>
    /// Helper class to print indented code to a stringbuilder
    /// </summary>
    internal class CodeWriter
    {
        private readonly StringBuilder sb = new StringBuilder();

        public CodeWriter()
        {
            this.IndentText = "    ";
        }

        public string IndentText { get; set; }

        public int IndentLevel { get; set; }

        public void WriteLine(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                this.sb.AppendLine();
                return;
            }
            for (int i = 0; i < this.IndentLevel; i++)
            {
                this.sb.Append(this.IndentText);
            }
            this.sb.AppendLine(s);
        }

        public IDisposable IncreaseIndent(int level)
        {
            this.IndentLevel += level;
            return new ActionDisposable(() => this.IndentLevel -= level);
        }

        public IDisposable CodeBlock()
        {
            this.WriteLine("{");
            this.IndentLevel++;
            return new ActionDisposable(() => { this.IndentLevel--; this.WriteLine("}"); });
        }

        public override string ToString() => this.sb.ToString();

        private class ActionDisposable : IDisposable
        {
            private readonly Action action;

            public ActionDisposable(Action action)
            {
                this.action = action;
            }

            public void Dispose()
            {
                this.action();
            }
        }
    }
}