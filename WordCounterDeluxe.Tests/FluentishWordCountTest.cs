using Xunit;
using Test;

namespace WordCounterDeluxe.Tests
{
    [Collection("TextCollection")]
    public class FluentishWordCountTest
    {
        private readonly string _textSpecialCharacters;

        private readonly FluentishWordCountAnalyzer _analyzer;

        public FluentishWordCountTest(TextFixture textFixture)
        {
            _textSpecialCharacters = textFixture.TextSpecialCharacters;

            _analyzer = new FluentishWordCountAnalyzer();
        }
        [Fact]
        public void MostOccuringWord_TextSpecialCharacters_ReturnEqual()
        {   
            var MostOccuringWord = _analyzer.SetWords(_textSpecialCharacters)
                                            .SetWordCountDictionary()
                                            .SortByValueThenKey()
                                            .GetFirstWordCount();

            Assert.Equal(2, MostOccuringWord);
        }

        [Fact]
        public void singleWordCount_TextSpecialCharacters_ReturnEqual()
        {
            var singleWordCount = _analyzer.SetWords(_textSpecialCharacters)
                                .SetWordCountDictionary()
                                .GetWordCount("car");   

            Assert.Equal(2, singleWordCount);
        }

        [Fact]
        public void topWordCount_TextSpecialCharacters_ReturnEqual()
        {
            var topWordCount = _analyzer.SetWords(_textSpecialCharacters)
                                        .SetWordCountDictionary()
                                        .SortByValueThenKey()
                                        .TakeTop(3);
        
            Assert.Equal("car", topWordCount[0].Word);
        }
    }
}