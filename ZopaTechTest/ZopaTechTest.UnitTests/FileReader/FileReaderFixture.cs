using NUnit.Framework;
using ZopaTechTest.FileReader;

namespace ZopaTechTest.UnitTests.FileReader
{
    [TestFixture]
    public class FileReaderFixture
    {
        [Test]
        public void FileReader_Can_Accept_String_FilePath()
        {
            var filePath = "any string";
            var fileReader = new CsvFileReader(filePath);

            Assert.IsInstanceOf<CsvFileReader>(fileReader);
        }
        
    }
}
