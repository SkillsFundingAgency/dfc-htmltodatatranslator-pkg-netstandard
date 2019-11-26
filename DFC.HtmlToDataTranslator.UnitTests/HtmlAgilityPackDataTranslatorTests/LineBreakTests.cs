using DFC.HtmlToDataTranslator.Services;
using System.Linq;
using Xunit;

namespace DFC.HtmlToDataTranslator.UnitTests.HtmlAgilityPackDataTranslatorTests
{
    public class LineBreakTests
    {
        [Theory]
        [InlineData("content1<br/>content2")]
        [InlineData("content1<br/><p>content2</p>")]
        [InlineData("content1<br/><br/>content2")]
        [InlineData("<p>content1<br/><br/>content2</p>")]
        public void CanTranslateLineBreaks(string sourceValue)
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var outputValue = translator.Translate(sourceValue);
            Assert.Equal(2, outputValue.Count);
            Assert.Equal("content1", outputValue.First());
            Assert.Equal("content2", outputValue.Last());
        }

        [Theory]
        [InlineData("content1<br/><br/><br/><p>p1</p>")]
        [InlineData("<br/><br/><br/>content1<br/><br/><br/><p>p1</p>")]
        public void ExtraLineBreaksAreIgnored(string sourceValue)
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var outputValue = translator.Translate(sourceValue);
            Assert.Equal(2, outputValue.Count);
            Assert.Equal("content1", outputValue.First());
            Assert.Equal("p1", outputValue.Last());
        }
    }
}
