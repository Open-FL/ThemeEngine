using System.Linq;

namespace ThemeEngine.Script.Selectors
{
    public class AndSelector : StyleScriptSelector
    {

        private readonly StyleScriptSelector[] Selectors;

        public AndSelector(StyleScriptSelector[] selectors)
        {
            Selectors = selectors;
        }

        public override bool Satisfies(string[] selector)
        {
            return Selectors.All(x => x.Satisfies(selector));
        }

    }
}