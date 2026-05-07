using System.ComponentModel.DataAnnotations;

namespace TraineesAPI.Models;

public class Trainee
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Gender is required.")]
    public Gender? Gender { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mobile number is required.")]
    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    public string MobileNo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Birthdate is required.")]
    public DateTime? Birthdate { get; set; }

    public bool IsGraduated { get; set; }

    public int? TrackId { get; set; }

    // Navigation property
    public Track? Track { get; set; }
}
