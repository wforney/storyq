using System;
using System.Windows.Input;

namespace StoryQ.Converter.Wpf.ViewModel
{
    public class DelegateCommand : ICommand
    {
        private readonly Action action;

        public DelegateCommand(Action action)
        {
            this.action = action;
        }

        public void Execute(object parameter)
        {
            action();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        //no-op
        public event EventHandler CanExecuteChanged{add{}remove{}}
    }
}