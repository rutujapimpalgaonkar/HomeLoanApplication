using HomeLoanApplication.Models;
using System.Threading.Tasks;

namespace HomeLoanApplication.Repositories
{
    public interface IDocumentRepository
    {
        Task<Document> GetDocumentByIdAsync(int id);
        Task<int> AddDocumentAsync(Document document);
        Task<bool> DocumentExistsAsync(int documentId);
        Task<bool> DeleteDocumentAsync(int id);
        // Task UpdateDocumentAsync(Document documentExists);
        Task<bool> UpdateDocumentAsync(Document document);
        // Task UpdateDocumentAsync(Document documentExists);
        // Task UpdateDocumentAsync(Document documentExists);
        // Task UpdateDocumentAsync(Document documentExists);
    }
}
