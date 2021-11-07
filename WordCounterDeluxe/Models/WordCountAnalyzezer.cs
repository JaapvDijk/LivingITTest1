using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Test
{
    public class WordCountAnalyzer : IWordCountAnalyzer
    {
        public int CalculateHighestWordCount(string text)
        {
            Dictionary<string, int> wordCountDictionary = GetWordCountDictionary(text);            
            IOrderedEnumerable<KeyValuePair<string, int>> wordCount = GetWordCountSorted(wordCountDictionary);

            return wordCount.FirstOrDefault().Value;
        }

        public int CalculateWordCount(string text, string word)
        {   
            word = word.ToLower();

            Dictionary<string, int> wordCountDict = GetWordCountDictionary(text);

            if (!wordCountDict.ContainsKey(word)) 
            {
                return -1;
            }

            return wordCountDict[word]; 
        }

        public IList<IWordCount> GetMostCountedWords(string text, int top)
        {
            Dictionary<string, int> wordCountDictionary = GetWordCountDictionary(text);
            IOrderedEnumerable<KeyValuePair<string, int>> wordCountSorted = GetWordCountSorted(wordCountDictionary);

            IEnumerable<KeyValuePair<string, int>> topN = wordCountSorted.Take(top);

            IList<IWordCount> wordCountTop = topN.Select(x => new WordCount(x.Key, x.Value))
                                                    .ToList<IWordCount>();

            return wordCountTop;
        }

        private Dictionary<string, int> GetWordCountDictionary(string text) 
        {
            string[] words = GetWordsFromText(text);

            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            foreach(string word in words) 
            {
                if(wordCount.ContainsKey(word)) 
                {
                    wordCount[word] += 1;
                }
                else
                {
                    wordCount[word] = 1;
                }
            }
            
            return wordCount;
        }

        private IOrderedEnumerable<KeyValuePair<string, int>> GetWordCountSorted(Dictionary<string, int> wordCountDictionary) 
        {
            return wordCountDictionary.OrderByDescending(x => x.Value)
                                      .ThenBy(x => x.Key);
        }

        private string[] GetWordsFromText(string text)
        {
            text = text.ToLower();
            text = Regex.Replace(text, @"[^a-z ]+", "");

            string[] words = text.Split(new char[] {'.', ',', ' ', '\n'}, 
                              StringSplitOptions.RemoveEmptyEntries);

            return words;
        }
    }
}