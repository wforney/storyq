using System.Collections.Generic;

using StoryQ.Execution.Rendering;

namespace StoryQ.Infrastructure
{
    ///<summary>
    /// Something that holds steps
    ///</summary>
    public interface IStepContainer
    {
        /// <summary>
        /// Gets or sets the Step.
        /// </summary>
        /// <value>The Step.</value>
        Step Step { get; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        IStepContainer Parent { get; }

        /// <summary>
        /// Enumerates over this and each of its ancestors. Reverse the collection to go through the story in correct order
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStepContainer> SelfAndAncestors();

        /// <summary>
        /// Runs the current sequence of Steps against a renderer
        /// </summary>
        /// <param name="renderers"></param>
        void Execute(params IRenderer[] renderers);
    }
}