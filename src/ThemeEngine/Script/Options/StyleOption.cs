namespace ThemeEngine.Script.Options
{
    public class StyleOption
    {

        public readonly string DefaultValue;

        public readonly string Keyword;
        private string value;

        public StyleOption(string line)
        {
            string[] parts = line.Split(':');
            Keyword = parts[0].Trim();
            DefaultValue = parts[1].Trim();
        }

        public StyleScript Owner { get; private set; }

        public bool HasChanged => value != null && value != DefaultValue;

        public string Value
        {
            get => value ?? DefaultValue;
            set => this.value = value;
        }


        public void SetOwner(StyleScript owner)
        {
            Owner = owner;
        }

    }
}