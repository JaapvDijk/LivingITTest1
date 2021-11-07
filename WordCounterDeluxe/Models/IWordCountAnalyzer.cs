using System.Collections.Generic;

namespace Test
{
    public interface IWordCountAnalyzer
    {
        int CalculateHighestWordCount(string text);

        int CalculateWordCount(string text, string word);

        IList<IWordCount> GetMostCountedWords(string text, int top);
    }

}