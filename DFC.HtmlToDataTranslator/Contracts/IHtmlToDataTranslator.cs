using System.Collections.Generic;

namespace DFC.HtmlToDataTranslator.Contracts
{
    public interface IHtmlToDataTranslator
    {
        List<string> Translate(string value);
    }
}
