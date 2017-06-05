using System;
using System.Collections.Generic;
using System.Linq;
using ZopaTechTest.Calculator;
using ZopaTechTest.Models;

namespace ZopaTechTest.ProductFinder
{
    public class LowestRateFinder
    {
        private IInterestCalculator _compundInterestCalculator;
        private IEnumerable<LenderDetail> _source;

        public LowestRateFinder(IEnumerable<LenderDetail> source,
            IInterestCalculator calculator)
        {
            _source = source;
            _compundInterestCalculator = calculator;
        }

        public Product Find(int amountRequested)
        {
            Product product = null;
            try
            {
                var bestRate = _source
                    .GroupBy(x => decimal.Round(x.Rate, 2, MidpointRounding.AwayFromZero))
                    .Select(g => new { Rate = g.Key, Sum = g.Sum(x => x.AvailableFund) })
                    .Where(g => g.Sum >= amountRequested)
                    .OrderBy(g => g.Rate)
                    .First();

                product = new Product { PrincipalAmount = amountRequested, Rate = bestRate.Rate };
                product.TotalRepayment = _compundInterestCalculator.CalculateTotalPayment(
                    amountRequested, bestRate.Rate, product.Term / 12);
                product.MonthlyRepayment = _compundInterestCalculator.CalculateMonthlyPayment(
                    amountRequested, bestRate.Rate, product.Term);
            }
            catch (Exception)
            {
                throw new Exception("Sorry, it is not possible to provide a quote at this time.");
            }

            return product;
        }
    }
}
