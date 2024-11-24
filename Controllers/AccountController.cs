using HomeLoanApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeLoanApplication.Controllers
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

        // GET: api/account/exists/{accountId}
        [HttpGet("exists/{accountId}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> AccountExists(int accountId)
        {
            var exists = await _accountService.ValidateAccountExistsAsync(accountId);
            return Ok(exists);
        }
    }
}
