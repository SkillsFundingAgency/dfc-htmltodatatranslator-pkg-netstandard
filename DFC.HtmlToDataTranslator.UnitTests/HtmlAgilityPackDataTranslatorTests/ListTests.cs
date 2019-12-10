using DFC.HtmlToDataTranslator.Services;
using System.Linq;
using Xunit;

namespace DFC.HtmlToDataTranslator.UnitTests.HtmlAgilityPackDataTranslatorTests
{
    public class ListTests
    {
        [Fact]
        public void CanTranslateUnorderedList()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = @"<ul><li>item1</li><li>item2</li><li>item3</li></ul>";
            var outputValue = translator.Translate(sourceValue);
            Assert.Single(outputValue);
            Assert.Equal(" item1; item2; item3", outputValue.First());
        }

        [Fact]
        public void CanTranslateOrderedList()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = @"<ol><li>item1</li><li>item2</li><li>item3</li></ol>";
            var outputValue = translator.Translate(sourceValue);
            Assert.Single(outputValue);
            Assert.Equal(" item1; item2; item3", outputValue.First());
        }
    }
}