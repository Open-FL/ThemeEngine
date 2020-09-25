using System.IO;
using System.Linq;

using ThemeEngine.Script.Queries;

namespace ThemeEngine.Script
{
    public class StyleScriptImportDataObject
    {

        public readonly StyleQuery ImportCondition;

        public readonly StyleScript[] ImportFiles;

        public StyleScriptImportDataObject(string statement, string rootDir)
        {
            string[] parts = statement.Split('(', ')');
            string[] files;
            if (parts.Length == 3)
            {
                ImportCondition = new StyleQuery(parts[1]);
                files = parts.Skip(2).ToArray();
            }
            else
            {
                ImportCondition = null;
                files = parts;
            }

            ImportFiles = files.Select(
                                       x =>
                                       {
                                           string path = Path.Combine(rootDir, x.Trim());
                                           return StyleScriptParser.Parse(File.ReadAllText(path), rootDir);
                                       }
                                      ).ToArray();
        }

    }
}