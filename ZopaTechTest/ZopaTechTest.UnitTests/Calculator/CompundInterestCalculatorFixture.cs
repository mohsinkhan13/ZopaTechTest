using NUnit.Framework;
using System;
using ZopaTechTest.Calculator;

namespace ZopaTechTest.UnitTests.Calculator
{
    [TestFixture]
    public class CompundInterestCalculatorFixture
    {

    [Test]
    public void CalculateTotalRepayment_Should_Return_Correct_Value()
    {
        var expectedTotalPayment = 1232.93m;
        var calculator = new CompundInterestCalculator();
        var result = calculator.CalculateTotalPayment(1000, 0.07m, 3);
        var actualTotalPayment = decimal.Round(result, 2, MidpointRounding.AwayFromZero);
        Assert.AreEqual(expectedTotalPayment,actualTotalPayment);
    }

    [Test]
    public void CalculateMonthlypayment_Should_Return_Correct_Value()
    {
        var expectedTotalPayment = 30.88m;
        var calculator = new CompundInterestCalculator();
        var result = calculator.CalculateMonthlyPayment(1000, 0.07m, 36);
        var actualPayment = decimal.Round(result, 2, MidpointRounding.AwayFromZero);
        Assert.AreEqual(expectedTotalPayment,actualPayment);
    }
    }
}
