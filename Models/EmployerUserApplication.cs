using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ronwell_assignment.Models;

public class EmployerUserApplication : IdentityUser
{
    [Required]
    public string Position { get; set; } = default!;
    [Required]
    public double Salary { get; set; }
}
