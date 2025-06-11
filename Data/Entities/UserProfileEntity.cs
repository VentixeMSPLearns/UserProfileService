using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class UserProfileEntity
{
    [Key]
    public string UserId { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string AvatarUrl { get; set; } = "https://avatar.iran.liara.run/public";
}
