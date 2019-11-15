using DFC.HtmlToDataTranslator.Services;
using System.Linq;
using Xunit;

namespace DFC.HtmlToDataTranslator.UnitTests.HtmlAgilityPackDataTranslatorTests
{
    public class ParagraphWithLinksTest
    {
        [Fact]
        public void CanTranslateParagraphWithLink()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = @"<p>Large broadcasters like&nbsp;<a href=""https://www.bbc.co.uk/careers/work-experience/"">BBC Careers</a>, <a href=""http://www.itvjobs.com/workinghere/entry-careers/"">ITV</a>&nbsp;and <a href=""https://careers.channel4.com/4talent"">Channel 4</a> offer work experience placements, insight and talent days.</p>";
            var outputValue = translator.Translate(sourceValue);
            Assert.Single(outputValue);
            Assert.Equal("Large broadcasters like&nbsp;[BBC Careers | https://www.bbc.co.uk/careers/work-experience/], [ITV | http://www.itvjobs.com/workinghere/entry-careers/]&nbsp;and [Channel 4 | https://careers.channel4.com/4talent] offer work experience placements, insight and talent days.", outputValue.First());
        }
    }
}
