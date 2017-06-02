using System;

namespace ZopaTechTest.Calculator
{
    public class CompundInterestCalculator : IInterestCalculator
    {
        private const int TimesPerYear = 12;
        public decimal CalculateMonthlyPayment(decimal principalAmount, decimal rate, int termInMonth)
        {
            //formula - //P * ( J / (1 - (1 + J)^-N))

            //J
            var monthlyRate = rate / 12;
            
            //(1+j)^-n
            var powResult = Math.Pow((double)(1 + monthlyRate), -termInMonth);

            //j/1-(1+j)^n
            var body = monthlyRate / (decimal)(1 - powResult);

            var monthlyPayment = principalAmount * body;

            //P * ( J / (1 - (1 + J)^-N))
            return monthlyPayment; 

        }

        public decimal CalculateTotalPayment(decimal principalAmount, decimal rate,int termInYear)
        {
            decimal body = 1 + (rate / TimesPerYear);

            // nt
            double exponent = TimesPerYear * (termInYear);

            // P(1 + r/n)^nt
            return principalAmount * (decimal)Math.Pow((double)body, exponent);
        }
    }
}
