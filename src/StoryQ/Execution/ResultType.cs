namespace StoryQ.Execution
{
    /// <summary>
    /// The different outcomes of each Narrative run
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// The narrative was not something that can be executed
        /// </summary>
        NotExecutable,

        /// <summary>
        /// The narrative passed
        /// </summary>
        Passed,

        /// <summary>
        /// The narrative was pending (more development required)
        /// </summary>
        Pending,

        /// <summary>
        /// There was an unexpected exception or an assertion failure
        /// </summary>
        Failed
    }
}