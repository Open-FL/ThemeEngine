using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using ThemeEngine.Script;
using ThemeEngine.Script.Options;

using Utility.Expressions;
using Utility.Expressions.Enums;
using Utility.Expressions.Interfaces;

namespace ThemeEngine
{
    public static class StyleManager
    {
        internal static readonly Evaluator Evaluator = new Evaluator(ParserSyntax.CSharp);
        private static readonly List<StyleItem> themeItems = new List<StyleItem>();
        private static StyleScript CurrentStyle;

        private class StyleEvaluatorBridge: IVariableBag
        {

            private class FormTypedVariable : IEvalTypedValue
            {

                public object Value { get; }

                public event ValueChangedEventHandler ValueChanged;

                public Type SystemType { get; }

                public EvalType EvalType => EvalType.Object;

                public FormTypedVariable(Form variable)
                {
                    if (variable == null)
                        return;
                    Value = variable;
                    SystemType = variable.GetType();
                    variable.Invalidated += (sender, args) => ValueChanged(sender, args);
                }

            }
            public IEvalTypedValue GetVariable(string varname)
            {
                return new FormTypedVariable(Application.OpenForms.Cast<Form>().FirstOrDefault(x => x.Name == varname));
            }

        }

        static StyleManager()
        {
            Evaluator.AddEnvironmentFunctions(new StyleEvaluatorBridge());
        }

        public static List<StyleOption> StyleOptions => CurrentStyle?.FullStyleOptions ?? new List<StyleOption>();

        public static event Action OnReload;

        public static void ApplyThemeFile(string file)
        {
            string content = File.ReadAllText(file);
            ApplyTheme(content, Path.GetDirectoryName(Path.GetFullPath(file)));
        }

        public static void ApplyTheme(string content, string rootDir)
        {
            CurrentStyle = StyleScriptParser.Parse(content, rootDir);

            Reload();
        }

        public static void Reload()
        {
            if (CurrentStyle == null)
            {
                return;
            }

            CurrentStyle.ClearCache();
            for (int i = themeItems.Count - 1; i >= 0; i--)
            {
                StyleItem styleItem = themeItems[i];
                StyleItemLine[] theme = CurrentStyle.GetApplicableBody(styleItem.Selectors);
                foreach (StyleItemLine styleItemLine in theme)
                {
                    styleItem.Apply(styleItemLine);
                }
            }

            OnReload?.Invoke();
        }


        public static void RegisterControls(Form form, params string[] selectors)
        {
            form.ResizeEnd += (sender, args) => Reload();
            List<string> sels = selectors.ToList();
            if (!sels.Contains("form"))
            {
                sels.Add("form");
            }

            themeItems.Add(new StyleItem(sels.ToArray(), form));

            List<StyleItem> items = GetControls(form, selectors);
            themeItems.AddRange(items);
            Reload();
        }

        public static void RegisterControl(Control control, params string[] selectors)
        {
            themeItems.Add(new StyleItem(selectors, control));


            if (control is DataGridView dgv)
            {
                List<StyleItem> ret = RegisterDataGridView(dgv, selectors);

                foreach (StyleItem styleItem in ret)
                {
                    themeItems.Add(styleItem);
                }
            }


            Reload();
        }

        private static List<StyleItem> RegisterDataGridView(DataGridView dgv, string[] selectors)
        {
            List<StyleItem> ret = new List<StyleItem>();
            string[] cellSelectorTags =
                    selectors.Concat(new[] { "cell" }).Distinct().ToArray();
            ret.Add(
                    new StyleItem(cellSelectorTags.Length == 0 ? null : cellSelectorTags, dgv.DefaultCellStyle)
                   );
            string[] cCellHeaderSelectorTags = selectors
                                               .Concat(new[] { "c-cell-header", "cell-header" }).Distinct()
                                               .ToArray();
            ret.Add(
                    new StyleItem(
                                  cCellHeaderSelectorTags.Length == 0 ? null : cCellHeaderSelectorTags,
                                  dgv.ColumnHeadersDefaultCellStyle
                                 )
                   );
            string[] rCellHeaderSelectorTags = selectors
                                               .Concat(new[] { "r-cell-header", "cell-header" }).Distinct()
                                               .ToArray();
            ret.Add(
                    new StyleItem(
                                  rCellHeaderSelectorTags.Length == 0 ? null : rCellHeaderSelectorTags,
                                  dgv.RowHeadersDefaultCellStyle
                                 )
                   );
            string[] arCellHeaderSelectorTags = selectors
                                                .Concat(new[] { "ar-cell-header", "cell-header" }).Distinct()
                                                .ToArray();
            ret.Add(
                    new StyleItem(
                                  arCellHeaderSelectorTags.Length == 0 ? null : arCellHeaderSelectorTags,
                                  dgv.AlternatingRowsDefaultCellStyle
                                 )
                   );
            string[] rcCellHeaderSelectorTags = selectors.Concat(new[] { "rows-cell", "cell" })
                                                .Distinct().ToArray();
            ret.Add(
                    new StyleItem(
                                  rcCellHeaderSelectorTags.Length == 0 ? null : rcCellHeaderSelectorTags,
                                  dgv.RowsDefaultCellStyle
                                 )
                   );
            return ret;
        }

        private static List<StyleItem> GetControls(Form form, string[] baseSelectors)
        {
            List<StyleItem> ret = new List<StyleItem>();

            IEnumerable<FieldInfo> fis = form
                                         .GetType().GetFields(
                                                              BindingFlags.NonPublic |
                                                              BindingFlags.Public |
                                                              BindingFlags.Instance
                                                             ).Where(
                                                                     x => typeof(Control).IsAssignableFrom(x.FieldType)
                                                                    );
            foreach (FieldInfo fieldInfo in fis)
            {
                IEnumerable<StyleSelectorAttribute> selectors = fieldInfo.GetCustomAttributes<StyleSelectorAttribute>();

                Control ctrl = (Control) fieldInfo.GetValue(form);
                if (ctrl == null)
                {
                    continue;
                }

                string[] selectorTags = selectors.Select(x => x.Selector).Concat(baseSelectors).Distinct().ToArray();

                if (ctrl is DataGridView dgv)
                {
                    ret.AddRange(RegisterDataGridView(dgv, selectorTags));
                }

                ret.Add(new StyleItem(selectorTags.Length == 0 ? null : selectorTags, ctrl));
            }

            return ret;
        }

    }
}