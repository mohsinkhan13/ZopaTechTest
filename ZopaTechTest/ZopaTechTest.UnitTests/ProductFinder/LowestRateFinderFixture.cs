using NUnit.Framework;
using System.Collections.Generic;
using ZopaTechTest.Calculator;
using ZopaTechTest.Models;
using ZopaTechTest.ProductFinder;

namespace ZopaTechTest.UnitTests.ProductFinder
{
    [TestFixture]
    public class LowestRateFinderFixture
    {
        private IEnumerable<LenderDetail> _lenders;
        private IInterestCalculator _calculator;
        private LowestRateFinder _finder;

        [SetUp]
        public void Setup()
        {
            _lenders = new List<LenderDetail>
            {
                new LenderDetail {LenderName = "Bob",  Rate = 0.075m, AvailableFund= 640 },
                new LenderDetail {LenderName = "Jane", Rate = 0.069m, AvailableFund = 480 },
                new LenderDetail {LenderName = "Fred", Rate = 0.071m, AvailableFund = 520 },
                new LenderDetail {LenderName = "Mary", Rate = 0.104m, AvailableFund = 170 },
                new LenderDetail {LenderName = "John", Rate = 0.081m, AvailableFund = 320 },
                new LenderDetail {LenderName = "Dave", Rate = 0.074m, AvailableFund = 140 },
                new LenderDetail {LenderName = "Angela", Rate = 0.071m, AvailableFund = 60 }
            };
            _calculator = new CompundInterestCalculator();
            _finder = new LowestRateFinder(_lenders, _calculator);
        }

        [Test]
        public void LowestRateFinder_Find_Returns_LowestRateAs_7()
        {
            var amountRequested = 1000;

            var result = _finder.Find(amountRequested);

            Assert.AreEqual(0.07, result.Rate);
        }

        [Test]
        public void LowestRateFinder_Find_Sets_MonthlyPayment()
        {
            var amountRequested = 1000;

            var result = _finder.Find(amountRequested);

            Assert.NotZero(result.MonthlyRepayment);
        }

        [Test]
        public void LowestRateFinder_Find_Sets_TotalRepayment()
        {
            var amountRequested = 1000;

            var result = _finder.Find(amountRequested);

            Assert.NotZero(result.TotalRepayment);
        }
    }
}
