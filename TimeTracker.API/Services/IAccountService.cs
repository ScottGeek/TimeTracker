namespace TimeTracker.API.Services
{
    public interface IAccountService
    {

        Task<AccountRegistrationReponse> RegisterAsync(AccountRegistrationRequest request);

    }
}
