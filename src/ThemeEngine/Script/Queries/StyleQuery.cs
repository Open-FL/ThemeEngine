namespace ThemeEngine.Script.Queries
{
    public class StyleQuery
    {

        public readonly char comparisonType;
        public readonly string left;
        public readonly string right;

        public StyleQuery(string query)
        {
            (left, comparisonType, right) = query.ReadUntilAndSplit('=', '<', '>');
        }

    }
}