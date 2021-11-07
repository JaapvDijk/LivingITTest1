using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Test
{
    public interface ISetWords : IBeforeSetDictionary {}
    public interface ISetDictonary : IBeforeSort {}
    public interface ISort : IBeforeStat {}

    public interface IBeforeSetwords
    {
        ISetWords SetWords();
    }
    public interface IBeforeSetDictionary
    {
        ISetDictonary SetWordCountDictionary();
    }
    public interface IBeforeSort : IBeforeStat
    {
        ISort SortByValueThenKey();
    }
    public interface IBeforeStat 
    {
        int GetFirstWordCount();
        IList<IWordCount> TakeTop(int top);
        int GetWordCount(string word);
    }

    public class FluentishWordCountAnalyzer : ISetWords, ISetDictonary, ISort
    {   
        private string[] words;
        private Dictionary<string, int> countDictionary;
        
        public ISetWords SetWords(string text)
        {
            text = text.ToLower();
            text = Regex.Replace(text, @"[^a-z ]+", "");

            string[] words = text.Split(new char[] {'.', ',', ' ', '\n'}, 
                              StringSplitOptions.RemoveEmptyEntries);

            this.words = words;

            return this;
        }

        public ISetDictonary SetWordCountDictionary() 
        {
            countDictionary = new Dictionary<string, int>();

            foreach(string word in words) 
            {
                if(countDictionary.ContainsKey(word)) 
                {
                    countDictionary[word] += 1;
                }
                else
                {
                    countDictionary[word] = 1;
                }
            }
            
            return this;
        }

        public ISort SortByValueThenKey() 
        {   
            countDictionary = countDictionary.OrderByDescending(x => x.Value)
                                             .ThenBy(x => x.Key)
                                             .ToDictionary(x => x.Key, x => x.Value);
            
            return this;
        }

        public int GetFirstWordCount()
        {
            return countDictionary.FirstOrDefault().Value;
        }

        public int GetWordCount(string word)
        {
            if (!countDictionary.ContainsKey(word)) 
            {
                return -1;
            }

            return countDictionary[word]; 
        }
        
        public IList<IWordCount> TakeTop(int top)
        {
            return countDictionary.Take(top)
                                  .Select(x => new WordCount(x.Key, x.Value))
                                  .ToList<IWordCount>();
        }
    }

}
