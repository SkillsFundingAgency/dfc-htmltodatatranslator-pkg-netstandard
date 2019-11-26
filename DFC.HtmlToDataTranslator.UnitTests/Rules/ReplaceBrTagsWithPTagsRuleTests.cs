using DFC.HtmlToDataTranslator.Rules;
using Xunit;

namespace DFC.HtmlToDataTranslator.UnitTests.Rules
{
    public class ReplaceBrTagsWithPTagsRuleTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("abc", "abc")]
        [InlineData("<div>c</div>", "<div>c</div>")]
        [InlineData("<div id='div1'>content</div>", "<div id='div1'>content</div>")]
        [InlineData("<p>some content<br></p>", "<p>some content</p><p></p>")]
        [InlineData("<p>some content<br/></p>", "<p>some content</p><p></p>")]
        [InlineData("<p>some content<br /></p>", "<p>some content</p><p></p>")]
        [InlineData("<p id='p1'>some content<br></p>", "<p id='p1'>some content</p><p></p>")]
        [InlineData("<p id='p1'>some content<br><br/></p>", "<p id='p1'>some content</p><p></p><p></p>")]
        public void CanAppluRule(string source, string expected)
        {
            var rule = new ReplaceBrTagsWithPTagsRule();
            var actual = rule.Process(source);
            Assert.Equal(expected, actual);
        }
    }
}
