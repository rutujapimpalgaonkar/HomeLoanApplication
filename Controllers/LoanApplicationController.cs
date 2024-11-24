// Controllers/LoanApplicationController.cs
using HomeLoanApplication.DTOs;
using HomeLoanApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeLoanApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanApplicationController : ControllerBase
    {
        private readonly ILoanApplicationService _loanApplicationService;

        public LoanApplicationController(ILoanApplicationService loanApplicationService)
        {
            _loanApplicationService = loanApplicationService;
        }

        // POST: api/loanapplication (Add)
        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<ActionResult> AddLoanApplication([FromBody] LoanApplicationDTO loanApplicationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loanApplicationId = await _loanApplicationService.AddLoanApplicationAsync(loanApplicationDTO);

            return CreatedAtAction(nameof(GetLoanApplication), new { id = loanApplicationId }, null);
        }

        // GET: api/loanapplication/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, user")]
        public async Task<ActionResult<LoanApplicationDTO>> GetLoanApplication(int id)
        {
            var loanApplication = await _loanApplicationService.GetLoanApplicationByIdAsync(id);
            if (loanApplication == null)
            {
                return NotFound();
            }

            return Ok(loanApplication);
        }

        // PUT: api/loanapplication/{id} (Update)
        [HttpPut("{id}")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<LoanApplicationDTO>> UpdateLoanApplication(int id, [FromBody] LoanApplicationDTO loanApplicationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedLoanApplication = await _loanApplicationService.UpdateLoanApplicationAsync(id, loanApplicationDTO);

            if (updatedLoanApplication == null)
            {
                return NotFound();
            }

            return Ok(updatedLoanApplication);
        }

        // DELETE: api/loanapplication/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteLoanApplication(int id)
        {
            var deleted = await _loanApplicationService.DeleteLoanApplicationAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
