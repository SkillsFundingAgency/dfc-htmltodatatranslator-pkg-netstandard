using DFC.HtmlToDataTranslator.Rules;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DFC.HtmlToDataTranslator.UnitTests.Rules
{
    public class DecodeHtmlRuleTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("<p>content1&nbsp; content2</p>", "<p>content1  content2</p>")]
        [InlineData("<p>content1&pound; content2</p>", "<p>content1£ content2</p>")]
        [InlineData("<p>content1&ndash; content2</p>", "<p>content1– content2</p>")]
        [InlineData("<p>content1&amp; content2</p>", "<p>content1& content2</p>")]
        [InlineData("<p>content1&lsquo; content2</p>", "<p>content1‘ content2</p>")]
        [InlineData("<p>content1&rsquo; content2</p>", "<p>content1’ content2</p>")]
        [InlineData("<p>content1&copy; content2</p>", "<p>content1© content2</p>")]
        [InlineData("<p>content1&lt; content2</p>", "<p>content1< content2</p>")]
        [InlineData("<p>content1&gt; content2</p>", "<p>content1> content2</p>")]
        [InlineData("<p>content1&le; content2</p>", "<p>content1≤ content2</p>")]
        [InlineData("<p>content1&ge; content2</p>", "<p>content1≥ content2</p>")]
        public void ReplaceBrTagsWithPTags(string source, string expected)
        {
            var rule = new DecodeHtmlRule();
            var actual = rule.Process(source);
            Assert.Equal(expected, actual);
        }
    }
}
