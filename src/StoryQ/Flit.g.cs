using System;
using System.ComponentModel;

namespace StoryQ
{

    /// <summary>
    /// The [Story] story fragment.
    /// This is the root item of any story
    /// <h1>Transitions:</h1><ul>
    /// <li>In order to [<see cref="Benefit"/>]: <see cref="InOrderTo(string)"/></li>
    /// </ul>
    /// </summary>
    public partial class Story : FragmentBase
    {
        /// <summary>
        /// Starts a new StoryQ Story. 
        /// </summary>
        /// <param name="text">The name of the new story</param>
        public Story(string text):base(new Narrative("Story is", 0, text, Narrative.DoNothing)){}

        /// <summary>
        /// <em>In order to [Benefit]</em> <br/>
        /// Describe the real-world value for this story. What is the business process that the user requires software support from?
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Benefit"/></returns>
        [Description("Describe the real-world value for this story. What is the business process that the user requires software support from?")]
        public Benefit InOrderTo(string text)
        {
            Narrative n = new Narrative("In order to", 1, text, Narrative.DoNothing);
            return new Benefit(n){ Parent = this };
        }
    }

    /// <summary>
    /// The [Benefit] story fragment.
    /// The real-world objective (business value) of a story
    /// <h1>Transitions:</h1><ul>
    /// <li>And [<see cref="Benefit"/>]: <see cref="And(string)"/></li>
    /// <li>As a [<see cref="Role"/>]: <see cref="AsA(string)"/></li>
    /// </ul>
    /// </summary>
    public partial class Benefit : FragmentBase
    {
        internal Benefit(Narrative narrative):base(narrative){}

        /// <summary>
        /// <em>And [Benefit]</em> <br/>
        /// Describe any secondary business functions that this story will support
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Benefit"/></returns>
        [Description("Describe any secondary business functions that this story will support")]
        public Benefit And(string text)
        {
            Narrative n = new Narrative("And", 2, text, Narrative.DoNothing);
            return new Benefit(n){ Parent = this };
        }

        /// <summary>
        /// <em>As a [Role]</em> <br/>
        /// The role of the person who is the intended user of this feature
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Role"/></returns>
        [Description("The role of the person who is the intended user of this feature")]
        public Role AsA(string text)
        {
            Narrative n = new Narrative("As a", 1, text, Narrative.DoNothing);
            return new Role(n){ Parent = this };
        }
    }

    /// <summary>
    /// The [Role] story fragment.
    /// The role (a category of actors/users) or roles that receive this benefit. 
    /// <h1>Transitions:</h1><ul>
    /// <li>Or as a [<see cref="Role"/>]: <see cref="OrAsA(string)"/></li>
    /// <li>I want [<see cref="Feature"/>]: <see cref="IWant(string)"/></li>
    /// </ul>
    /// </summary>
    public partial class Role : FragmentBase
    {
        internal Role(Narrative narrative):base(narrative){}

        /// <summary>
        /// <em>Or as a [Role]</em> <br/>
        /// Any other roles that may use this story
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Role"/></returns>
        [Description("Any other roles that may use this story")]
        public Role OrAsA(string text)
        {
            Narrative n = new Narrative("Or as a", 2, text, Narrative.DoNothing);
            return new Role(n){ Parent = this };
        }

        /// <summary>
        /// <em>I want [Feature]</em> <br/>
        /// Describe the software process (features) that will support the business requirement
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Feature"/></returns>
        [Description("Describe the software process (features) that will support the business requirement")]
        public Feature IWant(string text)
        {
            Narrative n = new Narrative("I want", 1, text, Narrative.DoNothing);
            return new Feature(n){ Parent = this };
        }
    }

    /// <summary>
    /// The [Feature] story fragment.
    /// The software process that will implement the specified benefit.
    /// <h1>Transitions:</h1><ul>
    /// <li>And [<see cref="Feature"/>]: <see cref="And(string)"/></li>
    /// <li>With scenario [<see cref="Scenario"/>]: <see cref="WithScenario(string)"/></li>
    /// </ul>
    /// </summary>
    public partial class Feature : FragmentBase
    {
        internal Feature(Narrative narrative):base(narrative){}

