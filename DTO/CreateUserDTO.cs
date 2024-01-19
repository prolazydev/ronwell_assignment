namespace Assignment.DTO;

public class CreateUserDTO
{
    public string Id { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Position { get; set; } = default!;
    public double Salary { get; set; }
    public string[] Roles { get; set; } = [];
}
