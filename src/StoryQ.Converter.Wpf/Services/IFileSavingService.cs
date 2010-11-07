namespace StoryQ.Converter.Wpf.Services
{
    public interface IFileSavingService
    {

        /// <summary>
        /// asks the user for a directory
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        string PromptForDirectory(string message);

        /// <summary>
        /// copies all the dll and xml files from the current directory and it's subdirectories into targetDirectory
        /// </summary>
        void CopyLibFiles(string targetDirectory);
    }
}
