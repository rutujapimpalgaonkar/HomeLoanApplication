using HomeLoanApplication.Data;
using HomeLoanApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HomeLoanApplication.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly HomeLoanContext _context;

        public DocumentRepository(HomeLoanContext context)
        {
            _context = context;
        }

        // GET: Fetch a document by its ID
        public async Task<Document> GetDocumentByIdAsync(int documentId)
        {
            return await _context.Documents
                                 .FirstOrDefaultAsync(d => d.DocumentId == documentId);
        }

        // POST: Add a new document
        public async Task<int> AddDocumentAsync(Document document)
        {
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
            return document.DocumentId;
        }

        // DELETE: Delete a document by its ID
        public async Task<bool> DeleteDocumentAsync(int documentId)
        {
            var document = await _context.Documents.FindAsync(documentId);
            if (document == null)
            {
                return false; // Document not found
            }

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return true; // Successfully deleted
        }

        // PUT: Update an existing document
        public async Task<bool> UpdateDocumentAsync(Document document)
        {
            // First, check if the document exists
            var existingDocument = await _context.Documents.FindAsync(document.DocumentId);
            if (existingDocument == null)
            {
                return false; // Document not found
            }

            // Update document properties (or overwrite as needed)
            existingDocument.ApplicationId = document.ApplicationId;
            existingDocument.DocumentType = document.DocumentType;
            existingDocument.DocumentUrl = document.DocumentUrl;

            // Save changes
            await _context.SaveChangesAsync();
            return true;
        }

        // public Task<bool> DocumentExistsAsync(int documentId)
        // {
        //     throw new NotImplementedException();
        // }

        // Task IDocumentRepository.UpdateDocumentAsync(Document documentExists)
        // {
        //     throw new NotImplementedException();
        // }

        public Task<bool> DocumentExistsAsync(int documentId)
        {
            throw new NotImplementedException();
        }
    }
}
