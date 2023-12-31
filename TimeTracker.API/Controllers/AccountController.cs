﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TimeTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<AccountRegistrationResponse>> Register(AccountRegistrationRequest request)
        {
            var result = await _accountService.RegisterAsync(request);
            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("role")]
        public async Task<IActionResult> AssignRole(string userName, string roleName)
        {
            await _accountService.AssignRole(userName, roleName);
            return Ok();
        }

    }
}
