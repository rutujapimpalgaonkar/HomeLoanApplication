using HomeLoanApplication.DTOs;
using HomeLoanApplication.Models;
using HomeLoanApplication.Repositories;
using System;
using System.Threading.Tasks;

namespace HomeLoanApplication.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ILoanApplicationRepository _loanApplicationRepository;

        public DocumentService(IDocumentRepository documentRepository, ILoanApplicationRepository loanApplicationRepository)
        {
            _documentRepository = documentRepository;
            _loanApplicationRepository = loanApplicationRepository;
        }

        // Get document by Id
        public async Task<DocumentDTO> GetDocumentByIdAsync(int id)
        {
            var document = await _documentRepository.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return null; // Document not found
            }

            return new DocumentDTO
            {
                DocumentId = document.DocumentId,
                ApplicationId = (int)document.ApplicationId,  // ApplicationId needs to be included
                DocumentType = document.DocumentType,
                DocumentUrl = document.DocumentUrl
            };
        }

        // Add new document (POST)
        public async Task<int> AddDocumentAsync(DocumentDTO documentDTO)
        {
            // Step 1: Validate if the ApplicationId exists in the LoanApplication table
            var applicationExists = await _loanApplicationRepository.GetLoanApplicationByIdAsync(documentDTO.ApplicationId);
            if (applicationExists == null)
            {
                throw new ArgumentException($"Loan application with ApplicationId {documentDTO.ApplicationId} does not exist.");
            }

            // Step 2: Map DTO to Model
            var document = new Document
            {
                ApplicationId = documentDTO.ApplicationId,  // Mapping ApplicationId to the Document model
                DocumentType = documentDTO.DocumentType,
                DocumentUrl = documentDTO.DocumentUrl
            };

            // Step 3: Add document to the database
            return await _documentRepository.AddDocumentAsync(document);
        }

        // Delete document (DELETE)
        public async Task<bool> DeleteDocumentAsync(int id)
        {
            return await _documentRepository.DeleteDocumentAsync(id);
        }

        // Update document (PUT)
        public async Task<bool> UpdateDocumentAsync(DocumentDTO documentDTO)
        {
            // Validate if the document exists before updating
            var document = await _documentRepository.GetDocumentByIdAsync(documentDTO.DocumentId);
            if (document == null)
            {
                return false; // Document not found
            }

            // Map DTO to the document model
            document.ApplicationId = documentDTO.ApplicationId;  // Updating ApplicationId
            document.DocumentType = documentDTO.DocumentType;
            document.DocumentUrl = documentDTO.DocumentUrl;

            // Update the document in the repository
            return await _documentRepository.UpdateDocumentAsync(document);
        }

        public Task<bool> ValidateApplicationIdExistsAsync(int applicationId)
        {
            throw new NotImplementedException();
        }

        public Task GetDocumentByTypeAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
