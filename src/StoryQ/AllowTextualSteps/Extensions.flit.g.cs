using System;
using System.ComponentModel;

using StoryQ.Infrastructure;

namespace StoryQ.AllowTextualSteps
{
	public static class Extensions
	{
        /// <summary>
        /// Given [Condition].
        /// Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment should be executable, but you are choosing to supply a string for now. The step will Pend.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state")]        
        public static Condition Given(this Scenario parent, string text)
        {
			string message = @"implement your textual method with:
			
public void "+(text.Camel())+@"()
{
	throw new NotImplementedException();
}

";
            Step s = new Step("Given", 4, text, ()=>{throw new StepIsTextException(message);});
            return new Condition(s, parent);
        }

        /// <summary>
        /// And [Condition].
        /// Provide another precondition to describe our scenario's initial state
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment should be executable, but you are choosing to supply a string for now. The step will Pend.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide another precondition to describe our scenario's initial state")]        
        public static Condition And(this Condition parent, string text)
        {
			string message = @"implement your textual method with:
			
public void "+(text.Camel())+@"()
{
	throw new NotImplementedException();
}

";
            Step s = new Step("And", 5, text, ()=>{throw new StepIsTextException(message);});
            return new Condition(s, parent);
        }

        /// <summary>
        /// When [Operation].
        /// Describe the actions that are done to the system under test. '
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment should be executable, but you are choosing to supply a string for now. The step will Pend.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Describe the actions that are done to the system under test. '")]        
        public static Operation When(this Condition parent, string text)
        {
			string message = @"implement your textual method with:
			
public void "+(text.Camel())+@"()
{
	throw new NotImplementedException();
}

";
            Step s = new Step("When", 4, text, ()=>{throw new StepIsTextException(message);});
            return new Operation(s, parent);
        }

        /// <summary>
        /// And [Operation].
        /// Provide another action that is to be performed on the system, prior to our check for behaviour ('then')
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment should be executable, but you are choosing to supply a string for now. The step will Pend.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Provide another action that is to be performed on the system, prior to our check for behaviour ('then')")]        
        public static Operation And(this Operation parent, string text)
        {
			string message = @"implement your textual method with:
			
public void "+(text.Camel())+@"()
{
	throw new NotImplementedException();
}

";
            Step s = new Step("And", 5, text, ()=>{throw new StepIsTextException(message);});
            return new Operation(s, parent);
        }

        /// <summary>
        /// Then [Outcome].
        /// Describe the system's behaviour that the prior state and actions should elicit
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment should be executable, but you are choosing to supply a string for now. The step will Pend.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Describe the system's behaviour that the prior state and actions should elicit")]        
        public static Outcome Then(this Operation parent, string text)
        {
			string message = @"implement your textual method with:
			
public void "+(text.Camel())+@"()
{
	throw new NotImplementedException();
}

";
            Step s = new Step("Then", 4, text, ()=>{throw new StepIsTextException(message);});
            return new Outcome(s, parent);
        }

        /// <summary>
        /// And [Outcome].
        /// Provide another resultant behaviour to check for
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment should be executable, but you are choosing to supply a string for now. The step will Pend.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Provide another resultant behaviour to check for")]        
        public static Outcome And(this Outcome parent, string text)
        {
			string message = @"implement your textual method with:
			
public void "+(text.Camel())+@"()
{
	throw new NotImplementedException();
}

";
            Step s = new Step("And", 5, text, ()=>{throw new StepIsTextException(message);});
            return new Outcome(s, parent);
        }

	}
	
	public class StepIsTextException : NotImplementedException
    {
        public StepIsTextException(string message):base(message)
        {
        }
    }
}


