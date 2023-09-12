namespace SecretWordConsole
{
    public class WordInfo
    {
        public string Word { get; set; }
        public string Definition { get; set; }

        public WordInfo() { }

        public WordInfo(string word, string definition) 
        { 
            Word = word; 
            Definition = definition; 
        }
    }
}
