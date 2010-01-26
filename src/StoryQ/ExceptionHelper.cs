using System;
using System.Linq.Expressions;
using System.Reflection;

using StoryQ.Execution;
using StoryQ.Properties;
namespace StoryQ
{
    /// <summary>
    /// Exception-related operations for StoryQ.
    /// </summary>
    public static class ExceptionHelper
    {
        private static Func<string, Exception, Exception> exceptionBuilder;

        /// <summary>
        /// This Func is responsible for wrapping an exception up in a unit test framework specific "pending" exception
        /// If you don't set the func manually, it will try to reflectively build an appropriate Func for itself. If this fails, 
        /// you need to set the PendingExceptionBuilder manually
        /// <example>
        /// ExceptionHelper.PendingExceptionBuilder = (message, inner) => new PendingException(message, inner);
        /// </example>
        /// </summary>
        public static Func<string, Exception, Exception> PendingExceptionBuilder
        {
            get { return exceptionBuilder ?? (exceptionBuilder = CreateExceptionBuilder()); }
            set { exceptionBuilder = value; }
        }

        /// <summary>
        /// Tries to build an "exception builder" by scanning through the list of configured "pending exceptions" and creating a 
        /// Func out of the constructor
        /// </summary>
        /// <returns></returns>
        private static Func<string, Exception, Exception> CreateExceptionBuilder()
        {
            foreach (string s in Settings.Default.IgnoreExceptionType)
            {
                string[] split = s.Split(',');
                if (split.Length == 2)
                {
                    Type type = Type.GetType(s);
                    if (type != null)
                    {
                        var c = type.GetConstructor(new[] { typeof(string), typeof(Exception) });
                        if (c != null)
                        {
                            var pm = Expression.Parameter(typeof(string), "message");
                            var pe = Expression.Parameter(typeof(Exception), "exception");
                            var e = Expression.New(c, pm, pe);
                            return Expression.Lambda<Func<string, Exception, Exception>>(e, pm, pe).Compile();
                        }
                    }
                }
            }
            return (x, y) => new NotSupportedException("You need to set StoryQ.ExceptionHelper.PendingExceptionBuilder manually in your test setup. See the property's documentation for more info");
        }

        /// <summary>
        /// Uses forbidden .Net information to try and force the exception to keep its stack trace, even when rethrown
        /// http://stackoverflow.com/questions/1009762/how-can-i-rethrow-an-inner-exception-while-maintaining-the-stack-trace-generated/1009888#1009888
        /// </summary>
        /// <param name="ex">The exception to hack</param>
        /// <param name="separator">the message to put between the real stack trace and the new one</param>
        internal static void TryForceStackTracePermanence(Exception ex, string separator)
        {
            try
            {
                FieldInfo remoteStackTraceString = typeof(Exception).GetField("_remoteStackTraceString", BindingFlags.Instance | BindingFlags.NonPublic);
                if (remoteStackTraceString != null)
                {
                    string nl = Environment.NewLine;
                    remoteStackTraceString.SetValue(ex, ex.StackTrace + nl + nl + separator + nl + nl);
                }
            }
            catch
            {
                Console.Out.WriteLine("Failed to save exception stack trace for " + ex);
            }
        }
    }
}