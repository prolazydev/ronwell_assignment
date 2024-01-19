namespace Assignment.DTO;

public class UserDTO
{
    public string Id { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Position { get; set; } = default!;
    public double Salary { get; set; }
    public List<string> Roles { get; set; } = [];
}
