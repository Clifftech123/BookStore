namespace BookStore.Domain.DTO;

public class UpdateBookDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int AuthorId { get; set; }
    public string Author { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }
}