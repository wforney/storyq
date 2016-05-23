// ***********************************************************************
// Assembly         : StoryQ
// Last Modified By : William Forney
// Last Modified On : 05-22-2016
// ***********************************************************************
// <copyright file="ResultType.cs" company="">
//     2010 robfe and toddb
// </copyright>
// ***********************************************************************
namespace StoryQ.Execution
{
    /// <summary>
    /// The different outcomes of each Step run
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// The Step was not something that can be executed
        /// </summary>
        NotExecutable,

        /// <summary>
        /// The Step passed
        /// </summary>
        Passed,

        /// <summary>
        /// The Step was pending (more development required)
        /// </summary>
        Pending,

        /// <summary>
        /// There was an unexpected exception or an assertion failure
        /// </summary>
        Failed
    }
}