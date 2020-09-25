using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using ThemeEngine.Script.Queries;

namespace ThemeEngine.Script
{
    public static class ScriptImportResolver
    {

        private static readonly Dictionary<char, Func<decimal, decimal, bool>> ComparisonTypes =
            new Dictionary<char, Func<decimal, decimal, bool>>
            {
                {
                    '=', (i, i1) =>
                             i == i1
                },
                {
                    '<', (i, i1) =>
                             i < i1
                },
                {
                    '>', (i, i1) =>
                             i > i1
                }
            };

        private static readonly Dictionary<string, Func<Form, decimal>> FormKeywords =
            new Dictionary<string, Func<Form, decimal>>
            {
                { "width", form => form.Width },
                { "height", form => form.Height }
            };

        private static readonly Dictionary<string, Func<Form, decimal>> ScreenKeywords =
            new Dictionary<string, Func<Form, decimal>>
            {
                {
                    "width", form =>
                                 form.FindCurrentMonitor().Bounds.Width
                },
                {
                    "height", form =>
                                  form.FindCurrentMonitor().Bounds.Height
                }
            };

        private static Screen FindCurrentMonitor(this Form form)
        {
            return Screen.FromRectangle(new Rectangle(form.Location, form.Size));
        }

        public static bool GetData(StyleQuery importLine)
        {
            string[] path = importLine.left.Split(new[] { '.', ':' }, StringSplitOptions.RemoveEmptyEntries);
            string form = path[0];
            Form target = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x.Name == form);
            if (target == null)
            {
                return false;
            }

            string control = path[1];
            Dictionary<string, Func<Form, decimal>> keywords = FormKeywords;
            if (control == "screen")
            {
                control = path[2];
                keywords = ScreenKeywords;
            }

            return ComparisonTypes[importLine.comparisonType](
                                                              keywords[control](target),
                                                              decimal.Parse(importLine.right)
                                                             );
        }

    }
}