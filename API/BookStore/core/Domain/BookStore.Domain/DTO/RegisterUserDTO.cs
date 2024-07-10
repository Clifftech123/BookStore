namespace BookStore.Domain.DTO;

public  record RegisterUserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
