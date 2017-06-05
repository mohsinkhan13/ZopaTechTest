using System;
using System.Configuration;
using ZopaTechTest.Calculator;
using ZopaTechTest.FileReader;
using ZopaTechTest.ProductFinder;

namespace ZopaTechTest.ConsoleApp
{
    class Program
    {
        private static string _filePath = string.Empty;
        private static int _loanAmount = 0;
        static void Main(string[] args)
        {
            string validationError = string.Empty;
            if(!ValidateArguments(args, out validationError))
            {
                Console.WriteLine(validationError);
                return;
            }

            _filePath = args[0];
            _loanAmount = int.Parse(args[1]);

            if(!ValidateLoanAmount(_loanAmount,out validationError))
            {
                Console.WriteLine(validationError);
                return;
            }

            try
            {
                var reader = new CsvFileReader(_filePath);
                var lenders = reader.Read();

                var interestCalculator = new CompundInterestCalculator();

                var finder = new LowestRateFinder(lenders, interestCalculator);
                var product = finder.Find(_loanAmount);

                Console.WriteLine(string.Format("Requested amount: {0}", product.PrincipalAmount));
                Console.WriteLine(string.Format("Rate: {0}%", product.Rate * 100));
                Console.WriteLine(string.Format("Monthly repayment: £{0}",
                    decimal.Round(product.MonthlyRepayment, 2, MidpointRounding.AwayFromZero)));
                Console.WriteLine(string.Format("Total repayment: £{0}",
                    decimal.Round(product.TotalRepayment, 2, MidpointRounding.AwayFromZero)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static bool ValidateLoanAmount(int loanAmount, out string validationError)
        {
            var incrementBy100Only = ConfigurationManager.AppSettings["incrementBy100Only"] ?? "true";
            var minimumLoanKeyValue = ConfigurationManager.AppSettings["minimumLoanValue"] ?? "1000";
            var maximumLoanKeyValue = ConfigurationManager.AppSettings["maximumLoanValue"] ?? "15000";
            int minimumLoanValue, maximumLoanValue = 0;
            try
            {
                minimumLoanValue = int.Parse(minimumLoanKeyValue);
            }
            catch
            {
                //default
                minimumLoanValue = 1000;
            }

            try
            {
                maximumLoanValue = int.Parse(maximumLoanKeyValue);
            }
            catch
            {
                //default
                maximumLoanValue = 15000;
            }

            if (incrementBy100Only.ToLower() == "true" && loanAmount % 100 != 0)
            {
                validationError = "Invalid Loan amount. Only increments of 100 are allowed";
                return false;
            }

            if(loanAmount < minimumLoanValue || loanAmount > maximumLoanValue)
            {
                validationError = string.Format("Invalid loan amount. Loans are only available between {0} and {1}.",minimumLoanValue, maximumLoanValue);
                return false;
            }
            validationError = string.Empty;
            return true;
        }

        public static bool ValidateArguments(string[] args, out string errorMessage)
        {
            if (args.Length < 2)
            {
                errorMessage = "Invalid arguments";
                return false;
            }

            if (string.IsNullOrEmpty(args[0]))
            {
                errorMessage = "Invalid file provided";
                return false;
            }

            int loanAmount = 0;
            var parsed = int.TryParse(args[1], out loanAmount);
            if (!parsed || loanAmount <= 0)
            {

                errorMessage = "Please enter valid loan amount";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }


        
    }
}
