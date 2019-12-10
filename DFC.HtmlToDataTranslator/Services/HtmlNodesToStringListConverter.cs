using DFC.HtmlToDataTranslator.Constants;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace DFC.HtmlToDataTranslator.Services
{
    public class HtmlNodesToStringListConverter
    {
        private const string SeperatorSemicolonWithSpace = "; ";
        private const string Space = " ";

        public List<string> Convert(Queue<HtmlNode> htmlNodes)
        {
            var result = new List<string>();

            HtmlNode nextHtmlNode = null;
            HtmlNode previousHtmlNode = null;

            while (htmlNodes.Count > 0)
            {
                var currentHtmlNode = htmlNodes.Dequeue();
                if (htmlNodes.Any())
                {
                    nextHtmlNode = htmlNodes.Peek();
                }

                if (!ExcludeNode(currentHtmlNode))
                {
                    var text = Translate(currentHtmlNode);

                    var currentNodeIsLinkAndPreviousNodeIsText = previousHtmlNode != null && IsLink(currentHtmlNode) && IsNodeTypeText(previousHtmlNode);
                    var currentNodeIsTextAndPreviousNodeIsLink = previousHtmlNode != null && IsNodeTypeText(currentHtmlNode) && IsLink(previousHtmlNode);
                    var currentNodeIsListPreviousNodeIsParagraph = previousHtmlNode != null && IsList(currentHtmlNode) && IsParagraph(previousHtmlNode);

                    if (currentNodeIsLinkAndPreviousNodeIsText
                        || currentNodeIsTextAndPreviousNodeIsLink
                        || currentNodeIsListPreviousNodeIsParagraph)
                    {
                        result[result.Count - 1] += text;
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(text))
                        {
                            result.Add(text);
                        }
                    }

                    previousHtmlNode = currentHtmlNode;
                }
            }

            return result;
        }

        private bool ExcludeNode(HtmlNode htmlNode)
        {
            return htmlNode.NodeType == HtmlNodeType.Text
                && (string.IsNullOrEmpty(htmlNode.InnerText) || string.IsNullOrEmpty(htmlNode.InnerText.Trim()));
        }

        private string Translate(HtmlNode htmlNode)
        {
            var result = string.Empty;

            if (IsList(htmlNode))
            {
                result = TranslateList(htmlNode, SeperatorSemicolonWithSpace);
            }
            else if (htmlNode.ChildNodes.Any())
            {
                var childNodeTranslated = TranslateChildren(htmlNode);
                if (!string.IsNullOrEmpty(childNodeTranslated))
                {
                    result += childNodeTranslated;
                }
            }
            else
            {
                result = ParseText(htmlNode);
            }

            return result;
        }

        private string TranslateChildren(HtmlNode htmlNode)
        {
            var result = string.Empty;
            foreach (var childNode in htmlNode.ChildNodes)
            {
                var childNodeTranslated = Translate(childNode);
                if (!string.IsNullOrEmpty(childNodeTranslated))
                {
                    result += childNodeTranslated;
                }
            }

            return result;
        }

        private string TranslateList(HtmlNode htmlNode, string seperator)
        {
            var listItems = htmlNode.Descendants(TagName.Li);
            var listItemsTranslated = listItems.Select(x => Translate(x));
            var result = string.Join(seperator, listItemsTranslated);
            if (!string.IsNullOrEmpty(result))
            {
                result = Space + result;
            }

            return result;
        }

        private bool IsLink(HtmlNode htmlNode)
        {
            return htmlNode.Name == TagName.A;
        }

        private bool IsList(HtmlNode htmlNode)
        {
            return htmlNode.Name == TagName.UL || htmlNode.Name == TagName.OL;
        }

        private bool IsParagraph(HtmlNode htmlNode)
        {
            return htmlNode.Name == TagName.P;
        }

        private bool IsNodeTypeText(HtmlNode htmlNode)
        {
            return htmlNode.NodeType == HtmlNodeType.Text;
        }

        private string ParseText(HtmlNode htmlNode)
        {
            var result = string.Empty;
            if (htmlNode != null)
            {
                result = htmlNode.InnerText;
                if (!string.IsNullOrWhiteSpace(result))
                {
                    result = result.Trim();
                }
            }

            return result;
        }
    }
}
