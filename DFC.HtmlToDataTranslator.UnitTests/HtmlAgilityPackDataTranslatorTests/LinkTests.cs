using DFC.HtmlToDataTranslator.Services;
using System.Linq;
using Xunit;

namespace DFC.HtmlToDataTranslator.UnitTests.HtmlAgilityPackDataTranslatorTests
{
    public class LinkTests
    {
        [Fact]
        public void CanTranslateLinkThatWithHrefAndText()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = @"<a href='https://www.dta-uk.org/join_us.php'>The Dental Technologists Association</a>";
            var outputValue = translator.Translate(sourceValue);
            Assert.Single(outputValue);
            Assert.Equal("[The Dental Technologists Association | https://www.dta-uk.org/join_us.php]", outputValue.First());
        }

        [Fact]
        public void CanTranslateLinksWithNoHrefValue()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = @"<a href=''>The Dental Technologists Association</a>";
            var outputValue = translator.Translate(sourceValue);
            Assert.Single(outputValue);
            Assert.Equal("[The Dental Technologists Association | ]", outputValue.First());
        }

        [Fact]
        public void CanTranslateLinksWithNoHrefAttribute()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = @"<a>The Dental Technologists Association</a>";
            var outputValue = translator.Translate(sourceValue);
            Assert.Single(outputValue);
            Assert.Equal("[The Dental Technologists Association | ]", outputValue.First());
        }
    }
}
