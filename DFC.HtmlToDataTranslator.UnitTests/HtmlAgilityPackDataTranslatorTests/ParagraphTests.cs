using DFC.HtmlToDataTranslator.Services;
using System.Linq;
using Xunit;

namespace DFC.HtmlToDataTranslator.UnitTests.HtmlAgilityPackDataTranslatorTests
{
    public class ParagraphTests
    {
        [Fact]
        public void CanTranslateParagraphs()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = @"<p>Your duties include</p>";
            var outputValue = translator.Translate(sourceValue);
            Assert.Single(outputValue);
            Assert.Equal("Your duties include", outputValue.First());
        }

        [Fact]
        public void CanTranslateMultipleParagraphs()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = @"<p>p1</p><p>p2</p>";
            var outputValue = translator.Translate(sourceValue);
            Assert.Equal(2, outputValue.Count);
            Assert.Equal("p1", outputValue.First());
            Assert.Equal("p2", outputValue.Last());
        }
    }
}
