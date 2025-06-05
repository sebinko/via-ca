using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoryItemsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<StoryItemsController> _logger;

    public StoryItemsController(ApplicationDbContext context, ILogger<StoryItemsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/StoryItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StoryItem>>> GetStoryItems()
    {
        _logger.LogInformation("Getting all story items");
        return await _context.StoryItems.OrderByDescending(s => s.CreatedAt).ToListAsync();
    }

    // GET: api/StoryItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<StoryItem>> GetStoryItem(int id)
    {
        _logger.LogInformation("Getting story item with id {Id}", id);
        var storyItem = await _context.StoryItems.FindAsync(id);

        if (storyItem == null)
        {
            _logger.LogWarning("Story item with id {Id} not found", id);
            return NotFound();
        }

        return storyItem;
    }

    // POST: api/StoryItems
    [HttpPost]
    public async Task<ActionResult<StoryItem>> PostStoryItem(StoryItem storyItem)
    {
        _logger.LogInformation("Creating a new story item");
        storyItem.CreatedAt = DateTime.UtcNow;
        _context.StoryItems.Add(storyItem);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Story item created with id {Id}", storyItem.Id);
        return CreatedAtAction(nameof(GetStoryItem), new { id = storyItem.Id }, storyItem);
    }

    // PUT: api/StoryItems/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStoryItem(int id, StoryItem storyItem)
    {
        if (id != storyItem.Id)
        {
            _logger.LogWarning("Bad request: id {Id} doesn't match story item id {StoryItemId}", id, storyItem.Id);
            return BadRequest();
        }

        _context.Entry(storyItem).State = EntityState.Modified;
        // Preserve the original creation date
        _context.Entry(storyItem).Property(x => x.CreatedAt).IsModified = false;

        try
        {
            _logger.LogInformation("Updating story item with id {Id}", id);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StoryItemExists(id))
            {
                _logger.LogWarning("Story item with id {Id} not found during update", id);
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/StoryItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStoryItem(int id)
    {
        _logger.LogInformation("Deleting story item with id {Id}", id);
        var storyItem = await _context.StoryItems.FindAsync(id);
        if (storyItem == null)
        {
            _logger.LogWarning("Story item with id {Id} not found during delete attempt", id);
            return NotFound();
        }

        _context.StoryItems.Remove(storyItem);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Story item with id {Id} deleted successfully", id);
        return NoContent();
    }

    private bool StoryItemExists(int id)
    {
        return _context.StoryItems.Any(e => e.Id == id);
    }
}
