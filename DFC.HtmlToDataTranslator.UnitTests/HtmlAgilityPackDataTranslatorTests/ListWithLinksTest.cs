using DFC.HtmlToDataTranslator.Services;
using System.Linq;
using Xunit;

namespace DFC.HtmlToDataTranslator.UnitTests.HtmlAgilityPackDataTranslatorTests
{
    public class ListWithLinksTest
    {
        [Fact]
        public void CanTranslate()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = @"
<ul>
<li><a href=""http://www.commedia.org.uk/"">Community Media Association </a></li>
<li><a href=""http://www.hbauk.com/"">Hospital Broadcasting Association</a></li>
<li><a href=""http://www.radiocentre.org/"">RadioCentre</a></li>
</ul>";

            var outputValue = translator.Translate(sourceValue);
            Assert.Single(outputValue);
            Assert.Equal("[Community Media Association | http://www.commedia.org.uk/]; [Hospital Broadcasting Association | http://www.hbauk.com/]; [RadioCentre | http://www.radiocentre.org/]", outputValue.First());
        }
    }
}
