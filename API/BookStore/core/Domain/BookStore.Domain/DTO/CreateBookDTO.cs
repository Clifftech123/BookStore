namespace BookStore.Domain.DTO;

public record CreateBookDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
}