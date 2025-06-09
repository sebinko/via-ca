using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Services;
using Microsoft.Extensions.Logging;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoryItemsController : ControllerBase
{    
    private readonly IStoryItemService _service;
    private readonly ILogger<StoryItemsController> _logger;

    public StoryItemsController(IStoryItemService service, ILogger<StoryItemsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // GET: api/StoryItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StoryItem>>> GetStoryItems()
    {
        _logger.LogInformation("Getting all story items");
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    // GET: api/StoryItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<StoryItem>> GetStoryItem(int id)
    {
        _logger.LogInformation("Getting story item with id {Id}", id);
        var storyItem = await _service.GetByIdAsync(id);

        if (storyItem == null)
        {
            _logger.LogWarning("Story item with id {Id} not found", id);
            return NotFound();
        }

        return Ok(storyItem);
    }

    // POST: api/StoryItems
    [HttpPost]
    public async Task<ActionResult<StoryItem>> PostStoryItem(StoryItem storyItem)
    {
        _logger.LogInformation("Creating a new story item");
        var created = await _service.CreateAsync(storyItem);

        _logger.LogInformation("Story item created with id {Id}", storyItem.Id);
        return CreatedAtAction(nameof(GetStoryItem), new { id = created.Id }, created);
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

        var updated = await _service.UpdateAsync(storyItem);
        if (!updated)
        {
            _logger.LogWarning("Story item with id {Id} not found during update", id);
            return NotFound();
        }

        _logger.LogInformation("Story item with id {Id} updated successfully", id);
        return NoContent();
    }

    // DELETE: api/StoryItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStoryItem(int id)
    {
        _logger.LogInformation("Deleting story item with id {Id}", id);
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
        {
            _logger.LogWarning("Story item with id {Id} not found during delete attempt", id);
            return NotFound();
        }

        _logger.LogInformation("Story item with id {Id} deleted successfully", id);
        return NoContent();
    }
}
