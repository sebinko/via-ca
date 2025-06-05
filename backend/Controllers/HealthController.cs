using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HealthController> _logger;

    public HealthController(ApplicationDbContext context, ILogger<HealthController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            // Test database connection
            bool canConnect = await _context.Database.CanConnectAsync();
            
            if (canConnect)
            {
                _logger.LogInformation("Health check passed: Database connection successful");
                return Ok(new { status = "healthy", database = "connected" });
            }
            else
            {
                _logger.LogWarning("Health check warning: Cannot connect to database");
                return StatusCode(503, new { status = "unhealthy", database = "disconnected" });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Health check failed: Error connecting to database");
            return StatusCode(500, new { status = "error", message = "Error connecting to database" });
        }
    }
}
