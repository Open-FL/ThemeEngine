using System;

namespace ThemeEngine.Script.Selectors
{
    public abstract class StyleScriptSelector
    {

        public abstract bool Satisfies(string[] selectors);

        public static StyleScriptSelector Parse(string selector)
        {
            string right = selector;
            string left;
            char selectorChar;
            (left, selectorChar, right) = right.ReadUntilAndSplit('|', '&', '^');
            if (string.IsNullOrEmpty(right))
            {
                return ValueSelector.ParseValue(left);
            }

            int next = right.ReadUntil('|', '&', '^');
            if (next == -1)
            {
                return Parse(selectorChar, ValueSelector.ParseValue(left), ValueSelector.ParseValue(right));
            }

            return Parse(selectorChar, ValueSelector.ParseValue(left), Parse(right));
        }

        public static StyleScriptSelector Parse(char selector, StyleScriptSelector left, StyleScriptSelector right)
        {
            switch (selector)
            {
                case '&':
                    return new AndSelector(new[] { left, right });
                case '|':
                    return new OrSelector(new[] { left, right });
                case '^':
                    return new XOrSelector(new[] { left, right });
                default:
                    throw new ArgumentException($"There is no {selector} selector.");
            }
        }

    }
}