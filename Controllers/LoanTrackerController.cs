using HomeLoanApplication.DTOs;
using HomeLoanApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeLoanApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanTrackerController : ControllerBase
    {
        private readonly ILoanTrackerService _loanTrackerService;

        public LoanTrackerController(ILoanTrackerService loanTrackerService)
        {
            _loanTrackerService = loanTrackerService;
        }

        // POST: api/loantracker (Add Loan Tracker)
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<LoanTrackerDTO>> AddLoanTracker([FromBody] LoanTrackerDTO loanTrackerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var loanTrackerId = await _loanTrackerService.AddLoanTrackerAsync(loanTrackerDTO);
                return CreatedAtAction(nameof(GetLoanTracker), new { id = loanTrackerId }, loanTrackerDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/loantracker/{id} (Get Loan Tracker by ID)
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, user")]
        public async Task<ActionResult<LoanTrackerDTO>> GetLoanTracker(int id)
        {
            var loanTrackerDTO = await _loanTrackerService.GetLoanTrackerByIdAsync(id);
            if (loanTrackerDTO == null)
            {
                return NotFound();
            }

            return Ok(loanTrackerDTO);
        }

        // PUT: api/loantracker/{id}/status (Update Loan Tracker Status)
        [HttpPut("{id}/status")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateLoanTrackerStatus(int id, [FromBody] string newStatus)
        {
            var success = await _loanTrackerService.UpdateLoanTrackerStatusAsync(id, newStatus);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/loantracker/{id} (Delete Loan Tracker)
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteLoanTracker(int id)
        {
            var success = await _loanTrackerService.DeleteLoanTrackerAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
