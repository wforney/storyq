namespace StoryQ.Converter.Wpf.ViewModel
{
    using System.ComponentModel;

    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void FirePropertyChanged(string s)
        {
            PropertyChangedEventHandler changed = PropertyChanged;
            if (changed != null)
            {
                changed(this, new PropertyChangedEventArgs(s));
            }
        }
    }
}