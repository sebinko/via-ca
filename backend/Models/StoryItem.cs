using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class StoryItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(5000)]
    public string Content { get; set; } = string.Empty;

    [StringLength(500)]
    public string? ImageUrl { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [StringLength(100)]
    public string? Author { get; set; }

    public bool IsPublished { get; set; } = true;

    [StringLength(200)]
    public string? Category { get; set; }
}
