using DFC.HtmlToDataTranslator.Services;
using System.Linq;
using Xunit;

namespace DFC.HtmlToDataTranslator.UnitTests.HtmlAgilityPackDataTranslatorTests
{
    public class MixedTests
    {
        [Fact]
        public void CanTranslateTextFollowedByPTag()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = @"this is some content<p>p1</p>";
            var outputValue = translator.Translate(sourceValue);
            Assert.Equal(2, outputValue.Count);
            Assert.Equal("this is some content", outputValue.First());
            Assert.Equal("p1", outputValue.Last());
        }

        [Fact]
        public void CanTranslatePTagFollowedByText()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = @"<p>p1</p>this is some content";
            var outputValue = translator.Translate(sourceValue);
            Assert.Equal(2, outputValue.Count);
            Assert.Equal("p1", outputValue.First());
            Assert.Equal("this is some content", outputValue.Last());
        }

        [Fact]
        public void CanTranslateTextInMiddleWithPTag()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = @"<p>p1</p>this is some content<p>p2</p>";
            var outputValue = translator.Translate(sourceValue);
            Assert.Equal(3, outputValue.Count);
            Assert.Equal("p1", outputValue.ElementAt(0));
            Assert.Equal("this is some content", outputValue.ElementAt(1));
            Assert.Equal("p2", outputValue.ElementAt(2));
        }

        [Fact]
        public void CanTranslateTextInMiddleWithPTagAndLink()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = @"<p>p1</p>this is some <a href='http://www.yahoo.com'>link1</a>content<p>p2</p>";
            var outputValue = translator.Translate(sourceValue);
            Assert.Equal(3, outputValue.Count);
            Assert.Equal("p1", outputValue.ElementAt(0));
            Assert.Equal("this is some[link1 | http://www.yahoo.com]content", outputValue.ElementAt(1));
            Assert.Equal("p2", outputValue.ElementAt(2));
        }

        [Fact]
        public void CanTranslateTextWithLinks()
        {
            var translator = new HtmlAgilityPackDataTranslator();
            var sourceValue = @"content non in html tags <a href='http://www.google.com'>link1</a> more content";
            var outputValue = translator.Translate(sourceValue);
            Assert.Single(outputValue);
            Assert.Equal("content non in html tags[link1 | http://www.google.com]more content", outputValue.ElementAt(0));
        }
    }
}
