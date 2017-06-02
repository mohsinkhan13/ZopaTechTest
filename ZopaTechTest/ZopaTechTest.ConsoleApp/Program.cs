using System;
using ZopaTechTest.Calculator;
using ZopaTechTest.FileReader;
using ZopaTechTest.ProductFinder;

namespace ZopaTechTest.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Invalid arguments");
                return;
            }

            var filepath = args[0];
            int loanAmount = 0;
            var parsed = int.TryParse(args[1], out loanAmount);

            
            if (string.IsNullOrEmpty(filepath))
            {
                Console.WriteLine("Invalid file provided");
                return;
            }

            if (!parsed || loanAmount <= 0)
            {
                Console.WriteLine("Please enter valid loan amount");
                return;
            }

            try
            {
                var reader = new CsvFileReader(filepath);
                var lenders = reader.Read();

                var interestCalculator = new CompundInterestCalculator();

                var finder = new LowestRateFinder(lenders, interestCalculator);
                var product = finder.Find(loanAmount);

                Console.WriteLine(string.Format("Requested amount: {0}",product.PrincipalAmount));
                Console.WriteLine(string.Format("Rate: {0}%",product.Rate*100));
                Console.WriteLine(string.Format("Monthly repayment: £{0}",
                    decimal.Round(product.MonthlyRepayment,2,MidpointRounding.AwayFromZero)));
                Console.WriteLine(string.Format("Total repayment: £{0}", 
                    decimal.Round(product.TotalRepayment,2,MidpointRounding.AwayFromZero)));
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }
    }
}
