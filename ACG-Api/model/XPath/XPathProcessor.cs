using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace ACG_Api.model.XPath
{
    public class PathSettings
    {
        public string PathToFolder { get; set; } = "";
    }
    public class XPathProcessor : IFile, IXMLParser, IDocxService, IDocxFileNameService
    {
        private readonly string _filePath;
        private XmlDocument doc;
        private XmlNamespaceManager nsManager;
        private readonly string _pathToTemplate;

        public XPathProcessor(){}

        public XPathProcessor(string pathToTemplate)
        {
            _pathToTemplate = pathToTemplate;
            string dataXml = ExtractXml(_pathToTemplate);

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _filePath = config["PathToSaveAgreements:PathToFolder"] ?? " ";

            doc = new XmlDocument();
            doc.LoadXml(dataXml);

            nsManager = new XmlNamespaceManager(doc.NameTable);
            nsManager.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
        }

        public string ExtractXml(string pathToDocx)
        {
            try
            {
                using (var archive = ZipFile.OpenRead(pathToDocx))
                {
                    var entry = archive.GetEntry("word/document.xml");
                    if (entry == null)
                        throw new Exception($"word file is empty or broken: {pathToDocx}");

                    using (var reader = new StreamReader(entry.Open()))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (InvalidDataException)
            {
                throw new Exception($"word file is empty or broken: {pathToDocx}");
            }
        }


        public void GetXmlTree()
        {
            XPathProcessor xPath = new (_pathToTemplate);

            string dataXml = xPath.ExtractXml(_pathToTemplate);

            doc.Load(new StringReader(dataXml));
        
            nsManager = new XmlNamespaceManager(doc.NameTable);
            nsManager.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");


            var paragraphs = doc.SelectNodes("//w:sdt[w:*]", nsManager);

            if (paragraphs is not null)
            {
                foreach (XmlNode node in paragraphs)
                {
                    XmlNode? sdtPr = node.SelectSingleNode("w:sdtPr", nsManager);

                    if (sdtPr != null)
                    {
                        var alias = sdtPr.SelectSingleNode("w:alias", nsManager);
                        if (alias?.Attributes?["w:val"] != null)
                        {
                            Console.WriteLine("Alias: " + alias.Attributes?["w:val"]?.Value);
                        }

                        var tag = sdtPr.SelectSingleNode("w:tag", nsManager);
                        if (tag?.Attributes?["w:val"] != null)
                        {
                            Console.WriteLine("Tag: " + tag?.Attributes?["w:val"]?.Value);
                        }
                    }

                    XmlNode? sdtContent = node.SelectSingleNode("w:sdtContent", nsManager);
                    if (sdtContent != null)
                    {
                        var textNode = sdtContent.SelectSingleNode(".//w:t", nsManager);
                        if (textNode != null)
                        {
                            Console.WriteLine("Text: " + textNode.InnerText);
                        }
                    }
                }

            }
        }

        public void WriteXmlTree(string Tag, string Value)
        {
            var paragraphs = doc.SelectNodes("//w:sdt[w:*]", nsManager);

            if (paragraphs != null)
            {
                foreach (XmlNode node in paragraphs)
                {
                    XmlNode? sdtPr = node.SelectSingleNode("w:sdtPr", nsManager);

                    if (sdtPr != null)
                    {
                        var tag = sdtPr.SelectSingleNode("w:tag", nsManager);
                        if (tag?.Attributes?["w:val"]?.Value == Tag)
                        {
                            XmlNode? sdtContent = node.SelectSingleNode("w:sdtContent", nsManager);
                            if (sdtContent != null)
                            {
                                var textNode = sdtContent.SelectSingleNode(".//w:t", nsManager);
                                if (textNode != null)
                                {
                                    textNode.InnerText = Value;
                                }
                            }
                        }
                    }
                }
            }
        }

        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => new UTF8Encoding(false);
        }

        public bool isCreatedErlier(string path)
        {
            if (File.Exists(path))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int GetDocxFileNumber(string path)
        {
            string name = Path.GetFileNameWithoutExtension(path);
            var match = Regex.Match(name, @"_(\d+)$");
            if (match.Success)
            {
                return int.Parse(match.Groups[1].Value);
            }
            return 0;
        }

        public string SetNumInName(int num, string path)
        {
            string directory = Path.GetDirectoryName(path);
            string filename = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path);

            string baseName = Regex.Replace(filename, @"_(\d+)$", "");
            string newFileName = $"{baseName}_{num}{extension}";

            return $"{directory}\\{newFileName}";
        }

        public string GenerateDocxFileName(string path)
        {
            bool is_createdErlier = isCreatedErlier(path);
            if (is_createdErlier == true)
                return path;

            int number = GetDocxFileNumber(path);

            if (number == 0)
                number = 1;
            else
                number++;

            string newPath = SetNumInName(number, path);
            while (File.Exists(newPath))
            {
                number++;
                newPath = SetNumInName(number, path);
            }

            return newPath;
        }

        public void Save(string nameFile)
        {
            string updatedXml;
            using (var stringWriter = new Utf8StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
            {
                Indent = false,
                Encoding = new UTF8Encoding(false)
            }))
            {
                doc.Save(xmlWriter);
                updatedXml = stringWriter.ToString();
            }

            SaveXml(_pathToTemplate, updatedXml, _filePath + nameFile + ".docx");
        }

        public void SaveXml(string originalDocxPath, string modifiedXml, string outputDocxPath)
        {
            string File = GenerateDocxFileName(outputDocxPath);
            using (FileStream fs = new FileStream(File, FileMode.Create))
            using (ZipArchive originalArchive = ZipFile.Open(originalDocxPath, ZipArchiveMode.Read))
            using (ZipArchive newArchive = new ZipArchive(fs, ZipArchiveMode.Create))
            {
                foreach (var entry in originalArchive.Entries)
                {
                    if (entry.FullName == "word/document.xml")
                    {
                        var newEntry = newArchive.CreateEntry("word/document.xml");
                        using (var entryStream = newEntry.Open())
                        using (var writer = new StreamWriter(entryStream))
                        {
                            writer.Write(modifiedXml);
                        }
                    }
                    else
                    {
                        var newEntry = newArchive.CreateEntry(entry.FullName);
                        using (var entryStream = newEntry.Open())
                        using (var originalStream = entry.Open())
                        {
                            originalStream.CopyTo(entryStream);
                        }
                    }
                }
            }
        }
    }
}