namespace BookStore.Domain.DTO;

public record UpdateCategoryDTO
{
    public string name { get; set; }
    public string description { get; set; }
}