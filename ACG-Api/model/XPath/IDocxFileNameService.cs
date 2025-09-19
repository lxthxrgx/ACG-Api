namespace ACG_Api.model.XPath
{
    public interface IDocxFileNameService
    {
        bool isCreatedErlier(string path);
        int GetDocxFileNumber(string path);
        string GenerateDocxFileName(string path);
    }
}
