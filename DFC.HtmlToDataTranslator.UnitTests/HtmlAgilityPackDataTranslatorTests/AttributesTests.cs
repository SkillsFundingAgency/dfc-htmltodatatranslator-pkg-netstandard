using DFC.HtmlToDataTranslator.Services;
using System.Linq;
using Xunit;

namespace DFC.HtmlToDataTranslator.UnitTests.HtmlAgilityPackDataTranslatorTests
{
    public class AttributesTests
    {
        [Theory]
        [InlineData("<p id='para1'>p1</p>", "p1")]
        [InlineData("<p id='para1' class='important'>p1</p>", "p1")]
        public void ContentWithAttributesIsIgnored(string sourceValue, string expectedValue)
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var outputValue = translator.Translate(sourceValue);
            Assert.Single(outputValue);
            Assert.Equal(expectedValue, outputValue.First());
        }
    }
}
