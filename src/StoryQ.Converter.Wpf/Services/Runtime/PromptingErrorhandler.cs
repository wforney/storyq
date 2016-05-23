namespace StoryQ.Converter.Wpf.Services.Runtime
{
    using System;
    using System.Windows;

    internal class PromptingErrorhandler : IErrorhandler
    {
        public void HandleExpectedError(Exception e)
        {
            MessageBox.Show(System.Windows.Application.Current.MainWindow, e.Message);
        }
    }
}