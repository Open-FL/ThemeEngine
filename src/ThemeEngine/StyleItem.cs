using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

using ThemeEngine.Script;
using ThemeEngine.Script.IO;

namespace ThemeEngine
{
    public class StyleItem
    {

        private static readonly Dictionary<string, object> objectCache = new Dictionary<string, object>();
        private static readonly ColorConverter converter = new ColorConverter();
        public readonly string[] Selectors;
        public object Target;

        public StyleItem(string[] selectors, object target)
        {
            Selectors = new[] { "", target.GetType().Name };
            if (selectors != null && selectors.Length != 0)
            {
                Selectors = new[] { "", target.GetType().Name }.Concat(selectors).Distinct().ToArray();
            }

            Target = target;
        }

        public virtual void Apply(StyleItemLine item)
        {
            MemberInfo mi = FindMember(item.TargetProperty);
            if (mi is FieldInfo fi)
            {
                object current = fi.GetValue(Target);
                if (TryParseValue(fi.FieldType, current, item, out object newValue))
                {
                    fi.SetValue(Target, newValue);
                }
            }
            else if (mi is PropertyInfo pi)
            {
                object current = pi.GetValue(Target);
                if (TryParseValue(pi.PropertyType, current, item, out object newValue))
                {
                    pi.SetValue(Target, newValue);
                }
            }
        }

        public static bool TryParseExpression(string expr, out string result)
        {
            try
            {
                string cleanExpr = expr.Remove(expr.Length - 1, 1).Remove(0, 1);
                result = StyleManager.Evaluator.Parse(cleanExpr).Value.ToString();
                return true;
            }
            catch (Exception e)
            {
                result = null;
                return false;
            }
        }

        private bool TryParseValue(Type targetType, object currentValue, StyleItemLine item, out object result)
        {
            string value = item.Value;

            if (item.Value.StartsWith("(") && item.Value.EndsWith(")") && TryParseExpression(value, out string v))
            {
                value = v;
            }

            try
            {
                if (targetType == typeof(Color))
                {
                    if (objectCache.ContainsKey(value) && objectCache[value] is Color)
                    {
                        result = objectCache[value];
                    }
                    else
                    {
                        result = ParseColor(value);
                        objectCache[value] = result;
                    }
                }

                if (typeof(Image).IsAssignableFrom(targetType))
                {
                    string path = Path.Combine(item.RootDir, value);
                    result = ImageCache.Load(path);
                }

                if (typeof(Font).IsAssignableFrom(targetType))
                {
                    result = FontCache.GetFont((Font) currentValue, value);
                }

                if (targetType.IsEnum)
                {
                    if (objectCache.ContainsKey(value) && objectCache[value].GetType() == targetType)
                    {
                        result = objectCache[value];
                    }
                    else
                    {
                        result = Enum.Parse(targetType, value, true);
                        objectCache[value] = result;
                    }
                }

                if (objectCache.ContainsKey(value) && objectCache[value].GetType() == targetType)
                {
                    result = objectCache[value];
                }
                else
                {
                    result = ParseLiteral(value, targetType);
                    objectCache[value] = result;
                }

                return true;
            }
            catch (Exception e)
            {
                result = null;
                return false;
            }
        }

        private Color ParseColor(string value)
        {
            return (Color) converter.ConvertFromString(value);
        }

        private object ParseLiteral(string value, Type target)
        {
            return Convert.ChangeType(value, target);
        }

        private MemberInfo FindMember(string target)
        {
            return Target.GetType().GetMember(target).FirstOrDefault();
        }

    }
}