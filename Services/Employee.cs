using Assignment.Data;
using Assignment.DTO;
using Assignment.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Assignment.Services;

public class Employee : IEmployee
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public Employee(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IQueryable<UserDTO>> GetAllUsersAsync()
    {
        try
        {
            var allusers = await _context.Users.ToListAsync();
            List<UserDTO> model = [];

            foreach(var myUser in allusers)
            {
                // List of roles for each user
                var rolesList = await (from user in _context.Users
                                       join userRoles in _context.UserRoles on user.Id equals userRoles.UserId
                                       join role in _context.Roles on userRoles.RoleId equals role.Id
                                       select new { UserId = user.Id, UserName = user.UserName, RoleId = role.Id, RoleName = role.Name })
                                    .Where(t => t.UserId == myUser.Id)
                                    .Select(x => x.RoleName)
                                    .ToListAsync();
                UserDTO tempUser = new()
                {
                    Id = myUser.Id,
                    Email = myUser.Email!,
                    UserName = myUser.UserName!,
                    Salary = myUser.Salary,
                    Position = myUser.Position,
                    Roles = rolesList,
                };
                model.Add(tempUser);
            }

            return model.AsQueryable();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching user details: {ex.Message}");
            throw;
        }
    }

    public async Task<UserDTO> GetUserByIdAsync(string id)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is not null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                
                return new UserDTO
                {
                    Id = user!.Id,
                    Email = user.Email!,
                    UserName = user.UserName!,
                    Salary = user.Salary,
                    Position = user.Position,
                    Roles = (List<string>)roles,
                };

            } else
            {
                return new UserDTO();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching user details: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> UpdateUserAsync(UserDTO updatedUser)
    {
        try
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == updatedUser.Id);
            if (foundUser is not null)
            {
                foundUser.Email = updatedUser.Email;
                foundUser.UserName = updatedUser.UserName;
                foundUser.Salary = updatedUser.Salary;
                foundUser.Position = updatedUser.Position;

                foreach (var role in updatedUser.Roles)
                    await _userManager.AddToRoleAsync(foundUser, role);

                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching user details: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> DeleteUserAsync(string id)
    {
        try
        {
            var foundUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (foundUser is not null)
            {
                await _userManager.DeleteAsync(foundUser);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching user details: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> AddUserAsync(UserDTO newUser)
    {
        try
        {
            var userEntity = new ApplicationUser
            {
                UserName = newUser.UserName,
                Email = newUser.Email,
                EmailConfirmed = true,
                Position = newUser.Position,
                Salary = newUser.Salary
            };

            var newUserResult = await _userManager.CreateAsync(userEntity, "Admin123");
            if (newUserResult.Succeeded)
            {
                // Assign roles to the new user
                await _userManager.AddToRolesAsync(userEntity, newUser.Roles);

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching user details: {ex.Message}");
            throw;
        }
    }   

    public async Task<string[]> GetAllRolesAsync()
    {
        try
        {
            var roles = await _context.Roles.Select(r => r.Name).ToArrayAsync();
            return roles;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching user details: {ex.Message}");
            throw;
        }
    }

}
