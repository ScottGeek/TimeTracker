﻿namespace TimeTracker.API.Services
{
    public interface IAccountService
    {

        Task<AccountRegistrationResponse> RegisterAsync(AccountRegistrationRequest request);

    }
}