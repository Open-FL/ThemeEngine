using System.Linq;

namespace ThemeEngine.Script.Selectors
{
    public class OrSelector : StyleScriptSelector
    {

        private readonly StyleScriptSelector[] Selectors;

        public OrSelector(StyleScriptSelector[] selectors)
        {
            Selectors = selectors;
        }

        public override bool Satisfies(string[] selector)
        {
            return Selectors.Any(x => x.Satisfies(selector));
        }

    }
}