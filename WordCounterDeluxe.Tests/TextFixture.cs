using System;
using System.IO;
using System.Security.Cryptography;
using Xunit;

namespace WordCounterDeluxe.Tests
{
    [CollectionDefinition("TextCollection")]
    public class ClientCollection : ICollectionFixture<TextFixture> { }

    public class TextFixture
    {
        public readonly string TextTwoThe;
        public readonly string TextTwoCar;
        public readonly string TextSpecialCharacters;

        public TextFixture()
        {
            TextTwoThe = "The car in the garage should be repaired";
            TextTwoCar = "The car in the car garage should be repaired";
            TextSpecialCharacters = "The #car #in the @ car GaraGe.7 \nsHOuld ## Be repaired! 7 #";

            //Or load config/data file
        }

    }

    
}
