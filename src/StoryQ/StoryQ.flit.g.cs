using System;
using System.ComponentModel;
using StoryQ.Infrastructure;

// tells the parser what our entry points are:
[assembly:ParserEntryPointAttribute(typeof(StoryQ.Infrastructure.StoryQEntryPoints))]

namespace StoryQ
{

    /// <summary>
    /// The [Story] story fragment.
    /// This is the root item of any story
    /// <h1>Transitions:</h1><ul>
    /// <li>in order to [<see cref="Benefit"/>]: <see cref="InOrderTo(string)"/></li>
    /// </ul>
    /// </summary>
    public class Story : FragmentBase
    {
        /// <summary>
        /// Starts a new StoryQ Story.
        /// </summary>
        /// <param name="text">The name of the new Story</param>

        public Story(string text) : base(new Step("story is", 0, text, Step.DoNothing), null){}

        /// <summary>
        /// in order to [Benefit].
        /// Describe the real-world value for this story. What is the business process that the user requires software support from?
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Benefit"/></returns>
        [Description("Describe the real-world value for this story. What is the business process that the user requires software support from?")]
        public Benefit InOrderTo(string text)
        {
            Step s = new Step("in order to", 1, text, Step.DoNothing);
            return new Benefit(s, this);
        }

        /// <summary>
        /// Adds a tag to this step. Tags can be used make disparate steps searchable.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Story Tag(string tag)
        {
            ((IStepContainer)this).Step.Tags.Add(tag.Trim().Trim('#'));
            return this;
        }

    }

    /// <summary>
    /// The [Benefit] story fragment.
    /// The real-world objective (business value) of a story
    /// <h1>Transitions:</h1><ul>
    /// <li>and [<see cref="Benefit"/>]: <see cref="And(string)"/></li>
    /// <li>as a [<see cref="Role"/>]: <see cref="AsA(string)"/></li>
    /// </ul>
    /// </summary>
    public class Benefit : FragmentBase
    {
        internal Benefit(Step step, IStepContainer parent) : base(step, parent){}

        /// <summary>
        /// and [Benefit].
        /// Describe any secondary business functions that this story will support
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Benefit"/></returns>
        [Description("Describe any secondary business functions that this story will support")]
        public Benefit And(string text)
        {
            Step s = new Step("and", 2, text, Step.DoNothing);
            return new Benefit(s, this);
        }


        /// <summary>
        /// as a [Role].
        /// The role of the person who is the intended user of this feature
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Role"/></returns>
        [Description("The role of the person who is the intended user of this feature")]
        public Role AsA(string text)
        {
            Step s = new Step("as a", 1, text, Step.DoNothing);
            return new Role(s, this);
        }

        /// <summary>
        /// Adds a tag to this step. Tags can be used make disparate steps searchable.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Benefit Tag(string tag)
        {
            ((IStepContainer)this).Step.Tags.Add(tag.Trim().Trim('#'));
            return this;
        }

    }

    /// <summary>
    /// The [Role] story fragment.
    /// The role (a category of actors/users) or roles that receive this benefit. 
    /// <h1>Transitions:</h1><ul>
    /// <li>or as a [<see cref="Role"/>]: <see cref="OrAsA(string)"/></li>
    /// <li>i want [<see cref="Feature"/>]: <see cref="IWant(string)"/></li>
    /// </ul>
    /// </summary>
    public class Role : FragmentBase
    {
        internal Role(Step step, IStepContainer parent) : base(step, parent){}

        /// <summary>
        /// or as a [Role].
        /// Any other roles that may use this story
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Role"/></returns>
        [Description("Any other roles that may use this story")]
        public Role OrAsA(string text)
        {
            Step s = new Step("or as a", 2, text, Step.DoNothing);
            return new Role(s, this);
        }


        /// <summary>
        /// i want [Feature].
        /// Describe the software process (features) that will support the business requirement
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Feature"/></returns>
        [Description("Describe the software process (features) that will support the business requirement")]
        public Feature IWant(string text)
        {
            Step s = new Step("i want", 1, text, Step.DoNothing);
            return new Feature(s, this);
        }

