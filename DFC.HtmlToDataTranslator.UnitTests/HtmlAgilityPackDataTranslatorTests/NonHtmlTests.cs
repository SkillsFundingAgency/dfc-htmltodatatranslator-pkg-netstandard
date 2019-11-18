using DFC.HtmlToDataTranslator.Services;
using System.Linq;
using Xunit;

namespace DFC.HtmlToDataTranslator.UnitTests.HtmlAgilityPackDataTranslatorTests
{
    public class NonHtmlTests
    {
        [Fact]
        public void NonHtmlContentIsReturnedAsIs()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = "some text";
            var outputValue = translator.Translate(sourceValue);
            Assert.Single(outputValue);
            Assert.Equal(sourceValue, outputValue.First());
        }

        [Fact]
        public void BlankStringReturnsEmptyList()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = string.Empty;
            var outputValue = translator.Translate(sourceValue);
            Assert.Empty(outputValue);
        }
    }
}
