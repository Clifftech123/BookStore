using AutoMapper;
using BookStore.Domain.DTO;
using BookStore.Domain.Entities;

namespace BookStore.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookDTO>().ReverseMap();
        CreateMap<CreateBookDTO, Book>();
        CreateMap<UpdateBookDTO, Book>();
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<CreateCategoryDTO, Category>();
        CreateMap<UpdateCategoryDTO, Category>();

        
    }
}
