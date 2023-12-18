namespace TimeTracker.Client.Services
{
    public interface IAuthService
    {

        public Task<AccountRegistrationResponse> Register(AccountRegistrationRequest request);
        public Task<LoginResponse> Login(LoginRequest request);
        public Task Logout();
    }
}
