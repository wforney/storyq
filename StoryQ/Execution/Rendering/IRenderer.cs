namespace StoryQ.Execution.Rendering
{
    using System.Collections.Generic;

    /// <summary>
    /// Something that can render results
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Renders the results.
        /// </summary>
        /// <param name="results">The results.</param>
        void Render(IEnumerable<Result> results);
    }
}
