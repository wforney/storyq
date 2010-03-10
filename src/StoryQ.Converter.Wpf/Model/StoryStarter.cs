using System.ComponentModel;

namespace StoryQ.Converter.Wpf.Model
{
    public class StoryStarter
    {
        [Description("Starts a new Story")]
        public Story StoryIs(string s)
        {
            return new Story(s);
        }
    }
}