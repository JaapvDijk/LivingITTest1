using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Test
{
    public interface IFluentWordCounter
    {
        IDataPrep Set_words(string text);
    }
    public interface IDataPrep
    {
        ISetWordCount SetWord_countDictionary();
    }
    public interface ISetWordCount : ISortAble
    {
        ISortAble SortByValueThenKey();
    }
    public interface ISortAble 
    {
        int GetFirstWordCount();
        IList<IWordCount> TakeTop(int top);
        int GetWordCount(string word);
    }

    public class FluentishWordCountAnalyzer : IFluentWordCounter, IDataPrep, ISetWordCount, ISortAble
    {   
        private string[] _words;
        private Dictionary<string, int> _countDictionary;

        public IDataPrep Set_words(string text)
        {
            text = text.ToLower();
            text = Regex.Replace(text, @"[^a-z ]+", "");

            string[] _words = text.Split(new char[] {'.', ',', ' ', '\n'}, 
                              StringSplitOptions.RemoveEmptyEntries);

            this._words = _words;

            return this;
        }

        public ISetWordCount SetWord_countDictionary() 
        {
            _countDictionary = new Dictionary<string, int>();

            foreach(string word in _words) 
            {
                if(_countDictionary.ContainsKey(word)) 
                {
                    _countDictionary[word] += 1;
                }
                else
                {
                    _countDictionary[word] = 1;
                }
            }
            
            return this;
        }

        public ISortAble SortByValueThenKey() 
        {   
            _countDictionary = _countDictionary.OrderByDescending(x => x.Value)
                                             .ThenBy(x => x.Key)
                                             .ToDictionary(x => x.Key, x => x.Value);
            
            return this;
        }

        public int GetFirstWordCount()
        {
            return _countDictionary.FirstOrDefault().Value;
        }

        public int GetWordCount(string word)
        {
            if (!_countDictionary.ContainsKey(word)) 
            {
                return -1;
            }

            return _countDictionary[word]; 
        }
        
        public IList<IWordCount> TakeTop(int top)
        {
            return _countDictionary.Take(top)
                                  .Select(x => new WordCount(x.Key, x.Value))
                                  .ToList<IWordCount>();
        }
    }

}
