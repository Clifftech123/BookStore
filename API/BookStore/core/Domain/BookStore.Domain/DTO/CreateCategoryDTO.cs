namespace BookStore.Domain.DTO;

public  record CreateCategoryDTO
{
    
    public string Name { get; set; }
    public string Description
    {
        get; set;
    }
}