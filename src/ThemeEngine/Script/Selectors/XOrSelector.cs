using System.Linq;

namespace ThemeEngine.Script.Selectors
{
    public class XOrSelector : StyleScriptSelector
    {

        private readonly StyleScriptSelector[] Selectors;

        public XOrSelector(StyleScriptSelector[] selectors)
        {
            Selectors = selectors;
        }

        public override bool Satisfies(string[] selector)
        {
            return Selectors.Count(x => x.Satisfies(selector)) == 1;
        }

    }
}