        /// <summary>
        /// <em>And [Feature]</em> <br/>
        /// Any other features that will implement the desired benefit
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Feature"/></returns>
        [Description("Any other features that will implement the desired benefit")]
        public Feature And(string text)
        {
            Narrative n = new Narrative("And", 2, text, Narrative.DoNothing);
            return new Feature(n){ Parent = this };
        }

        /// <summary>
        /// <em>With scenario [Scenario]</em> <br/>
        /// Add a scenario ('given'/'when'/'then') to this story. Scenarios can be added (and will be run) in sequence. Each scenario should have a short descriptive name.
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Scenario"/></returns>
        [Description("Add a scenario ('given'/'when'/'then') to this story. Scenarios can be added (and will be run) in sequence. Each scenario should have a short descriptive name.")]
        public Scenario WithScenario(string text)
        {
            Narrative n = new Narrative("With scenario", 3, text, Narrative.DoNothing);
            return new Scenario(n){ Parent = this };
        }
    }

    /// <summary>
    /// The [Scenario] story fragment.
    /// The name of each scenario within a story. You can think of each scenario as a chapter in a book.
    /// <h1>Transitions:</h1><ul>
    /// <li>Given [<see cref="Condition"/>]: <see cref="Given(string)"/></li>
    /// </ul>
    /// </summary>
    public partial class Scenario : FragmentBase
    {
        internal Scenario(Narrative narrative):base(narrative){}

        /// <summary>
        /// <em>Given [Condition]</em> <br/>
        /// Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state
        /// <p>This overload uses the text that is passed in, and can execute any action</p>
        /// </summary>
        /// <param name="text">The text of this story fragment</param>
        /// <param name="action">
        /// The code that should be executed to represent this story fragment. 
        /// Because StoryQ doesn't need to infer any text from the action, you can use lambdas and anonymous delegates here
        /// </param>
        [Description("Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state")]
        public Condition Given(string text, Action action)
        {
            Narrative n = new Narrative("Given", 4, text, action);
            return new Condition(n){ Parent = this };
        }

        /// <summary>
        /// <em>Given [Condition]</em> <br/>
        /// Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state")]
        public Condition Given(Action descriptiveAction)
        {
            string text = MethodToText(descriptiveAction);
            Narrative n = new Narrative("Given", 4, text, descriptiveAction);
            return new Condition(n){ Parent = this };
        }

        /// <summary>
        /// <em>Given [Condition]</em> <br/>
        /// Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state")]
        public Condition Given<T1>(Action<T1> descriptiveAction, T1 arg1)
        {
            string text = MethodToText(descriptiveAction, arg1);
            Narrative n = new Narrative("Given", 4, text, () => descriptiveAction(arg1));
            return new Condition(n){ Parent = this };
        }

