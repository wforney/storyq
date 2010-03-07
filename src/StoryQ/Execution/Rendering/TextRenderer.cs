using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StoryQ.Execution.Rendering
{
    internal class TextRenderer : IRenderer
    {
        readonly TextWriter output;

        public TextRenderer(TextWriter output)
        {
            this.output = output;
        }

        public void Render(IEnumerable<Result> results)
        {
            List<Exception> exceptionTable = results.Where(x => x.Type == ResultType.Failed).Select(x => x.Exception).ToList();

            var messages = results.Select(r => new
                            {
                                Result = r,
                                Description = new string(' ', 2 * r.IndentLevel) + r.Prefix + " " + r.Text
                            }).ToList();

            int messageLength = messages.Max(x => x.Description.Length);

            foreach (var m in messages)
            {
                Result r = m.Result;

                if (shouldPutNewlineBefore(r))
                {
                    output.WriteLine();
                }

                output.Write(m.Description);

                if (r.Type != ResultType.NotExecutable)
                {
                    //padding
                    output.Write(new string(' ', messageLength - m.Description.Length));
                    output.Write(" => ");
                    output.Write(r.Type);
                    if(r.Type == ResultType.Pending)
                    {
                        output.Write(" !!");
                    }
                    else if (r.Type == ResultType.Failed)
                    {
                        output.Write(": \"");
                        output.Write(r.Exception.Message);
                        output.Write(" [");
                        output.Write(exceptionTable.IndexOf(r.Exception) + 1);
                        output.Write("]\"");
                    }
                }

                var tags = string.Join(", ", r.Tags.Select(x => "#" + x).ToArray());
                if(!string.IsNullOrEmpty(tags))
                {
                    output.Write(" (");
                    output.Write(tags);
                    output.Write(")");
                }


                output.WriteLine();
            }

            if (exceptionTable.Count > 0)
            {
                output.WriteLine();
                output.WriteLine("_______________________");
                output.WriteLine("Full exception details:");
                output.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");

                for (int i = 0; i < exceptionTable.Count; i++)
                {
                    output.WriteLine("[{0}]: {1}", (i + 1), exceptionTable[i]);
                    output.WriteLine();
                }
            }
        }

        protected virtual bool shouldPutNewlineBefore(Result r)
        {
            return r.IndentLevel == 3;
        }
    }
}
