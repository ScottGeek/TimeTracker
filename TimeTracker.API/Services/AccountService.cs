
using Microsoft.AspNetCore.Identity;

namespace TimeTracker.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AssignRole(string userName, string roleName)
        {
            if(!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.AddToRoleAsync(user!, roleName);

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
