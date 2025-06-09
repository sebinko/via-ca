using backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Services
{
    public interface IStoryItemService
    {
        Task<IEnumerable<StoryItem>> GetAllAsync();
        Task<StoryItem?> GetByIdAsync(int id);
        Task<StoryItem> CreateAsync(StoryItem item);
        Task<bool> UpdateAsync(StoryItem item);
        Task<bool> DeleteAsync(int id);
    }
}
