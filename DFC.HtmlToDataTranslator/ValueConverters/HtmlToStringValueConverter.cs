using AutoMapper;
using DFC.HtmlToDataTranslator.Contracts;
using System.Collections.Generic;

namespace DFC.HtmlToDataTranslator.ValueConverters
{
    public class HtmlToStringValueConverter : IValueConverter<string, List<string>>
    {
        private readonly IHtmlToDataTranslator htmlToDataTranslator;

        public HtmlToStringValueConverter(IHtmlToDataTranslator htmlToDataTranslator)
        {
            this.htmlToDataTranslator = htmlToDataTranslator;
        }

        public List<string> Convert(string sourceMember, ResolutionContext context)
        {
            var result = htmlToDataTranslator.Translate(sourceMember);
            return result;
        }
    }
}