using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    public DbSet<StoryItem> StoryItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed some initial data
        modelBuilder.Entity<StoryItem>().HasData(
            new StoryItem 
            { 
                Id = 1, 
                Title = "New Scientific Discovery Changes Everything", 
                Content = "Scientists have made an incredible breakthrough that could revolutionize our understanding of the universe.",
                Author = "Jane Smith",
                Category = "Science",
                CreatedAt = DateTime.Parse("2025-05-20T10:30:00Z")
            },
            new StoryItem 
            { 
                Id = 2, 
                Title = "Local Hero Saves Cat From Tree", 
                Content = "A local resident climbed a 30-foot tree to rescue a stranded cat, becoming the town's newest hero.",
                Author = "John Doe",
                Category = "Local News", 
                CreatedAt = DateTime.Parse("2025-05-21T14:15:00Z")
            },
            new StoryItem 
            { 
                Id = 3, 
                Title = "Celebrity Scandal Rocks Hollywood", 
                Content = "A major celebrity has been involved in a shocking scandal that has left fans stunned.",
                Author = "Gossip Reporter",
                Category = "Entertainment",
                CreatedAt = DateTime.Parse("2025-05-22T09:45:00Z")
            }
        );
    }
}
