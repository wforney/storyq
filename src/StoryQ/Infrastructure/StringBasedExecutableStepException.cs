namespace StoryQ.Infrastructure
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Thrown from a string based executable step
    /// </summary>
    public class StringBasedExecutableStepException : NotImplementedException
    {
        private readonly string stepText;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringBasedExecutableStepException"/> class.
        /// </summary>
        /// <param name="stepText">The step text.</param>
        public StringBasedExecutableStepException(string stepText)
        {
            this.stepText = stepText;
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
        /// </PermissionSet>
        public override string ToString()
        {
            const string Template = @"(string used for executable step)

You can choose to implement this '{0}' step with:


{2}
public void {1}()
{{
    throw new NotImplementedException();
}}


";

            var safeStepText = Regex.Replace(this.stepText, "[^a-zA-Z0-9_ ]", string.Empty);
            var attribute = safeStepText == this.stepText ? string.Empty : "[OverrideMethodFormat(" + this.stepText + ")]";
            return string.Format(Template, this.stepText, safeStepText.Camel(), attribute);
        }
    }
}