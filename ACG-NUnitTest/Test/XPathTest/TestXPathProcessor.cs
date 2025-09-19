using ACG_Api.model.XPath;
using FluentAssertions;
using Xunit;

namespace ACG_NUnitTest.Test.XPathTest
{
    public class TestXPathProcessor
    {
        public string SampleDocx = "D:\\Projects\\CSharp\\Acg-Api\\ACG-NUnitTest\\Test\\XPathTest\\Docx\\sample.docx";
        public string SampleDocx2 = "D:\\Projects\\CSharp\\Acg-Api\\ACG-NUnitTest\\Test\\XPathTest\\Docx\\sample2_2.docx";

        [Fact]
        public void ExtractXml_ShouldReturnXmlContent()
        {
            var processor = new XPathProcessor(SampleDocx2);

            string xml = processor.ExtractXml(SampleDocx2);

            xml.Should().Contain("<w:document");
        }

        [Fact]
        public void ExtractXml_ShouldThrowException_ForBrokenOrEmptyDocx()
        {
            Action act = () => new XPathProcessor(SampleDocx);

            act.Should().Throw<Exception>()
               .WithMessage($"word file is empty or broken: {SampleDocx}");
        }

        [Fact]
        public void GetDocxFileNumber_ShouldBe0()
        {
            var processor = new XPathProcessor(SampleDocx2);

            string newPath = processor.GetDocxFileNumber(SampleDocx2).ToString();

            newPath.Should().NotBe(1.ToString());
            newPath.Should().Contain("2");
        }

        [Fact]
        public void SetNumInName_ShouldBe0()
        {
            var processor = new XPathProcessor(SampleDocx2);

            string newPath = processor.SetNumInName(3, SampleDocx2).ToString();

            newPath.Should().NotBe("D:\\Projects\\CSharp\\Acg-Api\\ACG-NUnitTest\\Test\\XPathTest\\Docx\\sample2_2.docx");
            newPath.Should().Contain("D:\\Projects\\CSharp\\Acg-Api\\ACG-NUnitTest\\Test\\XPathTest\\Docx\\sample2_3.docx");
        }

        [Fact]
        public void isCreatedErlier_NeedTrue()
        {
            var processor = new XPathProcessor(SampleDocx2);

            string newPath = processor.isCreatedErlier(SampleDocx2).ToString();

            newPath.Should().NotBe(true.ToString());
            newPath.Should().Contain(false.ToString());
        }

        [Fact]
        public void GenerateDocxFileName_ShouldIncrement_WhenFileExists()
        {
            var processor = new XPathProcessor();

            string newPath = processor.GenerateDocxFileName(SampleDocx2);

            newPath.Should().Contain("D:\\Projects\\CSharp\\Acg-Api\\ACG-NUnitTest\\Test\\XPathTest\\Docx\\sample2_3.docx");
        }


        [Fact]
        public void WriteXmlTree_ShouldReplaceText_ForMatchingTag()
        {
            // Arrange
            var processor = new XPathProcessor(SampleDocx);

            // Act
            processor.WriteXmlTree("SomeTag", "NewValue");

            // Save во временный файл
            string tempPath = Path.Combine(Path.GetTempPath(), "updated.docx");
            processor.Save("updated");

            // Assert
            string xml = processor.ExtractXml(tempPath);
            xml.Should().Contain("NewValue");
        }
    }
}