using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Configurations;

public class CategoryConfiguration: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> entity)
    {
        entity.HasKey(c => c.Id);
        entity.Property(c => c.Name).IsRequired();
        entity.HasData(
            new Category
            {
                Id = 1,
                Name = "Poetry",
                Description = "Books on poetry and verse"
            },
            new Category
            {
                Id = 2,
                Name = "spiritual",
                Description = "Books on spiritual and religious topics"
            },
            new Category
            {
                Id = 3,
                Name = "Science",
                Description = "Books on science and technology"
            }
        );
    }
}
