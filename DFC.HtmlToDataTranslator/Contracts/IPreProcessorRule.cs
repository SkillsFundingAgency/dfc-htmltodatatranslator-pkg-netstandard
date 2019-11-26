namespace DFC.HtmlToDataTranslator.Contracts
{
    public interface IPreProcessorRule
    {
        string Process(string html);
    }
}
