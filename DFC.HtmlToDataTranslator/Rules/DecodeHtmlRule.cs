using DFC.HtmlToDataTranslator.Contracts;
using HtmlAgilityPack;

namespace DFC.HtmlToDataTranslator.Rules
{
    public class DecodeHtmlRule : IPreProcessorRule
    {
        public string Process(string html)
        {
            return HtmlEntity.DeEntitize(html);
        }
    }
}
