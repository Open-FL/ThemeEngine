using System.Collections.Generic;
using System.Linq;

using ThemeEngine.Script.Options;

namespace ThemeEngine.Script
{
    public class StyleScript
    {

        private readonly List<StyleScriptDataObject> data = new List<StyleScriptDataObject>();

        private readonly List<StyleScriptImportDataObject> imports;
        public readonly string RootDir;
        private readonly List<StyleOption> styleOptions;
        private List<StyleScriptDataObject> fullData;
        private List<StyleOption> fullStyleOptions;


        internal StyleScript(
            List<StyleScriptDataObject> data, List<StyleScriptImportDataObject> imports, List<StyleOption> options,
            string rootDir)
        {
            styleOptions = options;
            styleOptions.ForEach(x => x.SetOwner(this));
            this.data = data;
            this.data.ForEach(x => x.SetOwner(this));
            this.imports = imports;
            RootDir = rootDir;
        }

        public List<StyleOption> FullStyleOptions
        {
            get
            {
                if (fullStyleOptions != null)
                {
                    return fullStyleOptions;
                }

                fullStyleOptions = new List<StyleOption>(styleOptions);
                foreach (StyleScriptImportDataObject import in imports)
                {
                    if (ScriptImportResolver.GetData(import.ImportCondition))
                    {
                        foreach (StyleScript importFile in import.ImportFiles)
                        {
                            fullStyleOptions.AddRange(importFile.FullStyleOptions);
                        }
                    }
                }

                return fullStyleOptions;
            }
        }

        public List<StyleScriptDataObject> FullData
        {
            get
            {
                if (fullData != null)
                {
                    return fullData;
                }

                fullData = new List<StyleScriptDataObject>(data);
                foreach (StyleScriptImportDataObject import in imports)
                {
                    if (ScriptImportResolver.GetData(import.ImportCondition))
                    {
                        foreach (StyleScript importFile in import.ImportFiles)
                        {
                            fullData.AddRange(importFile.FullData);
                        }
                    }
                }

                return fullData;
            }
        }


        public void ClearCache()
        {
            fullData = null;
        }


        public StyleItemLine[] GetApplicableBody(string[] selectors)
        {
            List<StyleItemLine> ret = new List<StyleItemLine>();

            FullData.Where(
                           x =>
                               x.Selector.Satisfies(selectors)
                          )
                    .Select(
                            x =>
                                LoadTheme(x.Content.ToArray(), x.Owner.RootDir)
                           ).ToList()
                    .ForEach(
                             x =>
                                 ret.AddRange(x)
                            );


            return ret.ToArray();
        }

        private static StyleItemLine[] LoadTheme(string[] lines, string rootDir)
        {
            List<StyleItemLine> ret = new List<StyleItemLine>();
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                string[] parts = line.Split(':');
                ret.Add(new StyleItemLine(ReplaceOptionKeys(parts[1].Trim()), parts[0].Trim(), rootDir));
            }

            return ret.ToArray();
        }

        private static string ReplaceOptionKeys(string item)
        {
            if (item == null || item.Length <= 2)
            {
                return item;
            }

            string cleanItem = item.Remove(item.Length - 1, 1).Remove(0, 1);
            return StyleManager.StyleOptions.FirstOrDefault(x => x.Keyword == cleanItem)?.Value ?? item;
        }

    }
}