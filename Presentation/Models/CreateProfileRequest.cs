namespace Presentation.Models;

public class CreateProfileRequest
{
    public string UserId { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!; 
}
