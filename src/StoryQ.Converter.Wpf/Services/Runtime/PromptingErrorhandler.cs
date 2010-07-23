using System;
using System.Windows;

namespace StoryQ.Converter.Wpf.Services.Runtime
{
    class PromptingErrorhandler : IErrorhandler
    {
        public void HandleExpectedError(Exception e)
        {
            MessageBox.Show(System.Windows.Application.Current.MainWindow, e.Message);
        }
    }
}