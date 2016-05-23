// ***********************************************************************
// Assembly         : StoryQ
// Last Modified By : William Forney
// Last Modified On : 05-22-2016
// ***********************************************************************
// <copyright file="IRenderer.cs" company="">
//     2010 robfe & toddb
// </copyright>
// ***********************************************************************
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