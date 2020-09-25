namespace ThemeEngine.Script
{
    public class StyleItemLine
    {

        public readonly string RootDir;
        public readonly string TargetProperty;
        public readonly string Value;

        public StyleItemLine(string value, string targetProperty, string rootDir)
        {
            Value = value;

            RootDir = rootDir;

            TargetProperty = targetProperty;
        }

    }
}