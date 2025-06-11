using Business.Models;
using Business.Services;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserProfileController(UserProfileService userProfileService) : ControllerBase
{
    private readonly IUserProfileService _userProfileService = userProfileService;
    [HttpPost]
    public async Task<IActionResult> Create(CreateProfileRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.UserId) || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Email))
        {
            return BadRequest("Invalid profile data.");
        }
        var profileModel = new UserProfileModel
        {
            UserId = request.UserId,
            Username = request.Username,
            Email = request.Email
        };
        var result = await _userProfileService.CreateUserProfileAsync(profileModel);
        if (result.Success)
        {
            return Ok($"Created profile for user with id {request.UserId}");
        }
        return Problem($"An unexpected error occurred:{result.ErrorMessage}");

    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await _userProfileService.GetUserProfileAsync(id);
        if (result.Success && result.Data != default)
        {
            var model = result.Data;
            return Ok(model);
        }
        return Problem($"An unexpected error occurred:{result.ErrorMessage}");
    }
}
