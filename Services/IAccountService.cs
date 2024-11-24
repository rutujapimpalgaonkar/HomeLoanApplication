namespace HomeLoanApplication.Services
{
    public interface IAccountService
    {
        Task<bool> DeleteAccountAsync(int accountId);
        Task GetAllAccountsAsync();
        Task<bool> ValidateAccountExistsAsync(int accountId); // Validate AccountId
    }
}
