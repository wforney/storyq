using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using StoryQ.Infrastructure;

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
            StringWriter buffer = new StringWriter();
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
                    buffer.WriteLine();
                }

                buffer.Write(m.Description);

                if (r.Type != ResultType.NotExecutable)
                {
                    //padding
                    buffer.Write(new string(' ', messageLength - m.Description.Length));
                    buffer.Write(" => ");
                    buffer.Write(r.Type);
                    if(r.Type == ResultType.Pending)
                    {
                        buffer.Write(" !!");
                    }
                    else if (r.Type == ResultType.Failed)
                    {
                        buffer.Write(": \"");
                        buffer.Write(r.Exception.Message);
                        buffer.Write(" [");
                        buffer.Write(exceptionTable.IndexOf(r.Exception) + 1);
                        buffer.Write("]\"");
                    }
                }

                var tags = r.Tags.Select(x => "#" + x).Join(", ");
                if(!string.IsNullOrEmpty(tags))
                {
                    buffer.Write(" => (");
                    buffer.Write(tags);
                    buffer.Write(")");
                }


                buffer.WriteLine();
            }

            if (exceptionTable.Count > 0)
            {
                buffer.WriteLine();
                buffer.WriteLine("_______________________");
                buffer.WriteLine("Full exception details:");
                buffer.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");

                for (int i = 0; i < exceptionTable.Count; i++)
                {
                    buffer.WriteLine("[{0}]: {1}", (i + 1), exceptionTable[i]);
                    buffer.WriteLine();
                }
            }

            output.Write(buffer.ToString());
        }

        protected virtual bool shouldPutNewlineBefore(Result r)
        {
            return r.IndentLevel == 3;
        }
    }
}
