using System;
using System.ComponentModel;

namespace StoryQ
{

    /// <summary>
    /// The [Story] story fragment.
    /// This is the root item of any story
    /// <h1>Transitions:</h1><ul>
    /// <li>In order to [<see cref="Benefit"/>]: <see cref="InOrderTo"/></li>
    /// </ul>
    /// </summary>
    public partial class Story : FragmentBase
    {
        /// <summary>
        /// Starts a new StoryQ Story. 
        /// </summary>
        /// <param name="text">The name of the new Story</param>
        public Story(string text):base(new Step("Story is", 0, text, Step.DoNothing)){}

        /// <summary>
        /// In order to [Benefit].
        /// Describe the real-world value for this story. What is the business process that the user requires software support from?
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Benefit"/></returns>
        [Description("Describe the real-world value for this story. What is the business process that the user requires software support from?")]
        public Benefit InOrderTo(string text)
        {
            Step s = new Step("In order to", 1, text, Step.DoNothing);
            return new Benefit(s){ Parent = this };
        }

        /// <summary>
        /// Adds a tag to this step. Tags can be used make disparate steps searchable.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Story Tag(string tag)
        {
            Step.Tags.Add(tag.Trim().Trim('#'));
            return this;
        }

    }

    /// <summary>
    /// The [Benefit] story fragment.
    /// The real-world objective (business value) of a story
    /// <h1>Transitions:</h1><ul>
    /// <li>And [<see cref="Benefit"/>]: <see cref="And"/></li>
    /// <li>As a [<see cref="Role"/>]: <see cref="AsA"/></li>
    /// </ul>
    /// </summary>
    public partial class Benefit : FragmentBase
    {
        internal Benefit(Step step):base(step){}

        /// <summary>
        /// And [Benefit].
        /// Describe any secondary business functions that this story will support
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Benefit"/></returns>
        [Description("Describe any secondary business functions that this story will support")]
        public Benefit And(string text)
        {
            Step s = new Step("And", 2, text, Step.DoNothing);
            return new Benefit(s){ Parent = this };
        }


        /// <summary>
        /// As a [Role].
        /// The role of the person who is the intended user of this feature
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Role"/></returns>
        [Description("The role of the person who is the intended user of this feature")]
        public Role AsA(string text)
        {
            Step s = new Step("As a", 1, text, Step.DoNothing);
            return new Role(s){ Parent = this };
        }

        /// <summary>
        /// Adds a tag to this step. Tags can be used make disparate steps searchable.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Benefit Tag(string tag)
        {
            Step.Tags.Add(tag.Trim().Trim('#'));
            return this;
        }

    }

    /// <summary>
    /// The [Role] story fragment.
    /// The role (a category of actors/users) or roles that receive this benefit. 
    /// <h1>Transitions:</h1><ul>
    /// <li>Or as a [<see cref="Role"/>]: <see cref="OrAsA"/></li>
    /// <li>I want [<see cref="Feature"/>]: <see cref="IWant"/></li>
    /// </ul>
    /// </summary>
    public partial class Role : FragmentBase
    {
        internal Role(Step step):base(step){}

        /// <summary>
        /// Or as a [Role].
        /// Any other roles that may use this story
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Role"/></returns>
        [Description("Any other roles that may use this story")]
        public Role OrAsA(string text)
        {
            Step s = new Step("Or as a", 2, text, Step.DoNothing);
            return new Role(s){ Parent = this };
        }


        /// <summary>
        /// I want [Feature].
        /// Describe the software process (features) that will support the business requirement
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Feature"/></returns>
        [Description("Describe the software process (features) that will support the business requirement")]
        public Feature IWant(string text)
        {
            Step s = new Step("I want", 1, text, Step.DoNothing);
            return new Feature(s){ Parent = this };
        }

        /// <summary>
        /// Adds a tag to this step. Tags can be used make disparate steps searchable.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Role Tag(string tag)
        {
            Step.Tags.Add(tag.Trim().Trim('#'));
            return this;
        }

    }

    /// <summary>
    /// The [Feature] story fragment.
    /// The software process that will implement the specified benefit.
    /// <h1>Transitions:</h1><ul>
    /// <li>And [<see cref="Feature"/>]: <see cref="And"/></li>
    /// <li>With scenario [<see cref="Scenario"/>]: <see cref="WithScenario"/></li>
    /// </ul>
    /// </summary>
    public partial class Feature : FragmentBase
    {
        internal Feature(Step step):base(step){}

