
using Data.Context;
using Data.Entities;
using Data.Results;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    protected readonly DataContext _context;
    protected readonly DbSet<UserProfileEntity> _profiles;
    public UserProfileRepository(DataContext context)
    {
        _context = context;
        _profiles = _context.Set<UserProfileEntity>();
    }

    public async Task<RepositoryResult<UserProfileEntity?>> GetByIdAsync(string userId)
    {
        try
        {
            var result = await _profiles.FindAsync(userId);
            if (result == null)
            {
                return new RepositoryResult<UserProfileEntity?>
                {
                    Success = false,
                    ErrorMessage = "User profile not found."
                };
            }
            return new RepositoryResult<UserProfileEntity?>
            {
                Success = true,
                Data = result
            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<UserProfileEntity?>
            {
                Success = false,
                ErrorMessage = $"An error occurred while retrieving the user profile: {ex.Message}"
            };
        }
    }
    public async Task<RepositoryResult<UserProfileEntity?>> CreateAsync(UserProfileEntity profileEntity)
    {
        try
        {
            var result = await _profiles.AddAsync(profileEntity);
            var saveResult = await _context.SaveChangesAsync();

            if (saveResult > 0)
            {
                return new RepositoryResult<UserProfileEntity?>
                {
                    Success = true,
                    Data = result.Entity
                };
            }
            else
            {
                return new RepositoryResult<UserProfileEntity?>
                {
                    Success = false,
                    ErrorMessage = "No changes were saved to the database."
                };
            }
        }
        catch (Exception ex)
        {
            return new RepositoryResult<UserProfileEntity?>
            {
                Success = false,
                ErrorMessage = $"An error occurred while creating the user profile: {ex.Message}"
            };
        }
    }
}
