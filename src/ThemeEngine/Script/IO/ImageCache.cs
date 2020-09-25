using System;
using System.Collections.Generic;
using System.Drawing;

namespace ThemeEngine.Script.IO
{
    internal static class ImageCache
    {

        private static readonly Dictionary<string, Bitmap> Cache = new Dictionary<string, Bitmap>();

        public static void Clear()
        {
            Cache.Clear();
        }

        public static Bitmap Load(string fullPath)
        {
            string cleanPath = new Uri(fullPath).AbsolutePath;
            if (Cache.ContainsKey(cleanPath))
            {
                return Cache[cleanPath];
            }

            Cache[cleanPath] = LoadFile(cleanPath);
            return Cache[cleanPath];
        }

        private static Bitmap LoadFile(string file)
        {
            return (Bitmap) Image.FromFile(file);
        }

    }
}