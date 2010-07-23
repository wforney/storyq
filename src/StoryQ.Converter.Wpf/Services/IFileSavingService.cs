namespace StoryQ.Converter.Wpf.Services
{
    public interface IFileSavingService
    {
        string PromptForDirectory(string message);

        /// <summary>
        /// copies all the dll and xml files from the current directory and it's subdirectories into targetDirectory
        /// </summary>
        void CopyLibFiles(string targetDirectory);
    }
}
