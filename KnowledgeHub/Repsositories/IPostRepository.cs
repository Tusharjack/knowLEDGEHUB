using KnowledgeHub.Models;

namespace KnowledgeHub.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post?> GetByIdAsync(Guid id);
        Task<int> AddAsync(Post post);
        Task<int> UpdateAsync(Post post);
        Task<int> DeleteAsync(Guid id);
    }
}