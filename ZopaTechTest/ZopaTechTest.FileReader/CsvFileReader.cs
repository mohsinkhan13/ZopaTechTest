using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using ZopaTechTest.Models;

namespace ZopaTechTest.FileReader
{
    public class CsvFileReader
    {
        private const string AllowedExtension = ".csv";
        private string _path;

        public CsvFileReader(string path)
        {
            var extension = Path.GetExtension(path);
            if (!string.Equals(extension,AllowedExtension,StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("Invalid File. Only CSV files allowed");
            }
            _path = path;
        }

        public IEnumerable<LenderDetail> Read()
        {
            var lenders = new List<LenderDetail>();

            try
            {
                using (FileStream sourceStream = new FileStream(_path,
                            FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    StreamReader reader = new StreamReader(sourceStream);
                    string textToDisplay = string.Empty;
                    while (reader.Peek() > -1)
                    {
                        var newline = reader.ReadLine().Split(',');
                        var lenderDetail = new LenderDetail
                        {
                            LenderName = newline[0],
                            Rate = decimal.Parse(newline[1]),
                            AvailableFund = int.Parse(newline[2])
                        };

                        lenders.Add(lenderDetail);

                    }
                };
            }
            catch (FileNotFoundException e)
            {
                throw new FileNotFoundException("File does not exist");
            }
            return lenders;
        }
    }
}
