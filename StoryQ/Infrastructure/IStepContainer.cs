namespace StoryQ.Infrastructure
{
    using StoryQ.Execution.Rendering;

    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Something that holds steps
    /// </summary>
    public interface IStepContainer
    {
        /// <summary>
        /// Gets the Step.
        /// </summary>
        /// <value>The Step.</value>
        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Previously released.")]
        Step Step { get; }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>The parent.</value>
        IStepContainer Parent { get; }

        /// <summary>
        /// Enumerates over this and each of its ancestors. Reverse the collection to go through the
        /// story in correct order
        /// </summary>
        /// <returns>IEnumerable&lt;IStepContainer&gt;.</returns>
        IEnumerable<IStepContainer> SelfAndAncestors();

        /// <summary>
        /// Runs the current sequence of Steps against a renderer
        /// </summary>
        /// <param name="renderers">The renderers.</param>
        void Execute(params IRenderer[] renderers);
    }
}
