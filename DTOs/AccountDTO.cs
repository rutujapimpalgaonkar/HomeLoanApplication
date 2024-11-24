namespace HomeLoanApplication.DTOs
{
    public class AccountDTO
    {
        // public int AccountId { get; set; }
        public int ApplicationId { get; set; }  // Link to Loan Application
        public decimal Balance { get; set; }
    }
}