        /// <summary>
        /// And [Feature].
        /// Any other features that will implement the desired benefit
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Feature"/></returns>
        [Description("Any other features that will implement the desired benefit")]
        public Feature And(string text)
        {
            Step s = new Step("And", 2, text, Step.DoNothing);
            return new Feature(s){ Parent = this };
        }


        /// <summary>
        /// With scenario [Scenario].
        /// Add a scenario ('given'/'when'/'then') to this story. Scenarios can be added (and will be run) in sequence. Each scenario should have a short descriptive name.
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Scenario"/></returns>
        [Description("Add a scenario ('given'/'when'/'then') to this story. Scenarios can be added (and will be run) in sequence. Each scenario should have a short descriptive name.")]
        public Scenario WithScenario(string text)
        {
            Step s = new Step("With scenario", 3, text, Step.DoNothing);
            return new Scenario(s){ Parent = this };
        }

        /// <summary>
        /// Adds a tag to this step. Tags can be used make disparate steps searchable.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Feature Tag(string tag)
        {
            Step.Tags.Add(tag.Trim().Trim('#'));
            return this;
        }

    }

    /// <summary>
    /// The [Scenario] story fragment.
    /// The name of each scenario within a story. You can think of each scenario as a chapter in a book.
    /// <h1>Transitions:</h1><ul>
    /// <li>Given [<see cref="Condition"/>]: <see cref="Given"/></li>
    /// </ul>
    /// </summary>
    public partial class Scenario : FragmentBase
    {
        internal Scenario(Step step):base(step){}

        /// <summary>
        /// Given [Condition].
        /// Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("Given", 4, text, descriptiveAction);
            return new Condition(s){ Parent = this };
        }

