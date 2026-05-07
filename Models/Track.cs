using System.ComponentModel.DataAnnotations;

namespace lab1___blazor.Models;

public class Track
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Track name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string? Description { get; set; }
}
