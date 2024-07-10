namespace BookStore.Domain.DTO;

public record CategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description {get; set; }
}