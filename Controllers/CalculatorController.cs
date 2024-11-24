using HomeLoanApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeLoanApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class CalculatorController : ControllerBase
    {
        // GET: api/calculator/eligibility?monthlyIncome=50000
        [HttpGet("eligibility/admin")]
        public ActionResult<decimal> CalculateEligibility(decimal monthlyIncome)
        {
            if (monthlyIncome <= 0)
            {
                return BadRequest("Monthly income must be greater than 0.");
            }

            // Eligibility formula: 60 * (0.6 * monthlyIncome)
            var loanEligibility = 60 * (0.6m * monthlyIncome);
            return Ok($"You are eligible. \nAccording to your income, you will get {loanEligibility}.");

        }

        // GET: api/calculator/emi?loanAmount=1000000&loanTenure=20
        [HttpGet("emi/admin")]
        public ActionResult<decimal> CalculateEMI(decimal loanAmount, int loanTenure)
        {
            if (loanAmount <= 0 || loanTenure <= 0)
            {
                return BadRequest("Loan amount and loan tenure must be greater than 0.");
            }

            // EMI formula: P * R * {((1 + R) ^ n) / ((1 + R) ^ n - 1)}
            decimal annualInterestRate = 8.5m; // 8.5% annual interest rate
            decimal monthlyInterestRate = annualInterestRate / 12 / 100; // Convert to monthly and percentage
            int numberOfInstallments = loanTenure * 12; // Convert years to months

            decimal emi = loanAmount * monthlyInterestRate * (decimal)Math.Pow(1 + (double)monthlyInterestRate, numberOfInstallments) /
                          (decimal)(Math.Pow(1 + (double)monthlyInterestRate, numberOfInstallments) - 1);

            return Ok(emi);
        }

        // Inject the IEmailService
        private readonly IEmailService _emailService;

        public CalculatorController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        // POST: api/calculator/send-email
        [HttpPost("send-email/admin")]
        public async Task<IActionResult> SendLoanEligibilityEmail(string email, decimal monthlyIncome)
        {
            if (monthlyIncome <= 0)
            {
                return BadRequest("Monthly income must be greater than 0.");
            }

            // Eligibility calculation
            var loanEligibility = 60 * (0.6m * monthlyIncome);

            // Email content
            string subject = "Your Loan Eligibility Status";
            string body = $"Hello,\n\nYour estimated loan eligibility is: ₹{loanEligibility}\n\nBest Regards,\nHome Loan Application Team";

            // Send the email
            try
            {
                await _emailService.SendEmailAsync(email, subject, body);
                return Ok("Email sent successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
