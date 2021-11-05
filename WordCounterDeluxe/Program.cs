using System;
using System.Collections.Generic;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "The 8| car #in the\r @ car GaraGe.7 sHOuld Be repaired!";

            WordCountAnalyzer analyzer = new WordCountAnalyzer();

            IList<IWordCount> wcs = analyzer.GetMostCountedWords(text, 3);
            foreach (var wc in wcs) {
                Console.WriteLine($"{wc.Word} {wc.Count}");
            }
            Console.WriteLine(analyzer.CalculateWordCount(text, "the"));

            Console.WriteLine(analyzer.CalculateHighestWordCount(text));
        }
    }
}
