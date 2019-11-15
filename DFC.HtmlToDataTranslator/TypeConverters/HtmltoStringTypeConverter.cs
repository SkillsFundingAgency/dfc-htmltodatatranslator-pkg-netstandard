using AutoMapper;
using DFC.HtmlToDataTranslator.Contracts;
using System.Collections.Generic;

namespace DFC.HtmlToDataTranslator.TypeConverters
{
    public class HtmlToStringTypeConverter : ITypeConverter<string, List<string>>
    {
        private readonly IHtmlToDataTranslator htmlToDataTranslator;

        public HtmlToStringTypeConverter(IHtmlToDataTranslator htmlToDataTranslator)
        {
            this.htmlToDataTranslator = htmlToDataTranslator;
        }

        public List<string> Convert(string source, List<string> destination, ResolutionContext context)
        {
            return htmlToDataTranslator.Translate(source);
        }
    }
}