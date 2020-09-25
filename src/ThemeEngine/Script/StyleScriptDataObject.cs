using System.Collections.Generic;

using ThemeEngine.Script.Selectors;

namespace ThemeEngine.Script
{
    public class StyleScriptDataObject
    {

        public readonly StyleScriptSelector Selector;

        public StyleScriptDataObject(StyleScriptSelector selector, List<string> content)
        {
            Selector = selector;
            Content = content;
        }

        public virtual List<string> Content { get; }

        public StyleScript Owner { get; private set; }

        public void SetOwner(StyleScript owner)
        {
            Owner = owner;
        }

    }
}