        /// <summary>
        /// <em>Given [Condition]</em> <br/>
        /// Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state")]
        public Condition Given<T1, T2>(Action<T1, T2> descriptiveAction, T1 arg1, T2 arg2)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2);
            Narrative n = new Narrative("Given", 4, text, () => descriptiveAction(arg1, arg2));
            return new Condition(n){ Parent = this };
        }

        /// <summary>
        /// <em>Given [Condition]</em> <br/>
        /// Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg3">The third argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state")]
        public Condition Given<T1, T2, T3>(Action<T1, T2, T3> descriptiveAction, T1 arg1, T2 arg2, T3 arg3)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2, arg3);
            Narrative n = new Narrative("Given", 4, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Condition(n){ Parent = this };
        }

        /// <summary>
        /// <em>Given [Condition]</em> <br/>
        /// Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg3">The third argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg4">The fourth argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state")]
        public Condition Given<T1, T2, T3, T4>(Action<T1, T2, T3, T4> descriptiveAction, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2, arg3, arg4);
            Narrative n = new Narrative("Given", 4, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Condition(n){ Parent = this };
        }

        /// <summary>
        /// <em>Given [Condition]</em> <br/>
        /// Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment will 'Pend' unless you call .Pass() immediately after this method.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state")]
        public Condition Given(string text)
        {
            Narrative n = new Narrative("Given", 4, text, Narrative.Pend);
            return new Condition(n){ Parent = this };
        }
    }

    /// <summary>
    /// The [Condition] story fragment.
    /// The preconditions that are meant to be present at the beginning of the scenario.
    /// <h1>Transitions:</h1><ul>
    /// <li>And [<see cref="Condition"/>]: <see cref="And(string)"/></li>
    /// <li>When [<see cref="Operation"/>]: <see cref="When(string)"/></li>
    /// </ul>
    /// </summary>
    public partial class Condition : FragmentBase
    {
        internal Condition(Narrative narrative):base(narrative){}

        /// <summary>
        /// <em>And [Condition]</em> <br/>
        /// Provide another precondition to describe our scenario's initial state
        /// <p>This overload uses the text that is passed in, and can execute any action</p>
        /// </summary>
        /// <param name="text">The text of this story fragment</param>
        /// <param name="action">
        /// The code that should be executed to represent this story fragment. 
        /// Because StoryQ doesn't need to infer any text from the action, you can use lambdas and anonymous delegates here
        /// </param>
        [Description("Provide another precondition to describe our scenario's initial state")]
        public Condition And(string text, Action action)
        {
            Narrative n = new Narrative("And", 5, text, action);
            return new Condition(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Condition]</em> <br/>
        /// Provide another precondition to describe our scenario's initial state
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide another precondition to describe our scenario's initial state")]
        public Condition And(Action descriptiveAction)
        {
            string text = MethodToText(descriptiveAction);
            Narrative n = new Narrative("And", 5, text, descriptiveAction);
            return new Condition(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Condition]</em> <br/>
        /// Provide another precondition to describe our scenario's initial state
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide another precondition to describe our scenario's initial state")]
        public Condition And<T1>(Action<T1> descriptiveAction, T1 arg1)
        {
            string text = MethodToText(descriptiveAction, arg1);
            Narrative n = new Narrative("And", 5, text, () => descriptiveAction(arg1));
            return new Condition(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Condition]</em> <br/>
        /// Provide another precondition to describe our scenario's initial state
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide another precondition to describe our scenario's initial state")]
        public Condition And<T1, T2>(Action<T1, T2> descriptiveAction, T1 arg1, T2 arg2)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2);
            Narrative n = new Narrative("And", 5, text, () => descriptiveAction(arg1, arg2));
            return new Condition(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Condition]</em> <br/>
        /// Provide another precondition to describe our scenario's initial state
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg3">The third argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide another precondition to describe our scenario's initial state")]
        public Condition And<T1, T2, T3>(Action<T1, T2, T3> descriptiveAction, T1 arg1, T2 arg2, T3 arg3)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2, arg3);
            Narrative n = new Narrative("And", 5, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Condition(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Condition]</em> <br/>
        /// Provide another precondition to describe our scenario's initial state
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg3">The third argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg4">The fourth argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide another precondition to describe our scenario's initial state")]
        public Condition And<T1, T2, T3, T4>(Action<T1, T2, T3, T4> descriptiveAction, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2, arg3, arg4);
            Narrative n = new Narrative("And", 5, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Condition(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Condition]</em> <br/>
        /// Provide another precondition to describe our scenario's initial state
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment will 'Pend' unless you call .Pass() immediately after this method.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide another precondition to describe our scenario's initial state")]
        public Condition And(string text)
        {
            Narrative n = new Narrative("And", 5, text, Narrative.Pend);
            return new Condition(n){ Parent = this };
        }

        /// <summary>
        /// <em>When [Operation]</em> <br/>
        /// Describe the actions that are done <em>to</em> the system under test. '
        /// <p>This overload uses the text that is passed in, and can execute any action</p>
        /// </summary>
        /// <param name="text">The text of this story fragment</param>
        /// <param name="action">
        /// The code that should be executed to represent this story fragment. 
        /// Because StoryQ doesn't need to infer any text from the action, you can use lambdas and anonymous delegates here
        /// </param>
        [Description("Describe the actions that are done <em>to</em> the system under test. '")]
        public Operation When(string text, Action action)
        {
            Narrative n = new Narrative("When", 4, text, action);
            return new Operation(n){ Parent = this };
        }

        /// <summary>
        /// <em>When [Operation]</em> <br/>
        /// Describe the actions that are done <em>to</em> the system under test. '
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Describe the actions that are done <em>to</em> the system under test. '")]
        public Operation When(Action descriptiveAction)
        {
            string text = MethodToText(descriptiveAction);
            Narrative n = new Narrative("When", 4, text, descriptiveAction);
            return new Operation(n){ Parent = this };
        }

        /// <summary>
        /// <em>When [Operation]</em> <br/>
        /// Describe the actions that are done <em>to</em> the system under test. '
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Describe the actions that are done <em>to</em> the system under test. '")]
        public Operation When<T1>(Action<T1> descriptiveAction, T1 arg1)
        {
            string text = MethodToText(descriptiveAction, arg1);
            Narrative n = new Narrative("When", 4, text, () => descriptiveAction(arg1));
            return new Operation(n){ Parent = this };
        }

        /// <summary>
        /// <em>When [Operation]</em> <br/>
        /// Describe the actions that are done <em>to</em> the system under test. '
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Describe the actions that are done <em>to</em> the system under test. '")]
        public Operation When<T1, T2>(Action<T1, T2> descriptiveAction, T1 arg1, T2 arg2)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2);
            Narrative n = new Narrative("When", 4, text, () => descriptiveAction(arg1, arg2));
            return new Operation(n){ Parent = this };
        }

        /// <summary>
        /// <em>When [Operation]</em> <br/>
        /// Describe the actions that are done <em>to</em> the system under test. '
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg3">The third argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Describe the actions that are done <em>to</em> the system under test. '")]
        public Operation When<T1, T2, T3>(Action<T1, T2, T3> descriptiveAction, T1 arg1, T2 arg2, T3 arg3)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2, arg3);
            Narrative n = new Narrative("When", 4, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Operation(n){ Parent = this };
        }

        /// <summary>
        /// <em>When [Operation]</em> <br/>
        /// Describe the actions that are done <em>to</em> the system under test. '
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg3">The third argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg4">The fourth argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Describe the actions that are done <em>to</em> the system under test. '")]
        public Operation When<T1, T2, T3, T4>(Action<T1, T2, T3, T4> descriptiveAction, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2, arg3, arg4);
            Narrative n = new Narrative("When", 4, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Operation(n){ Parent = this };
        }

        /// <summary>
        /// <em>When [Operation]</em> <br/>
        /// Describe the actions that are done <em>to</em> the system under test. '
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment will 'Pend' unless you call .Pass() immediately after this method.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Describe the actions that are done <em>to</em> the system under test. '")]
        public Operation When(string text)
        {
            Narrative n = new Narrative("When", 4, text, Narrative.Pend);
            return new Operation(n){ Parent = this };
        }

        /// <summary>
        /// Makes this narrative "pend". Use this when the test itself is incomplete.
        /// This is the default for any executable narrative
        /// </summary>
        /// <returns>itself</returns>
        public Condition Pend()
        {
            Narrative.Action = Narrative.Pend;
            return this;
        }
        
        /// <summary>
        /// Makes this narrative pass. Use this when you want to override the existing behaviour 
        /// (such as pending, or running a failing peice of code)
        /// "Pending" is the default for any executable narrative
        /// </summary>
        /// <returns>itself</returns>
        public Condition Pass()
        {
            Narrative.Action = () => {};
            return this;
        }
    }

    /// <summary>
    /// The [Operation] story fragment.
    /// The action(s) that are performed upon the system under test
    /// <h1>Transitions:</h1><ul>
    /// <li>And [<see cref="Operation"/>]: <see cref="And(string)"/></li>
    /// <li>Then [<see cref="Outcome"/>]: <see cref="Then(string)"/></li>
    /// </ul>
    /// </summary>
    public partial class Operation : FragmentBase
    {
        internal Operation(Narrative narrative):base(narrative){}

        /// <summary>
        /// <em>And [Operation]</em> <br/>
        /// Provide another action that is to be performed on the system, prior to our check for behaviour ('then')
        /// <p>This overload uses the text that is passed in, and can execute any action</p>
        /// </summary>
        /// <param name="text">The text of this story fragment</param>
        /// <param name="action">
        /// The code that should be executed to represent this story fragment. 
        /// Because StoryQ doesn't need to infer any text from the action, you can use lambdas and anonymous delegates here
        /// </param>
        [Description("Provide another action that is to be performed on the system, prior to our check for behaviour ('then')")]
        public Operation And(string text, Action action)
        {
            Narrative n = new Narrative("And", 5, text, action);
            return new Operation(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Operation]</em> <br/>
        /// Provide another action that is to be performed on the system, prior to our check for behaviour ('then')
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Provide another action that is to be performed on the system, prior to our check for behaviour ('then')")]
        public Operation And(Action descriptiveAction)
        {
            string text = MethodToText(descriptiveAction);
            Narrative n = new Narrative("And", 5, text, descriptiveAction);
            return new Operation(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Operation]</em> <br/>
        /// Provide another action that is to be performed on the system, prior to our check for behaviour ('then')
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Provide another action that is to be performed on the system, prior to our check for behaviour ('then')")]
        public Operation And<T1>(Action<T1> descriptiveAction, T1 arg1)
        {
            string text = MethodToText(descriptiveAction, arg1);
            Narrative n = new Narrative("And", 5, text, () => descriptiveAction(arg1));
            return new Operation(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Operation]</em> <br/>
        /// Provide another action that is to be performed on the system, prior to our check for behaviour ('then')
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Provide another action that is to be performed on the system, prior to our check for behaviour ('then')")]
        public Operation And<T1, T2>(Action<T1, T2> descriptiveAction, T1 arg1, T2 arg2)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2);
            Narrative n = new Narrative("And", 5, text, () => descriptiveAction(arg1, arg2));
            return new Operation(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Operation]</em> <br/>
        /// Provide another action that is to be performed on the system, prior to our check for behaviour ('then')
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg3">The third argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Provide another action that is to be performed on the system, prior to our check for behaviour ('then')")]
        public Operation And<T1, T2, T3>(Action<T1, T2, T3> descriptiveAction, T1 arg1, T2 arg2, T3 arg3)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2, arg3);
            Narrative n = new Narrative("And", 5, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Operation(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Operation]</em> <br/>
        /// Provide another action that is to be performed on the system, prior to our check for behaviour ('then')
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg3">The third argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg4">The fourth argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Provide another action that is to be performed on the system, prior to our check for behaviour ('then')")]
        public Operation And<T1, T2, T3, T4>(Action<T1, T2, T3, T4> descriptiveAction, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2, arg3, arg4);
            Narrative n = new Narrative("And", 5, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Operation(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Operation]</em> <br/>
        /// Provide another action that is to be performed on the system, prior to our check for behaviour ('then')
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment will 'Pend' unless you call .Pass() immediately after this method.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Provide another action that is to be performed on the system, prior to our check for behaviour ('then')")]
        public Operation And(string text)
        {
            Narrative n = new Narrative("And", 5, text, Narrative.Pend);
            return new Operation(n){ Parent = this };
        }

        /// <summary>
        /// <em>Then [Outcome]</em> <br/>
        /// Describe the system's behaviour that the prior state and actions should elicit
        /// <p>This overload uses the text that is passed in, and can execute any action</p>
        /// </summary>
        /// <param name="text">The text of this story fragment</param>
        /// <param name="action">
        /// The code that should be executed to represent this story fragment. 
        /// Because StoryQ doesn't need to infer any text from the action, you can use lambdas and anonymous delegates here
        /// </param>
        [Description("Describe the system's behaviour that the prior state and actions should elicit")]
        public Outcome Then(string text, Action action)
        {
            Narrative n = new Narrative("Then", 4, text, action);
            return new Outcome(n){ Parent = this };
        }

        /// <summary>
        /// <em>Then [Outcome]</em> <br/>
        /// Describe the system's behaviour that the prior state and actions should elicit
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Describe the system's behaviour that the prior state and actions should elicit")]
        public Outcome Then(Action descriptiveAction)
        {
            string text = MethodToText(descriptiveAction);
            Narrative n = new Narrative("Then", 4, text, descriptiveAction);
            return new Outcome(n){ Parent = this };
        }

        /// <summary>
        /// <em>Then [Outcome]</em> <br/>
        /// Describe the system's behaviour that the prior state and actions should elicit
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Describe the system's behaviour that the prior state and actions should elicit")]
        public Outcome Then<T1>(Action<T1> descriptiveAction, T1 arg1)
        {
            string text = MethodToText(descriptiveAction, arg1);
            Narrative n = new Narrative("Then", 4, text, () => descriptiveAction(arg1));
            return new Outcome(n){ Parent = this };
        }

        /// <summary>
        /// <em>Then [Outcome]</em> <br/>
        /// Describe the system's behaviour that the prior state and actions should elicit
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Describe the system's behaviour that the prior state and actions should elicit")]
        public Outcome Then<T1, T2>(Action<T1, T2> descriptiveAction, T1 arg1, T2 arg2)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2);
            Narrative n = new Narrative("Then", 4, text, () => descriptiveAction(arg1, arg2));
            return new Outcome(n){ Parent = this };
        }

        /// <summary>
        /// <em>Then [Outcome]</em> <br/>
        /// Describe the system's behaviour that the prior state and actions should elicit
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg3">The third argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Describe the system's behaviour that the prior state and actions should elicit")]
        public Outcome Then<T1, T2, T3>(Action<T1, T2, T3> descriptiveAction, T1 arg1, T2 arg2, T3 arg3)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2, arg3);
            Narrative n = new Narrative("Then", 4, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Outcome(n){ Parent = this };
        }

        /// <summary>
        /// <em>Then [Outcome]</em> <br/>
        /// Describe the system's behaviour that the prior state and actions should elicit
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg3">The third argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg4">The fourth argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Describe the system's behaviour that the prior state and actions should elicit")]
        public Outcome Then<T1, T2, T3, T4>(Action<T1, T2, T3, T4> descriptiveAction, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2, arg3, arg4);
            Narrative n = new Narrative("Then", 4, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Outcome(n){ Parent = this };
        }

        /// <summary>
        /// <em>Then [Outcome]</em> <br/>
        /// Describe the system's behaviour that the prior state and actions should elicit
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment will 'Pend' unless you call .Pass() immediately after this method.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Describe the system's behaviour that the prior state and actions should elicit")]
        public Outcome Then(string text)
        {
            Narrative n = new Narrative("Then", 4, text, Narrative.Pend);
            return new Outcome(n){ Parent = this };
        }

        /// <summary>
        /// Makes this narrative "pend". Use this when the test itself is incomplete.
        /// This is the default for any executable narrative
        /// </summary>
        /// <returns>itself</returns>
        public Operation Pend()
        {
            Narrative.Action = Narrative.Pend;
            return this;
        }
        
        /// <summary>
        /// Makes this narrative pass. Use this when you want to override the existing behaviour 
        /// (such as pending, or running a failing peice of code)
        /// "Pending" is the default for any executable narrative
        /// </summary>
        /// <returns>itself</returns>
        public Operation Pass()
        {
            Narrative.Action = () => {};
            return this;
        }
    }

    /// <summary>
    /// The [Outcome] story fragment.
    /// The result that is expected from executing the specified actions on the specified initial state
    /// <h1>Transitions:</h1><ul>
    /// <li>And [<see cref="Outcome"/>]: <see cref="And(string)"/></li>
    /// <li>With scenario [<see cref="Scenario"/>]: <see cref="WithScenario(string)"/></li>
    /// </ul>
    /// </summary>
    public partial class Outcome : FragmentBase
    {
        internal Outcome(Narrative narrative):base(narrative){}

        /// <summary>
        /// <em>And [Outcome]</em> <br/>
        /// Provide another resultant behaviour to check for
        /// <p>This overload uses the text that is passed in, and can execute any action</p>
        /// </summary>
        /// <param name="text">The text of this story fragment</param>
        /// <param name="action">
        /// The code that should be executed to represent this story fragment. 
        /// Because StoryQ doesn't need to infer any text from the action, you can use lambdas and anonymous delegates here
        /// </param>
        [Description("Provide another resultant behaviour to check for")]
        public Outcome And(string text, Action action)
        {
            Narrative n = new Narrative("And", 5, text, action);
            return new Outcome(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Outcome]</em> <br/>
        /// Provide another resultant behaviour to check for
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Provide another resultant behaviour to check for")]
        public Outcome And(Action descriptiveAction)
        {
            string text = MethodToText(descriptiveAction);
            Narrative n = new Narrative("And", 5, text, descriptiveAction);
            return new Outcome(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Outcome]</em> <br/>
        /// Provide another resultant behaviour to check for
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Provide another resultant behaviour to check for")]
        public Outcome And<T1>(Action<T1> descriptiveAction, T1 arg1)
        {
            string text = MethodToText(descriptiveAction, arg1);
            Narrative n = new Narrative("And", 5, text, () => descriptiveAction(arg1));
            return new Outcome(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Outcome]</em> <br/>
        /// Provide another resultant behaviour to check for
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Provide another resultant behaviour to check for")]
        public Outcome And<T1, T2>(Action<T1, T2> descriptiveAction, T1 arg1, T2 arg2)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2);
            Narrative n = new Narrative("And", 5, text, () => descriptiveAction(arg1, arg2));
            return new Outcome(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Outcome]</em> <br/>
        /// Provide another resultant behaviour to check for
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg3">The third argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Provide another resultant behaviour to check for")]
        public Outcome And<T1, T2, T3>(Action<T1, T2, T3> descriptiveAction, T1 arg1, T2 arg2, T3 arg3)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2, arg3);
            Narrative n = new Narrative("And", 5, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Outcome(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Outcome]</em> <br/>
        /// Provide another resultant behaviour to check for
        /// <p>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></p>
        /// </summary>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg3">The third argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg4">The fourth argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Provide another resultant behaviour to check for")]
        public Outcome And<T1, T2, T3, T4>(Action<T1, T2, T3, T4> descriptiveAction, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2, arg3, arg4);
            Narrative n = new Narrative("And", 5, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Outcome(n){ Parent = this };
        }

        /// <summary>
        /// <em>And [Outcome]</em> <br/>
        /// Provide another resultant behaviour to check for
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment will 'Pend' unless you call .Pass() immediately after this method.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Provide another resultant behaviour to check for")]
        public Outcome And(string text)
        {
            Narrative n = new Narrative("And", 5, text, Narrative.Pend);
            return new Outcome(n){ Parent = this };
        }

        /// <summary>
        /// <em>With scenario [Scenario]</em> <br/>
        /// Add another scenario to this story. StoryQ executes these scenarios one after the other, so state <em>can</em> be shared between a single story's scenarios.
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Scenario"/></returns>
        [Description("Add another scenario to this story. StoryQ executes these scenarios one after the other, so state <em>can</em> be shared between a single story's scenarios.")]
        public Scenario WithScenario(string text)
        {
            Narrative n = new Narrative("With scenario", 3, text, Narrative.DoNothing);
            return new Scenario(n){ Parent = this };
        }

        /// <summary>
        /// Makes this narrative "pend". Use this when the test itself is incomplete.
        /// This is the default for any executable narrative
        /// </summary>
        /// <returns>itself</returns>
        public Outcome Pend()
        {
            Narrative.Action = Narrative.Pend;
            return this;
        }
        
        /// <summary>
        /// Makes this narrative pass. Use this when you want to override the existing behaviour 
        /// (such as pending, or running a failing peice of code)
        /// "Pending" is the default for any executable narrative
        /// </summary>
        /// <returns>itself</returns>
        public Outcome Pass()
        {
            Narrative.Action = () => {};
            return this;
        }
    }
}




