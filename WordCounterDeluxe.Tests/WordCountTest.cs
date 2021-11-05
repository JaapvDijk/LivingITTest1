using System;
using Xunit;
using Test;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// WordCountAnalyzer:
/// GetMostCountedWords: should return the WordCounts in the correct order (first by count then by word)
/// CalculateWordCount: should return the number of times the word occures in the text
/// CalculateHighestWordCount: should return the word occurs the most
/// </summary>

namespace WordCounterDeluxe.Tests
{
    [Collection("TextCollection")]
    public class WordCountAnalyzerTest
    {
        private readonly string _textTwoThe;
        private readonly string _textTwoCar;
        private readonly string _textSpecialCharacters;

        private readonly WordCountAnalyzer _analyzer;

        public WordCountAnalyzerTest(TextFixture textFixture)
        {
            _textTwoThe = textFixture.TextTwoThe;
            _textTwoCar = textFixture.TextTwoCar;
            _textSpecialCharacters = textFixture.TextSpecialCharacters;

            _analyzer = new WordCountAnalyzer();
        }

        [Fact]
        public void CalculateHighestWordCount_TextTwoThe_ReturnEqaul()
        {
            var highestCount = _analyzer.CalculateHighestWordCount(_textTwoThe);
            Assert.Equal(2, highestCount);
        }

        [Fact]
        public void CalculateWordCount_TextTwoThe_ReturnEqaul()
        {
            var wordCount = _analyzer.CalculateWordCount(_textTwoThe, "the");
            Assert.Equal(2, wordCount);
        }

        [Fact]
        public void GetMostCountedWords_TextTwoThe_ReturnEqaul()
        {
            var mostCountedWords = _analyzer.GetMostCountedWords(_textTwoThe, 3);
            Assert.Equal("the", mostCountedWords[0].Word);
            Assert.Equal(2, mostCountedWords[0].Count);
            Assert.Equal("be", mostCountedWords[1].Word);
            Assert.Equal(1, mostCountedWords[1].Count);
            Assert.Equal("car", mostCountedWords[2].Word);
            Assert.Equal(1, mostCountedWords[2].Count);           
        }

        [Fact]
        public void CalculateHighestWordCount_TextTwoCars_ReturnEqual()
        {
            var highestCount = _analyzer.CalculateHighestWordCount(_textTwoCar);
            Assert.Equal(2, highestCount);
        }

        [Fact]
        public void CalculateWordCount_TextTwoCars_ReturnEqual()
        {
            var wordCount = _analyzer.CalculateWordCount(_textTwoCar, "be");
            Assert.Equal(1, wordCount);
        }

        [Fact]
        public void GetMostCountedWords_TextTwoCars_ReturnEqual()
        {
            var mostCountedWords = _analyzer.GetMostCountedWords(_textTwoCar, 2);
            Assert.Equal("car", mostCountedWords[0].Word);
            Assert.Equal(2, mostCountedWords[0].Count);
            Assert.Equal("the", mostCountedWords[1].Word);
            Assert.Equal(2, mostCountedWords[1].Count);
        }

        [Fact]
        public void CalculateHighestWordCount_TextSpecialCharacters_ReturnEqual()
        {
            var highestCount = _analyzer.CalculateHighestWordCount(_textSpecialCharacters);
            Assert.Equal(2, highestCount);
        }

        [Fact]
        public void CalculateWordCount_TextSpecialCharacters_ReturnEqual()
        {
            var wordCount = _analyzer.CalculateWordCount(_textSpecialCharacters, "#");
            Assert.Equal(-1, wordCount);
        }

        [Fact]
        public void GetMostCountedWords_TextSpecialCharacters_ReturnEqual()
        {
            var mostCountedWords = _analyzer.GetMostCountedWords(_textSpecialCharacters, 2);
            Assert.Equal("car", mostCountedWords[0].Word);
            Assert.Equal(2, mostCountedWords[0].Count);
            Assert.Equal("the", mostCountedWords[1].Word);
            Assert.Equal(2, mostCountedWords[1].Count);
        }

        [Fact]
        public void GetMostCountedWords_HighTop_ReturnEqual()
        {
            var mostCountedWords = _analyzer.GetMostCountedWords(_textSpecialCharacters, 999);
            Assert.Equal("car", mostCountedWords[0].Word);
        }
    }
}
