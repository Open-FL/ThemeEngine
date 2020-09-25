using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace ThemeEngine.Script.IO
{
    internal static class FontCache
    {

        private static readonly InstalledFontCollection InstalledFonts = new InstalledFontCollection();
        private static readonly PrivateFontCollection LoadedFonts = new PrivateFontCollection();
        private static readonly Dictionary<string, string> Cache = new Dictionary<string, string>();

        public static void Clear()
        {
            Cache.Clear();
        }

        private static FontFamily GetFontByFile(string file)
        {
            if (Cache.ContainsKey(file))
            {
                return LoadedFonts.Families.First(x => x.Name == Cache[file]);
            }

            LoadedFonts.AddFontFile(file);
            FontFamily fam = LoadedFonts.Families.Last();
            Cache[file] = fam.Name;
            return fam;
        }

        public static Font GetFont(Font baseFont, string statement)
        {
            string[] parts = statement.Split('|');
            bool isName = parts[0].StartsWith("\"") && parts[0].EndsWith("\"");
            bool isNoName = isName && parts[0].Trim().Length == 2 || !isName && parts[0].Trim().Length == 0;
            bool hasSize = parts.Length > 1 && !string.IsNullOrEmpty(parts[1]);
            if (isName)
            {
                FontFamily fam = isNoName ? baseFont.FontFamily : GetFontByName(parts[0]);
                if (hasSize)
                {
                    return new Font(fam, float.Parse(parts[1]));
                }

                return new Font(fam, baseFont.Size);
            }
            else
            {
                FontFamily fam = isNoName ? baseFont.FontFamily : GetFontByFile(parts[0]);
                if (hasSize)
                {
                    return new Font(fam, float.Parse(parts[1]));
                }

                return new Font(fam, baseFont.Size);
            }
        }


        private static FontFamily GetFontByName(string statement)
        {
            string cleanName = statement.Remove(statement.Length - 1, 1).Remove(0, 1);
            return InstalledFonts.Families.FirstOrDefault(x => x.Name == cleanName) ??
                   LoadedFonts.Families.First(x => x.Name == cleanName);
        }

        public static Font ChangeFontFamily(this Font font, FontFamily fam)
        {
            return new Font(fam, font.Size, font.Style, font.Unit, font.GdiCharSet);
        }

        public static Font ChangeFontSize(this Font font, float px)
        {
            return new Font(font.FontFamily, px);
        }

    }
}