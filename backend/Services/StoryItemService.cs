using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class StoryItemService : IStoryItemService
    {
        private readonly ApplicationDbContext _context;

        public StoryItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StoryItem>> GetAllAsync()
        {
            return await _context.StoryItems
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }

        public async Task<StoryItem?> GetByIdAsync(int id)
        {
            return await _context.StoryItems.FindAsync(id);
        }

        public async Task<StoryItem> CreateAsync(StoryItem item)
        {
            item.CreatedAt = DateTime.UtcNow;
            _context.StoryItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> UpdateAsync(StoryItem item)
        {
            var exists = await _context.StoryItems.AnyAsync(s => s.Id == item.Id);
            if (!exists) return false;

            _context.Entry(item).State = EntityState.Modified;
            _context.Entry(item).Property(x => x.CreatedAt).IsModified = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.StoryItems.FindAsync(id);
            if (item == null) return false;

            _context.StoryItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
