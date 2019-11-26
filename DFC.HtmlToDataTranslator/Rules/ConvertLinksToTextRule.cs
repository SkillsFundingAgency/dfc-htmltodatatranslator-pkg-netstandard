using DFC.HtmlToDataTranslator.Constants;
using DFC.HtmlToDataTranslator.Contracts;
using HtmlAgilityPack;
using System;

namespace DFC.HtmlToDataTranslator.Rules
{
    public class ConvertLinksToTextRule : IPreProcessorRule
    {
        public string Process(string html)
        {
            var tagNameStart = "<a";
            var tagNameEnd = "</a>";

            if (string.IsNullOrWhiteSpace(html))
            {
                return html;
            }

            var result = string.Empty;
            var currentPosition = 0;
            var linkStartPosition = html.IndexOf(tagNameStart, StringComparison.OrdinalIgnoreCase);
            if (linkStartPosition == -1)
            {
                result = html;
            }

            while (linkStartPosition != -1)
            {
                var linkEndPosition = html.IndexOf(tagNameEnd, linkStartPosition, StringComparison.OrdinalIgnoreCase);
                if (linkEndPosition != -1)
                {
                    var linkHtml = html.Substring(linkStartPosition, linkEndPosition - linkStartPosition + tagNameEnd.Length);
                    var linkHtmlDoc = new HtmlDocument();
                    linkHtmlDoc.LoadHtml(linkHtml);
                    var linkHref = GetHref(linkHtmlDoc);
                    var linkText = GetText(linkHtmlDoc);
                    var newLinkText = $"[{linkText} | {linkHref}]";

                    var textBeforeLink = string.Empty;

                    if (currentPosition == 0)
                    {
                        textBeforeLink = html.Substring(currentPosition, linkStartPosition);
                    }
                    else
                    {
                        textBeforeLink = html.Substring(currentPosition + tagNameEnd.Length, linkStartPosition - currentPosition - tagNameEnd.Length);
                    }

                    result += textBeforeLink + newLinkText;
                    currentPosition = linkEndPosition;
                    linkStartPosition = html.IndexOf(tagNameStart, linkStartPosition + 1, StringComparison.OrdinalIgnoreCase);

                    if (linkStartPosition == -1 && linkEndPosition > 0)
                    {
                        var remainder = html.Substring(linkEndPosition + tagNameEnd.Length);
                        result += remainder;
                    }
                }
            }

            return result;
        }

        private string GetHref(HtmlDocument htmlDocument)
        {
            var result = string.Empty;
            var node = htmlDocument.DocumentNode.SelectSingleNode("a");
            if (node != null)
            {
                var hrefAttribute = node.Attributes[HtmlAttributeName.Href];
                if (hrefAttribute != null)
                {
                    result = hrefAttribute.Value;
                }
            }

            return result;
        }

        private string GetText(HtmlDocument htmlDocument)
        {
            var result = string.Empty;
            var node = htmlDocument.DocumentNode.SelectSingleNode("a");
            if (node != null)
            {
                result = node.InnerText;

                if (!string.IsNullOrWhiteSpace(result))
                {
                    result = result.Trim();
                }
            }

            return result;
        }
    }
}
