using DFC.HtmlToDataTranslator.Contracts;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace DFC.HtmlToDataTranslator.Services
{
    public class HtmlAgilityPackDataTranslator : IHtmlToDataTranslator
    {
        public List<string> Translate(string value)
        {
            var result = new List<string>();

            if (!string.IsNullOrWhiteSpace(value))
            {
                var nodeToStringListConvertor = new HtmlNodesToStringListConverter();
                var rulesProcessor = new RulesProcessor();

                var proProcessedContent = rulesProcessor.Process(value);

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(proProcessedContent);

                var nodes = ParseNodes(htmlDoc);
                result = nodeToStringListConvertor.Convert(nodes);
            }

            return result;
        }

        private Queue<HtmlNode> ParseNodes(HtmlDocument htmlDocument)
        {
            return new Queue<HtmlNode>(htmlDocument.DocumentNode.ChildNodes);
        }
    }
}
