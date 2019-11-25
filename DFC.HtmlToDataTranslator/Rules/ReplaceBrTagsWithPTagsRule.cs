using DFC.HtmlToDataTranslator.Constants;
using DFC.HtmlToDataTranslator.Contracts;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace DFC.HtmlToDataTranslator.Rules
{
    public class ReplaceBrTagsWithPTagsRule : IPreProcessorRule
    {
        public string Process(string html)
        {
            var result = html;

            if (!string.IsNullOrWhiteSpace(html))
            {
                result = string.Empty;

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                var newValue = "</p><p>";

                foreach (var element in htmlDoc.DocumentNode.ChildNodes)
                {
                    if (element.Name == TagName.P)
                    {
                        var elementContents = element.OuterHtml;
                        var sourceValues = new List<string>() { "<br>", "<br/>", "<br />" };
                        foreach (var sourceValue in sourceValues)
                        {
                            elementContents = elementContents.Replace(sourceValue, newValue, StringComparison.OrdinalIgnoreCase);
                        }

                        result += elementContents;
                    }
                    else
                    {
                        result += element.OuterHtml;
                    }
                }
            }

            return result;
        }
    }
}
