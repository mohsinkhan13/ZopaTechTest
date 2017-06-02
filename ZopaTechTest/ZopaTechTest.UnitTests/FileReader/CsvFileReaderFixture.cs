using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZopaTechTest.FileReader;
using ZopaTechTest.Models;

namespace ZopaTechTest.UnitTests.FileReader
{
    [TestFixture]
    public class CsvFileReaderFixture
    {
        
        [Test]
        public void CsvFileReader_Accepts_Only_CsvFile()
        {
            var filePath = "NonCsvFile.txt";
            var exception = Assert.Throws<Exception>(
                delegate
                {
                    var fileReader = new CsvFileReader(filePath);
                });

            Assert.IsTrue(exception.Message.Contains("Invalid File. Only CSV files allowed"));
        }

        [Test]
        public void CsvFileReader_Read_Throws_FileNotFound_Exception()
        {
            var filePath = "NonExistingFile.csv";
            var exception = Assert.Throws<FileNotFoundException>(
                delegate
                {
                    var fileReader = new CsvFileReader(filePath);
                    var lenders = fileReader.Read();
                });

            Assert.IsTrue(exception.Message.Contains("File does not exist"));
        }

        [Test]
        public void CsvFileReader_Read_Returns_ListOf_Lenders_FromFile()
        {

            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                     @"..\..\TestFiles\TestFile.csv");

            var fileReader = new CsvFileReader(filePath);
            var lenders = fileReader.Read();
            
            Assert.Multiple(() => {
                Assert.IsInstanceOf<List<LenderDetail>>(lenders);
                Assert.AreEqual(7,lenders.ToList().Count());
            }
            );
        }
    }
}
