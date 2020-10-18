namespace StoryQ.Infrastructure
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Exception-related operations for StoryQ.
    /// </summary>
    internal static class ExceptionHelper
    {
        /// <summary>
        /// Tries to build an "exception builder" by scanning through the list of configured
        /// "pending exceptions" and creating a Func out of the constructor
        /// </summary>
        /// <returns>Func&lt;System.String, Exception, Exception&gt;.</returns>
        internal static Func<string, Exception, Exception> CreateExceptionBuilder()
        {
            // TODO: add xunit and mbunit
            var config = new[]
            {
                new
                {
                    Assembly="Microsoft.VisualStudio.QualityTools.UnitTestFramework",
                    Class="Microsoft.VisualStudio.TestTools.UnitTesting.AssertInconclusiveException",
                },
                new
                {
                    Assembly="nunit.framework",
                    Class="NUnit.Framework.IgnoreException",
                },
            };

            foreach (var v in config)
            {
                var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == v.Assembly);
                if (assembly is null)
                {
                    continue;
                }

                var type = assembly.GetType(v.Class);
                if (type is null)
                {
                    continue;
                }

                var c = type.GetConstructor(new[] { typeof(string), typeof(Exception) });
                if (c is null)
                {
                    continue;
                }

                var pm = Expression.Parameter(typeof(string), "message");
                var pe = Expression.Parameter(typeof(Exception), "exception");
                var e = Expression.New(c, pm, pe);
                return Expression.Lambda<Func<string, Exception, Exception>>(e, pm, pe).Compile();
            }

            return (x, y) =>
                new NotSupportedException(
                    "You need to set StoryQ.ExceptionHelper.PendingExceptionBuilder manually in your test setup. See the property's documentation for more info");
        }


        /// <summary>
        /// Uses forbidden .Net information to try and force the exception to keep its stack trace,
        /// even when rethrown http://stackoverflow.com/questions/1009762/how-can-i-rethrow-an-inner-exception-while-maintaining-the-stack-trace-generated/1009888#1009888
        /// </summary>
        /// <param name="ex">The exception to hack</param>
        /// <param name="separator">
        /// the message to put between the real stack trace and the new one
        /// </param>
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Because.")]
        internal static void TryForceStackTracePermanence(Exception ex, string separator)
        {
            try
            {
                var remoteStackTraceString =
                    typeof(Exception).GetField(
                        "_remoteStackTraceString",
                        BindingFlags.Instance | BindingFlags.NonPublic);

                if (remoteStackTraceString != null)
                {
                    var nl = Environment.NewLine;
                    remoteStackTraceString.SetValue(ex, ex.StackTrace + nl + nl + separator + nl + nl);
                }
            }
            catch
            {
                Console.Out.WriteLine($"Failed to save exception stack trace for {ex}");
            }
        }
    }
}
