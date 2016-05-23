namespace StoryQ.Converter.Wpf.ViewModel
{
    using System;
    using System.Windows.Input;

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

        public bool CanExecute(object parameter) => true;

        //no-op
        public event EventHandler CanExecuteChanged{add{}remove{}}
    }
}