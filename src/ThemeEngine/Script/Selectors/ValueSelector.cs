using System;
using System.Linq;

namespace ThemeEngine.Script.Selectors
{
    public class ValueSelector : StyleScriptSelector
    {

        private readonly bool inverted;

        private readonly Func<string> value;

        public ValueSelector(bool inverted, Func<string> value)
        {
            this.inverted = inverted;
            this.value = value;
        }

        public override bool Satisfies(string[] selector)
        {
            string val = value();
            if (inverted)
            {
                return !selector.Contains(val);
            }

            return selector.Contains(val);
        }


        public static ValueSelector ParseValue(string value)
        {
            bool inverted = false;
            string val = value.Trim();
            if (val.StartsWith("!"))
            {
                val = value.Substring(1, value.Length - 1);
                inverted = true;
            }

            if (val.StartsWith("%") && val.EndsWith("%"))
            {
                string key = val.Remove(val.Length - 1, 1).Remove(0, 1);
                return new ValueSelector(
                                         inverted,
                                         () =>
                                             StyleManager.StyleOptions.First(x => x.Keyword == key).Value
                                        );
            }

            if (val.StartsWith("(") && val.EndsWith(")") && StyleItem.TryParseExpression(value, out string v))
            {
                val = v;
            }

            return new ValueSelector(inverted, () => val);
        }

    }
}