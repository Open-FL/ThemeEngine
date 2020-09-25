using System;
using System.Collections.Generic;
using System.Linq;

using ThemeEngine.Script.Options;
using ThemeEngine.Script.Selectors;

namespace ThemeEngine.Script
{
    internal static class StyleScriptParser
    {

        internal static StyleScript Parse(string content, string rootDir)
        {
            string[] blocks = content.Split('{', '}').Select(x => x.Trim()).ToArray();

            List<StyleScriptDataObject> data = new List<StyleScriptDataObject>();
            List<StyleOption> styleOptions = new List<StyleOption>();
            List<StyleScriptImportDataObject> imports = new List<StyleScriptImportDataObject>();

            List<string> functionStatements = new List<string>();
            (string, string[]) current = ("", null);
            List<(string, string[])> parsedBlocks = new List<(string, string[])>();
            for (int i = 0; i < blocks.Length; i++)
            {
                if (i % 2 == 0)
                {
                    string[] statements = blocks[i].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                                   .Select(x => x.Trim()).ToArray();
                    if (i < blocks.Length - 1)
                    {
                        current = (statements.LastOrDefault() ?? "", null);
                    }

                    functionStatements.AddRange(
                                                statements.Where(x => x.StartsWith("@"))
                                                          .Select(x => x.Remove(0, 1).Trim()).ToArray()
                                               );

                    //string[] importStatements = functionStatements.Where(x => x.StartsWith("import")).Select(x => x.Remove(0, "import".Length).Trim()).ToArray();
                    //string[] styleOptionStatements = functionStatements.Where(x => x.StartsWith("option")).Select(x => x.Remove(0, "option".Length).Trim()).ToArray();
                    //imports.AddRange(importStatements.Select(x => new StyleScriptImportDataObject(x, rootDir)));
                }
                else
                {
                    string[] statements = blocks[i].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                                   .Select(x => x.Trim()).ToArray();
                    current.Item2 = statements;
                    parsedBlocks.Add(current);

                    //data.Add(new StyleScriptDataObject(currentSelector, new List<string>(statements)));
                }
            }

            string[] importStatements = functionStatements
                                        .Where(x => x.StartsWith("import"))
                                        .Select(x => x.Remove(0, "import".Length).Trim()).ToArray();
            foreach (string importStatement in importStatements)
            {
                imports.Add(new StyleScriptImportDataObject(importStatement, rootDir));
            }

            string[] styleOptionStatements = functionStatements
                                             .Where(x => x.StartsWith("option"))
                                             .Select(x => x.Remove(0, "option".Length).Trim()).ToArray();
            foreach (string styleStatement in styleOptionStatements)
            {
                styleOptions.Add(new StyleOption(styleStatement));
            }

            foreach ((string, string[]) parsedBlock in parsedBlocks)
            {
                data.Add(
                         new StyleScriptDataObject(
                                                   StyleScriptSelector.Parse(parsedBlock.Item1),
                                                   parsedBlock.Item2.ToList()
                                                  )
                        );
            }

            return new StyleScript(data, imports, styleOptions, rootDir);
        }

        internal static int ReadUntil(this string str, params char[] characters)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (characters.Any(x => x == str[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        internal static (string left, char match, string right) ReadUntilAndSplit(
            this string str, params char[] characters)
        {
            int idx = ReadUntil(str, characters);
            if (idx == -1)
            {
                return (str, '\0', "");
            }

            return (str.Substring(0, idx).Trim(), str[idx], str.Substring(idx + 1, str.Length - (idx + 1)).Trim());
        }

    }
}