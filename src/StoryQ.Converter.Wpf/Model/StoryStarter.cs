namespace StoryQ.Converter.Wpf.Model
{
    public class StoryStarter
    {
        public Story StoryIs(string s)
        {
            return new Story(s);
        }
    }
}