        /// <summary>
        /// Adds a tag to this step. Tags can be used make disparate steps searchable.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Role Tag(string tag)
        {
            ((IStepContainer)this).Step.Tags.Add(tag.Trim().Trim('#'));
            return this;
        }

    }

    /// <summary>
    /// The [Feature] story fragment.
    /// The software process that will implement the specified benefit.
    /// <h1>Transitions:</h1><ul>
    /// <li>and [<see cref="Feature"/>]: <see cref="And(string)"/></li>
    /// <li>with scenario [<see cref="Scenario"/>]: <see cref="WithScenario(string)"/></li>
    /// </ul>
    /// </summary>
    public class Feature : FragmentBase
    {
        internal Feature(Step step, IStepContainer parent) : base(step, parent){}

        /// <summary>
        /// and [Feature].
        /// Any other features that will implement the desired benefit
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Feature"/></returns>
        [Description("Any other features that will implement the desired benefit")]
        public Feature And(string text)
        {
            Step s = new Step("and", 2, text, Step.DoNothing);
            return new Feature(s, this);
        }


        /// <summary>
        /// with scenario [Scenario].
        /// Add a scenario ('given'/'when'/'then') to this story. Scenarios can be added (and will be run) in sequence. Each scenario should have a short descriptive name.
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Scenario"/></returns>
        [Description("Add a scenario ('given'/'when'/'then') to this story. Scenarios can be added (and will be run) in sequence. Each scenario should have a short descriptive name.")]
        [Alias("Scenario:")]
        public Scenario WithScenario(string text)
        {
            Step s = new Step("with scenario", 3, text, Step.DoNothing);
            return new Scenario(s, this);
        }

        /// <summary>
        /// Adds a tag to this step. Tags can be used make disparate steps searchable.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Feature Tag(string tag)
        {
            ((IStepContainer)this).Step.Tags.Add(tag.Trim().Trim('#'));
            return this;
        }

    }

    /// <summary>
    /// The [Scenario] story fragment.
    /// The name of each scenario within a story. You can think of each scenario as a chapter in a book.
    /// <h1>Transitions:</h1><ul>
    /// <li>given [<see cref="Condition"/>]: <see cref="Given(Action)"/></li>
    /// </ul>
    /// </summary>
    public class Scenario : FragmentBase
    {
        internal Scenario(Step step, IStepContainer parent) : base(step, parent){}

        /// <summary>
        /// given [Condition].
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
            Step s = new Step("given", 4, text, descriptiveAction);
            return new Condition(s, this);
        }

