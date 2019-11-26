using DFC.HtmlToDataTranslator.Rules;
using Xunit;

namespace DFC.HtmlToDataTranslator.UnitTests.Rules
{
    public class ConvertLinksToTextRuleTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("<a href='http://www.google.com'>link1</a>", "[link1 | http://www.google.com]")]
        [InlineData("<a href=''>link1</a>", "[link1 | ]")]
        [InlineData("<a>link1</a>", "[link1 | ]")]
        [InlineData(" <a>link1</a> ", " [link1 | ] ")]
        [InlineData(" <p id=p1>p1</p> ", " <p id=p1>p1</p> ")]
        [InlineData("<p id=p1>p1</p>", "<p id=p1>p1</p>")]
        [InlineData("<p id=p1><a href='http://www.google.com'>link1</a></p>", "<p id=p1>[link1 | http://www.google.com]</p>")]
        [InlineData("<p><a href='page1'>link1</a> <div>div1</div> <a href=page2>link2</a></p>", "<p>[link1 | page1] <div>div1</div> [link2 | page2]</p>")]
        [InlineData(" <a href='page1'>link1</a> <a>link2</a> <a href>link3</a> ", " [link1 | page1] [link2 | ] [link3 | ] ")]
        public void CanApplyRule(string source, string expected)
        {
            var rule = new ConvertLinksToTextRule();
            var actual = rule.Process(source);
            Assert.Equal(expected, actual);
        }
    }
}
