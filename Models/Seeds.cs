using System.ComponentModel.DataAnnotations;

namespace Assignment.Models;

public class Seeds
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public bool IsSeedDataApplied { get; set; } = false;
}
