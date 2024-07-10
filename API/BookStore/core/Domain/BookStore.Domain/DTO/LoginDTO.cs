namespace BookStore.Domain.DTO;

public  record LoginDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
}