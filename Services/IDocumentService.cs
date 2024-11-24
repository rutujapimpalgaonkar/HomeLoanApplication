using HomeLoanApplication.DTOs;
using System.Threading.Tasks;

namespace HomeLoanApplication.Services
{
    public interface IDocumentService
    {
        Task<DocumentDTO> GetDocumentByIdAsync(int id);
        Task<int> AddDocumentAsync(DocumentDTO documentDTO);
        Task<bool> DeleteDocumentAsync(int id);
        Task<bool> ValidateApplicationIdExistsAsync(int applicationId);  // To validate if ApplicationId exists
        Task<bool> UpdateDocumentAsync(DocumentDTO document);
        Task GetDocumentByTypeAsync(int id);
    }
}
