using System.Text;

namespace StoryQ.Converter.Wpf.Model.CodeGen
{
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
            for (int i = 0; i < IndentLevel; i++)
            {
                sb.Append(IndentText);
            }
            sb.AppendLine(s);
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}