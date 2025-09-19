using ACG_Api.model.XPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ACG_NUnitTest.Test.XPathTest
{
    internal class XplLinqTest
    {

        //public void TestXmlTreeLinq()
        //{
        //    XPathProcessor xPath = new(_pathToTemplate);

        //    string dataXml = xPath.ExtractXml(_pathToTemplate);

        //    XDocument doc = XDocument.Parse(dataXml);

        //    XNamespace w = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

        //    var sdtElements = doc.Descendants(w + "sdt");

        //    var results = sdtElements
        //        .Where(sdt => sdt.Elements(w + "sdtPr").Any())
        //        .Select(sdt =>
        //        {
        //            var sdtPr = sdt.Element(w + "sdtPr");
        //            var alias = sdtPr?.Element(w + "alias")?.Attribute(w + "val")?.Value;
        //            var tag = sdtPr?.Element(w + "tag")?.Attribute(w + "val")?.Value;

        //            var contentText = sdt
        //                .Element(w + "sdtContent")?
        //                .Descendants(w + "t")
        //                .Select(t => t.Value)
        //                .Aggregate(string.Empty, (a, b) => a + b);

        //            return new
        //            {
        //                Alias = alias,
        //                Tag = tag,
        //                Text = contentText
        //            };
        //        });

        //    foreach (var item in results)
        //    {
        //        Console.WriteLine($"Alias: {item.Alias}");
        //        Console.WriteLine($"Tag: {item.Tag}");
        //        Console.WriteLine($"Text: {item.Text}");
        //        Console.WriteLine("-----");
        //    }
        //}

        //public List<TestXmlTree> GetXmlTreeForTest()
        //{
        //    var testList = new List<TestXmlTree>();

        //    XPathProcessor xPath = new XPathProcessor(_pathToTemplate);
        //    string dataXml = xPath.ExtractXml(_pathToTemplate);

        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(new StringReader(dataXml));

        //    XmlNamespaceManager nsManager = new XmlNamespaceManager(doc.NameTable);
        //    nsManager.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");

        //    var paragraphs = doc.SelectNodes("//w:sdt[w:*]", nsManager);

        //    if (paragraphs != null)
        //    {
        //        foreach (XmlNode node in paragraphs)
        //        {
        //            TestXmlTree test = new TestXmlTree();

        //            XmlNode? sdtPr = node.SelectSingleNode("w:sdtPr", nsManager);
        //            if (sdtPr != null)
        //            {
        //                var alias = sdtPr.SelectSingleNode("w:alias", nsManager);
        //                test.Alias = alias?.Attributes?["w:val"]?.Value;

        //                var tag = sdtPr.SelectSingleNode("w:tag", nsManager);
        //                test.Tag = tag?.Attributes?["w:val"]?.Value;
        //            }

        //            XmlNode? sdtContent = node.SelectSingleNode("w:sdtContent", nsManager);
        //            if (sdtContent != null)
        //            {
        //                var textNode = sdtContent.SelectSingleNode(".//w:t", nsManager);
        //                test.Text = textNode?.InnerText;
        //            }

        //            testList.Add(test);
        //        }
        //    }

        //    return testList;
        //}
    }
}
