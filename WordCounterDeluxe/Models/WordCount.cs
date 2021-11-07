namespace Test
{
    public class WordCount : IWordCount
    {
        public WordCount(string word, int count) 
        {
            Word = word;
            Count = count;
        }

        public string Word { get; }
        public int Count { get; }
    }

}