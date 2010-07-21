using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoryQ.Infrastructure
{
    /// <summary>
    /// Used to mark a constructor as a valid way to kick of a story
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor)]
    public class StoryStarterAttribute:Attribute
    {

        public StoryStarterAttribute(string text)
        {
            Text = text;
        }

        public string Text { get; private set; }
    }
}
