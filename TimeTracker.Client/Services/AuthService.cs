
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Query;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TimeTracker.Client.Services
{
    public class AuthService : IAuthService
    {

        private readonly HttpClient _httpClient;
        private readonly IToastService _toastService;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient httpClient,
                           IToastService toastService,
                           NavigationManager navigationManager,
                           ILocalStorageService localStorageService,
                           AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _toastService = toastService;
            _navigationManager = navigationManager;
            _localStorage = localStorageService;
            _authStateProvider = authStateProvider;
        }

        public async Task Login(LoginRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/login", request);

            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
                if (!response.IsSuccessful && response.Error != null)
                {
                    _toastService.ShowError(response.Error);
                }
                else
                  if (!response.IsSuccessful)
                  {
                    _toastService.ShowError("An unexpected error occuried with registraion!");
                  }
                  else
                  {

                    if(response.Token != null)
                    {
                        await _localStorage.SetItemAsStringAsync("authToken", response.Token);
                        await _authStateProvider.GetAuthenticationStateAsync();
                    }

                    _toastService.ShowSuccess("You are now logged in!");
                    _navigationManager.NavigateTo("timeentries");
                  }
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _authStateProvider.GetAuthenticationStateAsync();
            _toastService.ShowSuccess("You are now logged out!");
            _navigationManager.NavigateTo("/login");
        }

        public async Task Register(AccountRegistrationRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/account/register", request);

            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<AccountRegistrationResponse>();
                if (!response.IsSuccessful && response.Errors != null)
                {
                    foreach (var error in response.Errors)
                    {
                        _toastService.ShowError(error);
                    }
                }
                else
                {
                    if (!response.IsSuccessful)
                    {
                        _toastService.ShowError("An unexpected error occuried with registraion!");
                    }
                    else
                    {
                        _toastService.ShowSuccess("Registration successful! You may login now.");
                    }
                }
            }

        }
    }
}
