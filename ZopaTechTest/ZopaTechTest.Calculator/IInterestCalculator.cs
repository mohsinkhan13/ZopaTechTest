namespace ZopaTechTest.Calculator
{
    public interface IInterestCalculator
    {
        decimal CalculateTotalPayment(decimal principalAmount, decimal rate, int termInYear);
        decimal CalculateMonthlyPayment(decimal principalAmount, decimal rate, int termInMonth);
    }
}