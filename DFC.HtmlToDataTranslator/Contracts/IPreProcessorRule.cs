using System;
using System.Collections.Generic;
using System.Text;

namespace DFC.HtmlToDataTranslator.Contracts
{
    public interface IPreProcessorRule
    {
        string Process(string html);
    }
}
