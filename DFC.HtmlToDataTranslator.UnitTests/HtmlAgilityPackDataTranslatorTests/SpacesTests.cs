using DFC.HtmlToDataTranslator.Services;
using System.Linq;
using Xunit;

namespace DFC.HtmlToDataTranslator.UnitTests.HtmlAgilityPackDataTranslatorTests
{
    public class SpacesTests
    {
        [Theory]
        [InlineData("content ", "content")]
        [InlineData(" content", "content")]
        [InlineData(" content ", "content")]
        [InlineData("<p>content </p>", "content")]
        [InlineData("<p> content</p>", "content")]
        [InlineData("<p> content </p>", "content")]
        public void ContentWithSpacesIsTrimmed(string sourceValue, string expectedValue)
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var outputValue = translator.Translate(sourceValue);
            Assert.Single(outputValue);
            Assert.Equal(expectedValue, outputValue.First());
        }
    }
}