        /// <summary>
        /// given [Condition].
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
            Step s = new Step("given", 4, text, () => descriptiveAction(arg1));
            return new Condition(s, this);
        }

        /// <summary>
        /// given [Condition].
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
            Step s = new Step("given", 4, text, () => descriptiveAction(arg1, arg2));
            return new Condition(s, this);
        }

        /// <summary>
        /// given [Condition].
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
            Step s = new Step("given", 4, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Condition(s, this);
        }

        /// <summary>
        /// given [Condition].
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
            Step s = new Step("given", 4, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Condition(s, this);
        }

        /// <summary>
        /// given [Condition].
        /// Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state")]
        protected Condition Given(string text)
        {
            Step s = new Step("given", 4, text, null);
            return new Condition(s, this);
        }

        /// <summary>
        /// Adds a tag to this step. Tags can be used make disparate steps searchable.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Scenario Tag(string tag)
        {
            ((IStepContainer)this).Step.Tags.Add(tag.Trim().Trim('#'));
            return this;
        }

    }

    /// <summary>
    /// The [Condition] story fragment.
    /// The preconditions that are meant to be present at the beginning of the scenario.
    /// <h1>Transitions:</h1><ul>
    /// <li>and [<see cref="Condition"/>]: <see cref="And(Action)"/></li>
    /// <li>when [<see cref="Operation"/>]: <see cref="When(Action)"/></li>
    /// </ul>
    /// </summary>
    public class Condition : FragmentBase
    {
        internal Condition(Step step, IStepContainer parent) : base(step, parent){}

        /// <summary>
        /// and [Condition].
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
            Step s = new Step("and", 5, text, descriptiveAction);
            return new Condition(s, this);
        }

        /// <summary>
        /// and [Condition].
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
            Step s = new Step("and", 5, text, () => descriptiveAction(arg1));
            return new Condition(s, this);
        }

        /// <summary>
        /// and [Condition].
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
            Step s = new Step("and", 5, text, () => descriptiveAction(arg1, arg2));
            return new Condition(s, this);
        }

        /// <summary>
        /// and [Condition].
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
            Step s = new Step("and", 5, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Condition(s, this);
        }

        /// <summary>
        /// and [Condition].
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
            Step s = new Step("and", 5, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Condition(s, this);
        }

        /// <summary>
        /// and [Condition].
        /// Provide another precondition to describe our scenario's initial state
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
        [Description("Provide another precondition to describe our scenario's initial state")]
        protected Condition And(string text)
        {
            Step s = new Step("and", 5, text, null);
            return new Condition(s, this);
        }


        /// <summary>
        /// when [Operation].
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
            Step s = new Step("when", 4, text, descriptiveAction);
            return new Operation(s, this);
        }

        /// <summary>
        /// when [Operation].
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
            Step s = new Step("when", 4, text, () => descriptiveAction(arg1));
            return new Operation(s, this);
        }

        /// <summary>
        /// when [Operation].
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
            Step s = new Step("when", 4, text, () => descriptiveAction(arg1, arg2));
            return new Operation(s, this);
        }

        /// <summary>
        /// when [Operation].
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
            Step s = new Step("when", 4, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Operation(s, this);
        }

        /// <summary>
        /// when [Operation].
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
            Step s = new Step("when", 4, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Operation(s, this);
        }

        /// <summary>
        /// when [Operation].
        /// Describe the actions that are done to the system under test. '
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Describe the actions that are done to the system under test. '")]
        protected Operation When(string text)
        {
            Step s = new Step("when", 4, text, null);
            return new Operation(s, this);
        }

        /// <summary>
        /// Adds a tag to this step. Tags can be used make disparate steps searchable.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Condition Tag(string tag)
        {
            ((IStepContainer)this).Step.Tags.Add(tag.Trim().Trim('#'));
            return this;
        }

    }

    /// <summary>
    /// The [Operation] story fragment.
    /// The action(s) that are performed upon the system under test
    /// <h1>Transitions:</h1><ul>
    /// <li>and [<see cref="Operation"/>]: <see cref="And(Action)"/></li>
    /// <li>then [<see cref="Outcome"/>]: <see cref="Then(Action)"/></li>
    /// </ul>
    /// </summary>
    public class Operation : FragmentBase
    {
        internal Operation(Step step, IStepContainer parent) : base(step, parent){}

        /// <summary>
        /// and [Operation].
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
            Step s = new Step("and", 5, text, descriptiveAction);
            return new Operation(s, this);
        }

        /// <summary>
        /// and [Operation].
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
            Step s = new Step("and", 5, text, () => descriptiveAction(arg1));
            return new Operation(s, this);
        }

        /// <summary>
        /// and [Operation].
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
            Step s = new Step("and", 5, text, () => descriptiveAction(arg1, arg2));
            return new Operation(s, this);
        }

        /// <summary>
        /// and [Operation].
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
            Step s = new Step("and", 5, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Operation(s, this);
        }

        /// <summary>
        /// and [Operation].
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
            Step s = new Step("and", 5, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Operation(s, this);
        }

        /// <summary>
        /// and [Operation].
        /// Provide another action that is to be performed on the system, prior to our check for behaviour ('then')
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
        [Description("Provide another action that is to be performed on the system, prior to our check for behaviour ('then')")]
        protected Operation And(string text)
        {
            Step s = new Step("and", 5, text, null);
            return new Operation(s, this);
        }


        /// <summary>
        /// then [Outcome].
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
            Step s = new Step("then", 4, text, descriptiveAction);
            return new Outcome(s, this);
        }

        /// <summary>
        /// then [Outcome].
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
            Step s = new Step("then", 4, text, () => descriptiveAction(arg1));
            return new Outcome(s, this);
        }

        /// <summary>
        /// then [Outcome].
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
            Step s = new Step("then", 4, text, () => descriptiveAction(arg1, arg2));
            return new Outcome(s, this);
        }

        /// <summary>
        /// then [Outcome].
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
            Step s = new Step("then", 4, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Outcome(s, this);
        }

        /// <summary>
        /// then [Outcome].
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
            Step s = new Step("then", 4, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Outcome(s, this);
        }

        /// <summary>
        /// then [Outcome].
        /// Describe the system's behaviour that the prior state and actions should elicit
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Describe the system's behaviour that the prior state and actions should elicit")]
        protected Outcome Then(string text)
        {
            Step s = new Step("then", 4, text, null);
            return new Outcome(s, this);
        }

        /// <summary>
        /// Adds a tag to this step. Tags can be used make disparate steps searchable.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Operation Tag(string tag)
        {
            ((IStepContainer)this).Step.Tags.Add(tag.Trim().Trim('#'));
            return this;
        }

    }

    /// <summary>
    /// The [Outcome] story fragment.
    /// The result that is expected from executing the specified actions on the specified initial state
    /// <h1>Transitions:</h1><ul>
    /// <li>and [<see cref="Outcome"/>]: <see cref="And(Action)"/></li>
    /// <li>with scenario [<see cref="Scenario"/>]: <see cref="WithScenario(string)"/></li>
    /// </ul>
    /// </summary>
    public class Outcome : FragmentBase
    {
        internal Outcome(Step step, IStepContainer parent) : base(step, parent){}

        /// <summary>
        /// and [Outcome].
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
            Step s = new Step("and", 5, text, descriptiveAction);
            return new Outcome(s, this);
        }

        /// <summary>
        /// and [Outcome].
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
            Step s = new Step("and", 5, text, () => descriptiveAction(arg1));
            return new Outcome(s, this);
        }

        /// <summary>
        /// and [Outcome].
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
            Step s = new Step("and", 5, text, () => descriptiveAction(arg1, arg2));
            return new Outcome(s, this);
        }

        /// <summary>
        /// and [Outcome].
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
            Step s = new Step("and", 5, text, () => descriptiveAction(arg1, arg2, arg3));
            return new Outcome(s, this);
        }

        /// <summary>
        /// and [Outcome].
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
            Step s = new Step("and", 5, text, () => descriptiveAction(arg1, arg2, arg3, arg4));
            return new Outcome(s, this);
        }

        /// <summary>
        /// and [Outcome].
        /// Provide another resultant behaviour to check for
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
        [Description("Provide another resultant behaviour to check for")]
        protected Outcome And(string text)
        {
            Step s = new Step("and", 5, text, null);
            return new Outcome(s, this);
        }


        /// <summary>
        /// with scenario [Scenario].
        /// Add another scenario to this story. StoryQ executes these scenarios one after the other, so state can be shared between a single story's scenarios.
        /// </summary>
        /// <param name="text">
        /// A textual description. This story fragment is not executable.
        /// </param>
        /// <returns>The next fragment of your story, a <see cref="Scenario"/></returns>
        [Description("Add another scenario to this story. StoryQ executes these scenarios one after the other, so state can be shared between a single story's scenarios.")]
        [Alias("Scenario:")]
        public Scenario WithScenario(string text)
        {
            Step s = new Step("with scenario", 3, text, Step.DoNothing);
            return new Scenario(s, this);
        }

        /// <summary>
        /// Adds a tag to this step. Tags can be used make disparate steps searchable.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Outcome Tag(string tag)
        {
            ((IStepContainer)this).Step.Tags.Add(tag.Trim().Trim('#'));
            return this;
        }

    }

    namespace TextualSteps
    {
        ///<summary>
        /// Extension methods to enable string-based executable steps. These will always Pend
        ///</summary>
        public static class Extensions
        {
            /// <summary>
            /// given [Condition].
            /// Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state
            /// This story fragment should be executable, so a method is the preferred argument, but you can supply a string in the meantime. The step will Pend.
            /// </summary>
            /// <param name="parent">this</param>
            /// <param name="text">
            /// A textual description of the step.
            /// </param>
            /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
            [Description("Provide the initial context to the scenario. Try not to describe behaviour or actions, this step describes and sets up initial state")]
            public static Condition Given(this Scenario parent, string text)
            {
                Step s = new Step("given", 4, text, () => { throw new StringBasedExecutableStepException(text); });
                return new Condition(s, parent);
            }

            /// <summary>
            /// and [Condition].
            /// Provide another precondition to describe our scenario's initial state
            /// This story fragment should be executable, so a method is the preferred argument, but you can supply a string in the meantime. The step will Pend.
            /// </summary>
            /// <param name="parent">this</param>
            /// <param name="text">
            /// A textual description of the step.
            /// </param>
            /// <returns>The next fragment of your story, a <see cref="Condition"/></returns>
            [Description("Provide another precondition to describe our scenario's initial state")]
            public static Condition And(this Condition parent, string text)
            {
                Step s = new Step("and", 5, text, () => { throw new StringBasedExecutableStepException(text); });
                return new Condition(s, parent);
            }

            /// <summary>
            /// when [Operation].
            /// Describe the actions that are done to the system under test. '
            /// This story fragment should be executable, so a method is the preferred argument, but you can supply a string in the meantime. The step will Pend.
            /// </summary>
            /// <param name="parent">this</param>
            /// <param name="text">
            /// A textual description of the step.
            /// </param>
            /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
            [Description("Describe the actions that are done to the system under test. '")]
            public static Operation When(this Condition parent, string text)
            {
                Step s = new Step("when", 4, text, () => { throw new StringBasedExecutableStepException(text); });
                return new Operation(s, parent);
            }

            /// <summary>
            /// and [Operation].
            /// Provide another action that is to be performed on the system, prior to our check for behaviour ('then')
            /// This story fragment should be executable, so a method is the preferred argument, but you can supply a string in the meantime. The step will Pend.
            /// </summary>
            /// <param name="parent">this</param>
            /// <param name="text">
            /// A textual description of the step.
            /// </param>
            /// <returns>The next fragment of your story, a <see cref="Operation"/></returns>
            [Description("Provide another action that is to be performed on the system, prior to our check for behaviour ('then')")]
            public static Operation And(this Operation parent, string text)
            {
                Step s = new Step("and", 5, text, () => { throw new StringBasedExecutableStepException(text); });
                return new Operation(s, parent);
            }

            /// <summary>
            /// then [Outcome].
            /// Describe the system's behaviour that the prior state and actions should elicit
            /// This story fragment should be executable, so a method is the preferred argument, but you can supply a string in the meantime. The step will Pend.
            /// </summary>
            /// <param name="parent">this</param>
            /// <param name="text">
            /// A textual description of the step.
            /// </param>
            /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
            [Description("Describe the system's behaviour that the prior state and actions should elicit")]
            public static Outcome Then(this Operation parent, string text)
            {
                Step s = new Step("then", 4, text, () => { throw new StringBasedExecutableStepException(text); });
                return new Outcome(s, parent);
            }

            /// <summary>
            /// and [Outcome].
            /// Provide another resultant behaviour to check for
            /// This story fragment should be executable, so a method is the preferred argument, but you can supply a string in the meantime. The step will Pend.
            /// </summary>
            /// <param name="parent">this</param>
            /// <param name="text">
            /// A textual description of the step.
            /// </param>
            /// <returns>The next fragment of your story, a <see cref="Outcome"/></returns>
            [Description("Provide another resultant behaviour to check for")]
            public static Outcome And(this Outcome parent, string text)
            {
                Step s = new Step("and", 5, text, () => { throw new StringBasedExecutableStepException(text); });
                return new Outcome(s, parent);
            }

        }
    }

    namespace Infrastructure
    {
        /// <summary>
        /// Entry points for the StoryQ converter's parser
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public class StoryQEntryPoints
        {
            /// <summary>
            /// For infrastructure use only
            /// </summary>
            [Description("This is the root item of any story")]
            [Alias("Story is")]
            [Alias("Feature:")]
            protected Story Story(string text)
            {
                return new Story(text);
            }

        }
    }
}