        /// <summary>
        /// Given [Condition].
        /// Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("Given", 4, text, () => descriptiveAction(arg1));
            return new Condition(s){ Parent = this };
        }

        /// <summary>
        /// Given [Condition].
        /// Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("Given", 4, text, () => descriptiveAction(arg1, arg2));
            return new Condition(s){ Parent = this };
        }

        /// <summary>
        /// Given [Condition].
        /// Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("Given", 4, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Condition(s){ Parent = this };
        }

        /// <summary>
        /// Given [Condition].
        /// Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("Given", 4, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Condition(s){ Parent = this };
        }

        /// <summary>
        /// Given [Condition].
        /// Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state")]
        protected Condition Given(string text)
        {
            Step s = new Step("Given", 4, text, null);
            return new Condition(s){ Parent = this };
        }

        /// <summary>
        /// Adds a tag to this step. Tags can be used make disparate steps searchable.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Scenario Tag(string tag)
        {
            Step.Tags.Add(tag.Trim().Trim('#'));
            return this;
        }

    }

    /// <summary>
    /// The [Condition] story fragment.
    /// The preconditions that are meant to be present at the beginning of the scenario.
    /// <h1>Transitions:</h1><ul>
    /// <li>And [<see cref="Condition"/>]: <see cref="And"/></li>
    /// <li>When [<see cref="Operation"/>]: <see cref="When"/></li>
    /// </ul>
    /// </summary>
    public partial class Condition : FragmentBase
    {
        internal Condition(Step step):base(step){}

        /// <summary>
        /// And [Condition].
        /// Provide another precondition to describe our scenario's initial state.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("And", 5, text, descriptiveAction);
            return new Condition(s){ Parent = this };
        }

        /// <summary>
        /// And [Condition].
        /// Provide another precondition to describe our scenario's initial state.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("And", 5, text, () => descriptiveAction(arg1));
            return new Condition(s){ Parent = this };
        }

        /// <summary>
        /// And [Condition].
        /// Provide another precondition to describe our scenario's initial state.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("And", 5, text, () => descriptiveAction(arg1, arg2));
            return new Condition(s){ Parent = this };
        }

        /// <summary>
        /// And [Condition].
        /// Provide another precondition to describe our scenario's initial state.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("And", 5, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Condition(s){ Parent = this };
        }

        /// <summary>
        /// And [Condition].
        /// Provide another precondition to describe our scenario's initial state.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("And", 5, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Condition(s){ Parent = this };
        }

        /// <summary>
        /// And [Condition].
        /// Provide another precondition to describe our scenario's initial state
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide another precondition to describe our scenario's initial state")]
        protected Condition And(string text)
        {
            Step s = new Step("And", 5, text, null);
            return new Condition(s){ Parent = this };
        }


        /// <summary>
        /// When [Operation].
        /// Describe the actions that are done to the system under test. '.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Describe the actions that are done to the system under test. '")]
        public Operation When(Action descriptiveAction)
        {
            string text = MethodToText(descriptiveAction);
            Step s = new Step("When", 4, text, descriptiveAction);
            return new Operation(s){ Parent = this };
        }

        /// <summary>
        /// When [Operation].
        /// Describe the actions that are done to the system under test. '.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Describe the actions that are done to the system under test. '")]
        public Operation When<T1>(Action<T1> descriptiveAction, T1 arg1)
        {
            string text = MethodToText(descriptiveAction, arg1);
            Step s = new Step("When", 4, text, () => descriptiveAction(arg1));
            return new Operation(s){ Parent = this };
        }

        /// <summary>
        /// When [Operation].
        /// Describe the actions that are done to the system under test. '.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Describe the actions that are done to the system under test. '")]
        public Operation When<T1, T2>(Action<T1, T2> descriptiveAction, T1 arg1, T2 arg2)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2);
            Step s = new Step("When", 4, text, () => descriptiveAction(arg1, arg2));
            return new Operation(s){ Parent = this };
        }

        /// <summary>
        /// When [Operation].
        /// Describe the actions that are done to the system under test. '.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
        /// <param name="descriptiveAction">
        /// A descriptively named method that should be run to fulfil this story fragment. The method's name will be used as the description for this fragment, once converted from CamelCase
        /// Any underscores in the method's name will be used as placeholders and will be replaced with the <see cref="object.ToString"/> of each respective argument.
        /// Do not use a lambda or anonymous method here, as the name will not be human readable
        /// </param>
        /// <param name="arg1">The first argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg2">The second argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <param name="arg3">The third argument to be passed to <paramref name="descriptiveAction"/></param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Describe the actions that are done to the system under test. '")]
        public Operation When<T1, T2, T3>(Action<T1, T2, T3> descriptiveAction, T1 arg1, T2 arg2, T3 arg3)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2, arg3);
            Step s = new Step("When", 4, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Operation(s){ Parent = this };
        }

        /// <summary>
        /// When [Operation].
        /// Describe the actions that are done to the system under test. '.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
        [Description("Describe the actions that are done to the system under test. '")]
        public Operation When<T1, T2, T3, T4>(Action<T1, T2, T3, T4> descriptiveAction, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            string text = MethodToText(descriptiveAction, arg1, arg2, arg3, arg4);
            Step s = new Step("When", 4, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Operation(s){ Parent = this };
        }

        /// <summary>
        /// When [Operation].
        /// Describe the actions that are done to the system under test. '
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Describe the actions that are done to the system under test. '")]
        protected Operation When(string text)
        {
            Step s = new Step("When", 4, text, null);
            return new Operation(s){ Parent = this };
        }

        /// <summary>
        /// Adds a tag to this step. Tags can be used make disparate steps searchable.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Condition Tag(string tag)
        {
            Step.Tags.Add(tag.Trim().Trim('#'));
            return this;
        }

    }

    /// <summary>
    /// The [Operation] story fragment.
    /// The action(s) that are performed upon the system under test
    /// <h1>Transitions:</h1><ul>
    /// <li>And [<see cref="Operation"/>]: <see cref="And"/></li>
    /// <li>Then [<see cref="Outcome"/>]: <see cref="Then"/></li>
    /// </ul>
    /// </summary>
    public partial class Operation : FragmentBase
    {
        internal Operation(Step step):base(step){}

        /// <summary>
        /// And [Operation].
        /// Provide another action that is to be performed on the system, prior to our check for behaviour ('then').
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("And", 5, text, descriptiveAction);
            return new Operation(s){ Parent = this };
        }

        /// <summary>
        /// And [Operation].
        /// Provide another action that is to be performed on the system, prior to our check for behaviour ('then').
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("And", 5, text, () => descriptiveAction(arg1));
            return new Operation(s){ Parent = this };
        }

        /// <summary>
        /// And [Operation].
        /// Provide another action that is to be performed on the system, prior to our check for behaviour ('then').
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("And", 5, text, () => descriptiveAction(arg1, arg2));
            return new Operation(s){ Parent = this };
        }

        /// <summary>
        /// And [Operation].
        /// Provide another action that is to be performed on the system, prior to our check for behaviour ('then').
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("And", 5, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Operation(s){ Parent = this };
        }

        /// <summary>
        /// And [Operation].
        /// Provide another action that is to be performed on the system, prior to our check for behaviour ('then').
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("And", 5, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Operation(s){ Parent = this };
        }

        /// <summary>
        /// And [Operation].
        /// Provide another action that is to be performed on the system, prior to our check for behaviour ('then')
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Provide another action that is to be performed on the system, prior to our check for behaviour ('then')")]
        protected Operation And(string text)
        {
            Step s = new Step("And", 5, text, null);
            return new Operation(s){ Parent = this };
        }


        /// <summary>
        /// Then [Outcome].
        /// Describe the system's behaviour that the prior state and actions should elicit.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("Then", 4, text, descriptiveAction);
            return new Outcome(s){ Parent = this };
        }

        /// <summary>
        /// Then [Outcome].
        /// Describe the system's behaviour that the prior state and actions should elicit.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("Then", 4, text, () => descriptiveAction(arg1));
            return new Outcome(s){ Parent = this };
        }

        /// <summary>
        /// Then [Outcome].
        /// Describe the system's behaviour that the prior state and actions should elicit.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("Then", 4, text, () => descriptiveAction(arg1, arg2));
            return new Outcome(s){ Parent = this };
        }

        /// <summary>
        /// Then [Outcome].
        /// Describe the system's behaviour that the prior state and actions should elicit.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("Then", 4, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Outcome(s){ Parent = this };
        }

        /// <summary>
        /// Then [Outcome].
        /// Describe the system's behaviour that the prior state and actions should elicit.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("Then", 4, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Outcome(s){ Parent = this };
        }

        /// <summary>
        /// Then [Outcome].
        /// Describe the system's behaviour that the prior state and actions should elicit
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Describe the system's behaviour that the prior state and actions should elicit")]
        protected Outcome Then(string text)
        {
            Step s = new Step("Then", 4, text, null);
            return new Outcome(s){ Parent = this };
        }

        /// <summary>
        /// Adds a tag to this step. Tags can be used make disparate steps searchable.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Operation Tag(string tag)
        {
            Step.Tags.Add(tag.Trim().Trim('#'));
            return this;
        }

    }

    /// <summary>
    /// The [Outcome] story fragment.
    /// The result that is expected from executing the specified actions on the specified initial state
    /// <h1>Transitions:</h1><ul>
    /// <li>And [<see cref="Outcome"/>]: <see cref="And"/></li>
    /// <li>With scenario [<see cref="Scenario"/>]: <see cref="WithScenario"/></li>
    /// </ul>
    /// </summary>
    public partial class Outcome : FragmentBase
    {
        internal Outcome(Step step):base(step){}

        /// <summary>
        /// And [Outcome].
        /// Provide another resultant behaviour to check for.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("And", 5, text, descriptiveAction);
            return new Outcome(s){ Parent = this };
        }

        /// <summary>
        /// And [Outcome].
        /// Provide another resultant behaviour to check for.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("And", 5, text, () => descriptiveAction(arg1));
            return new Outcome(s){ Parent = this };
        }

        /// <summary>
        /// And [Outcome].
        /// Provide another resultant behaviour to check for.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("And", 5, text, () => descriptiveAction(arg1, arg2));
            return new Outcome(s){ Parent = this };
        }

        /// <summary>
        /// And [Outcome].
        /// Provide another resultant behaviour to check for.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("And", 5, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Outcome(s){ Parent = this };
        }

        /// <summary>
        /// And [Outcome].
        /// Provide another resultant behaviour to check for.
        /// </summary>
        /// <remarks>This overload infers its text from the name of the parameter <paramref name="descriptiveAction"/></remarks>
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
            Step s = new Step("And", 5, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Outcome(s){ Parent = this };
        }

        /// <summary>
        /// And [Outcome].
        /// Provide another resultant behaviour to check for
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Provide another resultant behaviour to check for")]
        protected Outcome And(string text)
        {
            Step s = new Step("And", 5, text, null);
            return new Outcome(s){ Parent = this };
        }


        /// <summary>
        /// With scenario [Scenario].
        /// Add another scenario to this story. StoryQ executes these scenarios one after the other, so state can be shared between a single story's scenarios.
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Scenario"/></returns>
        [Description("Add another scenario to this story. StoryQ executes these scenarios one after the other, so state can be shared between a single story's scenarios.")]
        public Scenario WithScenario(string text)
        {
            Step s = new Step("With scenario", 3, text, Step.DoNothing);
            return new Scenario(s){ Parent = this };
        }

        /// <summary>
        /// Adds a tag to this step. Tags can be used make disparate steps searchable.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Outcome Tag(string tag)
        {
            Step.Tags.Add(tag.Trim().Trim('#'));
            return this;
        }

    }
}


