using DFC.HtmlToDataTranslator.Services;
using System.Linq;
using Xunit;

namespace DFC.HtmlToDataTranslator.UnitTests.HtmlAgilityPackDataTranslatorTests
{
    public class ParagraphsWithAdjacentListsTest
    {
        [Fact]
        public void CanTranslateParagraphWithAdjacentLists()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = @"<p>Your duties may include:</p>
<ul>
<li>preparing for an event by researching clubs or players</li>
<li>working with a production team</li>
<li>taking direction from a producer</li>
<li>interviewing sports professionals</li>
<li>commentating on events before, during and after the fixture</li>
<li>working with experts who give their opinion or statistics</li>
<li>updating your website, blog or social media feed</li>
</ul>";
            var outputValue = translator.Translate(sourceValue);
            Assert.Single(outputValue);
            Assert.Equal("Your duties may include:preparing for an event by researching clubs or players; working with a production team; taking direction from a producer; interviewing sports professionals; commentating on events before, during and after the fixture; working with experts who give their opinion or statistics; updating your website, blog or social media feed", outputValue.First());
        }
    }
}
