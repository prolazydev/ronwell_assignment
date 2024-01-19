using Assignment.Data;
using Assignment.DTO;

namespace Assignment.Services.Contracts;

public interface IEmployee
{
    /// <summary>
    /// Gets a list of users.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a
    /// list of <see cref="ApplicationUser"/> instances.
    /// </returns>
    Task<IQueryable<UserDTO>> GetAllUsersAsync();
    Task<UserDTO> GetUserByIdAsync(string id);
    Task<bool> UpdateUserAsync(UserDTO user);
    Task<bool> DeleteUserAsync(string id);
    Task<bool> AddUserAsync(UserDTO user);
    Task<string[]> GetAllRolesAsync();
}
