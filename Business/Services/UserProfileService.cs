using Business.Models;
using Business.Results;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;

public class UserProfileService(IUserProfileRepository userProfileRepository) : IUserProfileService
{
    private readonly IUserProfileRepository _userProfileRepository = userProfileRepository;

    public async Task<ServiceResult<UserProfileModel>> GetUserProfileAsync(string userId)
    {
        var result = await _userProfileRepository.GetByIdAsync(userId);
        if (result.Success && result.Data != default)
        {
            var profileModel = new UserProfileModel
            {
                UserId = result.Data.UserId,
                Username = result.Data.Username,
                Email = result.Data.Email,
                AvatarUrl = result.Data.AvatarUrl
            };
            return new ServiceResult<UserProfileModel>
            {
                Success = true,
                Data = profileModel
            };
        }
        return new ServiceResult<UserProfileModel>
        {
            Success = false,
            ErrorMessage = result.ErrorMessage
        };
    }
    public async Task<ServiceResult> CreateUserProfileAsync(UserProfileModel profileModel)
    {
        var profileEntity = new UserProfileEntity
        {
            UserId = profileModel.UserId,
            Username = profileModel.Username,
            Email = profileModel.Email,
            AvatarUrl = profileModel.AvatarUrl
        };
        var result = await _userProfileRepository.CreateAsync(profileEntity);

        if (result.Success && result.Data != default)
        {
            return new ServiceResult { Success = true };
        }
        return new ServiceResult
        {
            Success = false,
            ErrorMessage = result.ErrorMessage
        };
    }
}
