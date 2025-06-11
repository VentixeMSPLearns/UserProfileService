using Data.Entities;
using Data.Results;

namespace Data.Repositories
{
    public interface IUserProfileRepository
    {
        Task<RepositoryResult<UserProfileEntity?>> CreateAsync(UserProfileEntity profileEntity);
        Task<RepositoryResult<UserProfileEntity?>> GetByIdAsync(string userId);
    }
}