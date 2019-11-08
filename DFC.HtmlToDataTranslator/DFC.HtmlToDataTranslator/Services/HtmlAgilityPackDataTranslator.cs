using DFC.HtmlToDataTranslator.Contracts;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace DFC.HtmlToDataTranslator.Services
{
    public class HtmlAgilityPackDataTranslator : IHtmlToDataTranslator
    {
        private const string Href = "href";

        private const string ElementA = "a";
        private const string ElementP = "p";
        private const string ElementUL = "ul";
        private const string ElementOL = "ol";
        private const string ElementLI = "li";

        private const string SpaceSingle = " ";
        private const string SeperatorSemicolon = ";";
        private const string SeperatorSemicolonWithSpace = "; ";

        public List<string> Translate(string value)
        {
            var result = new List<string>();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(value);

            var elementNodes = htmlDoc.DocumentNode.ChildNodes.Where(x => x.NodeType == HtmlNodeType.Element);
            foreach (var htmlNode in elementNodes)
            {
                var previousNode = GetPreviousNode(htmlNode);
                var nextNode = GetNextNode(htmlNode);
                var isCurrentNodeList = IsList(htmlNode);
                var isPreviousNodeParagraph = false;
                var isNextNodeList = false;

                if (previousNode != null)
                {
                    isPreviousNodeParagraph = IsParagraph(previousNode);
                }

                if (nextNode != null)
                {
                    isNextNodeList = IsList(nextNode);
                }

                if (isCurrentNodeList && isPreviousNodeParagraph)
                {
                    continue;
                }

                var translateResult = Translate(htmlNode);

                if (nextNode != null && IsList(nextNode))
                {
                    var nextNodeTranslateResult = TranslateList(nextNode, SeperatorSemicolonWithSpace);
                    translateResult = translateResult + SpaceSingle + nextNodeTranslateResult;
                }

                if (!string.IsNullOrEmpty(translateResult))
                {
                    result.Add(translateResult);
                }
            }

            return result;
        }

        private string Translate(HtmlNode htmlNode)
        {
            var result = string.Empty;

            if (IsLink(htmlNode))
            {
                result = TranslateLink(htmlNode);
            }
            else if (IsList(htmlNode))
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
                result = TranslateNode(htmlNode);
            }

            return result;
        }

        private string TranslateNode(HtmlNode htmlNode)
        {
            return htmlNode.InnerText;
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

        private string TranslateLink(HtmlNode htmlNode)
        {
            var hrefValue = string.Empty;
            if (htmlNode.Attributes.Contains(Href))
            {
                hrefValue = htmlNode.Attributes[Href].Value;
            }

            return $"[{htmlNode.InnerText} | {hrefValue}]";
        }

        private string TranslateList(HtmlNode htmlNode, string seperator = SeperatorSemicolon)
        {
            var listItems = htmlNode.Descendants(ElementLI);
            var listItemsTranslated = listItems.Select(x => Translate(x));
            var result = string.Join(seperator, listItemsTranslated);
            return result;
        }

        private bool IsLink(HtmlNode htmlNode)
        {
            return htmlNode.Name == ElementA;
        }

        private bool IsList(HtmlNode htmlNode)
        {
            return htmlNode.Name == ElementUL || htmlNode.Name == ElementOL;
        }

        private bool IsParagraph(HtmlNode htmlNode)
        {
            return htmlNode.Name == ElementP;
        }

        private HtmlNode GetPreviousNode(HtmlNode htmlNode)
        {
            var result = htmlNode.PreviousSibling;
            while (result != null && result.NodeType != HtmlNodeType.Element)
            {
                result = result.PreviousSibling;
            }

            return result;
        }

        private HtmlNode GetNextNode(HtmlNode htmlNode)
        {
            var result = htmlNode.NextSibling;
            while (result != null && result.NodeType != HtmlNodeType.Element)
            {
                result = result.NextSibling;
            }

            return result;
        }
    }
}
