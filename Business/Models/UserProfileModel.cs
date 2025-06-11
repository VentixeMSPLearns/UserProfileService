using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models;

public class UserProfileModel
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string AvatarUrl { get; set; } = "https://avatar.iran.liara.run/public";
}
