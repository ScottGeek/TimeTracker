
using Microsoft.AspNetCore.Identity;

namespace TimeTracker.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;

        public AccountService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AccountRegistrationResponse> RegisterAsync(AccountRegistrationRequest request)
        {
            var newUser = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);
            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);
                return new AccountRegistrationResponse
                {
                    IsSuccessful = false,
                    Errors = errors
                };
            }

            return new AccountRegistrationResponse
            {
                IsSuccessful = true
            };

        }
    }
}
