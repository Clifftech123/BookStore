using System.Text.Json.Serialization;

namespace BookStore.Domain.DTO;

public record BookDTO
{
    [JsonIgnore]
    public bool IsUpdateOperation { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
  
}