namespace ACG_Api.model.XPath
{
    public interface IDocxService
    {
        string ExtractXml(string docxPath);
        void SaveXml(string originalDocxPath, string modifiedXml, string outputDocxPath);
    }
}
