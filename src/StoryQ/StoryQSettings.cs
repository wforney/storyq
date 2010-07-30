using System;
using System.Reflection;
using StoryQ.Formatting.Methods;
using StoryQ.Formatting.Parameters;
using StoryQ.Infrastructure;

namespace StoryQ
{
    /// <summary>
    /// This class is used to control global settings within storyQ. Refer to each member's documentation for details.
    /// It is recommended that you override these settings during the test suite setup phase of your test framework's execution, as it only needs to be done once.
    /// </summary>
    public static class StoryQSettings
    {
        static StoryQSettings()
        {
            MethodFormatAttribute inline = new ParametersInlineMethodFormatAttribute();
            MethodFormatAttribute suffixed = new ParameterSuffixedMethodFormatAttribute();
            DefaultMethodFormatSelector = x => x.Name.Contains("_") ? inline : suffixed;

            ParameterFormatAttribute toString = new ToStringParameterFormatAttribute();
            DefaultParameterFormatter = x => toString;

            PendingExceptionBuilder = ExceptionHelper.CreateExceptionBuilder();
        }

        ///<summary>
        /// This function can be used to override the default formatting behaviour of StoryQ.
        /// If you don't override this, a selector will be provided that uses ParametersInlineMethodFormatAttribute if there are any underscores present, and ParameterSuffixedMethodFormatAttribute otherwise.
        /// If you have your own method formatter that you want to use globally, then you should set this property to a function that always returns an instance of your formatter.
        ///</summary>
        public static Func<MethodInfo, MethodFormatAttribute> DefaultMethodFormatSelector { get; set; } 
        
        ///<summary>
        /// This function can be used to override the default formatting behaviour of StoryQ.
        /// If you don't override this, a selector will be provided that uses ParametersInlineMethodFormatAttribute if there are any underscores present, and ParameterSuffixedMethodFormatAttribute otherwise.
        /// If you have your own method formatter that you want to use globally, then you should set this property to a function that always returns an instance of your formatter.
        ///</summary>
        public static Func<ParameterInfo, ParameterFormatAttribute> DefaultParameterFormatter { get; set; }

        /// <summary>
        /// This Func is responsible for wrapping an exception up in a unit test framework specific "pending" exception
        /// If you don't set the func manually, it will try to reflectively build an appropriate Func for itself. If this fails, 
        /// you need to set the PendingExceptionBuilder manually
        /// <example>
        /// StoryQSettings.PendingExceptionBuilder = (message, inner) => new PendingException(message, inner);
        /// </example>
        /// </summary>
        public static Func<string, Exception, Exception> PendingExceptionBuilder { get; set; }

        /// <summary>
        /// If you set this to true, then StoryQ's ExecuteWithReport method will generate a non-interactive html page that works 
        /// well in older browsers like IE6. If your entire team has access to a better browser than IE6, it's best to leave this "false"
        /// </summary>
        public static bool ReportSupportsLegacyBrowsers { get; set; }
    }
}
