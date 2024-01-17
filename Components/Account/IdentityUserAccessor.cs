using Microsoft.AspNetCore.Identity;
using ronwell_assignment.Data;
using ronwell_assignment.Models;

namespace ronwell_assignment.Components.Account
{
    internal sealed class IdentityUserAccessor(UserManager<EmployerUserApplication> userManager, IdentityRedirectManager redirectManager)
    {
        public async Task<EmployerUserApplication> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }
    }
}
