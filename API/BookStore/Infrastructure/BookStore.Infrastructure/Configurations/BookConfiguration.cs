using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Configurations;

public class BookConfiguration:IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> entity)
    {
        entity.HasKey(b => b.Id);
        entity.Property(b => b.Name).IsRequired();
        entity.Property(b => b.Price).IsRequired();
        entity.Property(b => b.Description).IsRequired();
        entity.Property(b => b.CreatedDate).IsRequired();
        entity.Property(b => b.UpdatedDate).IsRequired();
        entity.HasOne(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId);
        entity.HasData(
            new Book
            {
                Id = 1,
                Name = "The Complete Works of William Shakespeare",
                Price = 15.99,
                Description = "Books on poetry and verse",
                CategoryId = 1,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            },
            new Book
            {
                Id = 2,
                Name = "The Holy Bible",
                Price = 10.99,
                Description = "Books on spiritual and religious topics",
                CategoryId = 2,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            },
            new Book
            {
                Id = 3,
                Name = "The Origin of Species",
                Price = 20.99,
                Description = "Books on science and technology",
                CategoryId = 3,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            }
        );
    }
    
}