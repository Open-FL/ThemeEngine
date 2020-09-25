using System;

namespace ThemeEngine
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class StyleSelectorAttribute : Attribute
    {

        public readonly string Selector;

        public StyleSelectorAttribute(string selector)
        {
            Selector = selector;
        }

    }
}