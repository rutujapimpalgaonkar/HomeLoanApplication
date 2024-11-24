using HomeLoanApplication.DTOs;
using HomeLoanApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeLoanApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        // GET: api/documents/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> GetDocument(int id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound(); // Document not found
            }
            return Ok(document);
        }

        // POST: api/documents
        [HttpPost("User")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> AddDocument([FromBody] DocumentDTO documentDTO)
        {
            if (documentDTO == null)
            {
                return BadRequest("Invalid document data.");
            }

            try
            {
                var documentId = await _documentService.AddDocumentAsync(documentDTO);
                return CreatedAtAction(nameof(GetDocument), new { id = documentId }, documentDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Return the error message if the loan application does not exist
            }
        }

        // PUT: api/documents/{id}
        [HttpPut("{id}/User")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] DocumentDTO documentDTO)
        {
            if (documentDTO == null || documentDTO.DocumentId != id)
            {
                return BadRequest("Document data is invalid.");
            }

            var isUpdated = await _documentService.UpdateDocumentAsync(documentDTO);
            if (!isUpdated)
            {
                return NotFound("Document not found.");
            }

            return Ok(new { message = "Successfully Updated" });
        }

        // DELETE: api/documents/{id}
        [HttpDelete("{id}/Admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var isDeleted = await _documentService.DeleteDocumentAsync(id);
            if (!isDeleted)
            {
                return NotFound("Document not found.");
            }

            return Ok(new { message = "Successfully deleted" });
        }
    }
}
