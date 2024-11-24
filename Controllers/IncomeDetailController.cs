using HomeLoanApplication.DTOs;
using HomeLoanApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeLoanApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeDetailController : ControllerBase
    {
        private readonly IIncomeDetailService _incomeDetailService;

        public IncomeDetailController(IIncomeDetailService incomeDetailService)
        {
            _incomeDetailService = incomeDetailService;
        }

        // POST: api/incomedetail (Add IncomeDetail)
        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<IncomeDetailDTO>> AddIncomeDetail([FromBody] IncomeDetailDTO incomeDetailDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate if the ApplicationId exists
            var loanApplicationExists = await _incomeDetailService.ValidateApplicationIdExistsAsync(incomeDetailDTO.ApplicationId);
            if (!loanApplicationExists)
            {
                return BadRequest($"LoanApplication with ApplicationId {incomeDetailDTO.ApplicationId} does not exist.");
            }

            // Add the IncomeDetail
            var incomeDetailId = await _incomeDetailService.AddIncomeDetailAsync(incomeDetailDTO);

            // Return the newly created resource
            var createdIncomeDetail = await _incomeDetailService.GetIncomeDetailByIdAsync(incomeDetailId);
            return CreatedAtAction(nameof(GetIncomeDetail), new { id = createdIncomeDetail.IncomeDetailId }, createdIncomeDetail);
        }

        // GET: api/incomedetail/{id} (Get IncomeDetail by ID)
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, user")]
        public async Task<ActionResult<IncomeDetailDTO>> GetIncomeDetail(int id)
        {
            var incomeDetail = await _incomeDetailService.GetIncomeDetailByIdAsync(id);
            if (incomeDetail == null)
            {
                return NotFound($"IncomeDetail with ID {id} not found.");
            }

            return Ok(incomeDetail);
        }

        // PUT: api/incomedetail/{id} (Update IncomeDetail)
        [HttpPut("{id}")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<IncomeDetailDTO>> UpdateIncomeDetail(int id, [FromBody] IncomeDetailDTO incomeDetailDTO)
        {
            try
            {
                // Check if the IncomeDetail exists
                var updatedIncomeDetail = await _incomeDetailService.UpdateIncomeDetailAsync(id, incomeDetailDTO);
                if (updatedIncomeDetail == null)
                {
                    return NotFound($"IncomeDetail with ID {id} not found.");
                }

                // Return the updated IncomeDetail
                return Ok(updatedIncomeDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/incomedetail/{id} (Delete IncomeDetail)
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteIncomeDetail(int id)
        {
            try
            {
                var success = await _incomeDetailService.DeleteIncomeDetailAsync(id);
                if (!success)
                {
                    return NotFound($"IncomeDetail with ID {id} not found.");
                }

                return NoContent(); // Successfully deleted
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
