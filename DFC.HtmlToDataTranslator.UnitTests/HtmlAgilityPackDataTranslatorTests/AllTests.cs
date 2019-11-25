using DFC.HtmlToDataTranslator.Services;
using System.Linq;
using Xunit;

namespace DFC.HtmlToDataTranslator.UnitTests.HtmlAgilityPackDataTranslatorTests
{
    public class AllTests
    {
        [Fact]
        public void CanTranslateParagraphWithAdjacentLists()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = @"
<p>You'll need to have some practical experience and be able to show you have a real enthusiasm for sports commentating. </p>
<p>To get some work experience you could:</p>
<ul>
<li>volunteer to commentate on charity events like fun runs</li>
<li>commentate for amateur matches at schools, college or for local teams</li>
<li>record commentary for websites or internet radio stations</li>
<li>volunteer for community, hospital or student radio, or TV</li>
</ul>
<p>You can get a list of radio stations from:</p>
<ul>
<li><a href=""http://www.commedia.org.uk/"">Community Media Association</a></li>
<li><a href=""http://www.hbauk.com/"">Hospital Broadcasting Association</a></li>
<li><a href=""http://www.radiocentre.org/"">RadioCentre</a></li>
</ul>
<p>Large broadcasters like&nbsp;<a href=""https://www.bbc.co.uk/careers/work-experience/"">BBC Careers</a>, <a href=""http://www.itvjobs.com/workinghere/entry-careers/"">ITV</a> &nbsp; and <a href=""https://careers.channel4.com/4talent"">Channel 4</a> offer work experience placements, insight and talent days.</p>
<p>
The <a href = ""https://www.sportsjournalists.co.uk/training/work-experience/"" > Sports Journalists &rsquo; Association </a> has more ideas about where to look 
for work experience.</p>
<p>With experience<br/><br/>You could move into other roles. Find more <a href='http://ncs.com'>information</a> here</p>";
            var outputValue = translator.Translate(sourceValue);
            Assert.Equal(7, outputValue.Count);
            Assert.Equal("You'll need to have some practical experience and be able to show you have a real enthusiasm for sports commentating.", outputValue.ElementAt(0));
            Assert.Equal("To get some work experience you could:volunteer to commentate on charity events like fun runs; commentate for amateur matches at schools, college or for local teams; record commentary for websites or internet radio stations; volunteer for community, hospital or student radio, or TV", outputValue.ElementAt(1));
            Assert.Equal("You can get a list of radio stations from:[Community Media Association | http://www.commedia.org.uk/]; [Hospital Broadcasting Association | http://www.hbauk.com/]; [RadioCentre | http://www.radiocentre.org/]", outputValue.ElementAt(2));
            Assert.Equal("Large broadcasters like[BBC Careers | https://www.bbc.co.uk/careers/work-experience/],[ITV | http://www.itvjobs.com/workinghere/entry-careers/]and[Channel 4 | https://careers.channel4.com/4talent]offer work experience placements, insight and talent days.", outputValue.ElementAt(3));
            Assert.Equal("The[Sports Journalists ’ Association | https://www.sportsjournalists.co.uk/training/work-experience/]has more ideas about where to look \r\nfor work experience.", outputValue.ElementAt(4));
            Assert.Equal("With experience", outputValue.ElementAt(5));
            Assert.Equal("You could move into other roles. Find more[information | http://ncs.com]here", outputValue.ElementAt(6));
        }
    }
}
