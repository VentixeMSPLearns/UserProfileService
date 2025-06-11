using Business.Models;
using Business.Results;

namespace Business.Services
{
    public interface IUserProfileService
    {
        Task<ServiceResult> CreateUserProfileAsync(UserProfileModel profileModel);
        Task<ServiceResult<UserProfileModel>> GetUserProfileAsync(string userId);
    }
}