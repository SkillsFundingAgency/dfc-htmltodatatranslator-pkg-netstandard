using DFC.HtmlToDataTranslator.Services;
using System.Linq;
using Xunit;

namespace DFC.HtmlToDataTranslator.UnitTests.HtmlAgilityPackDataTranslatorTests
{
    public class EntitiesTests
    {
        [Theory]
        [InlineData("content&nbsp;", "content")]
        [InlineData("content&pound;", "content£")]
        [InlineData("content&ndash;", "content–")]
        [InlineData("content&amp;", "content&")]
        [InlineData("content&lsquo;", @"content‘")]
        [InlineData("content&rsquo;", @"content’")]
        [InlineData("content&copy;", @"content©")]
        [InlineData("content&lt;", @"content<")]
        [InlineData("content&gt;", @"content>")]
        [InlineData("content&le;", @"content≤")]
        [InlineData("content&ge;", @"content≥")]
        public void ContentWithEntitiesIsTranslatedCorrectly(string sourceValue, string expectedValue)
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var outputValue = translator.Translate(sourceValue);
            Assert.Single(outputValue);
            Assert.Equal(expectedValue, outputValue.First());
        }
    }
}
