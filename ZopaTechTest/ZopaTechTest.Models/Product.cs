namespace ZopaTechTest.Models
{
    public class Product
    {
        public int PrincipalAmount { get; set; }
        public int Term{ get;} = 36;
        public int TimesPerYear { get; } = 12;
        public decimal Rate { get; set; }
        public decimal MonthlyRepayment { get;set;}
        public decimal TotalRepayment {get;set;}
        
    }
}
