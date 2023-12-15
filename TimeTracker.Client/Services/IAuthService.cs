namespace TimeTracker.Client.Services
{
    public interface IAuthService
    {

        public Task Register(AccountRegistrationRequest request);
        public Task Login(LoginRequest request);
        public Task Logout();
    }
}
