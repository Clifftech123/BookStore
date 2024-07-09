using AutoMapper;
using BookStore.Domain.DTO;
using BookStore.Domain.Entities;

namespace BookStore.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Map from Book entity to BookDTO and back
        CreateMap<Book, BookDTO>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ReverseMap();
        CreateMap<CreateBookDTO, Book>();
        CreateMap<UpdateBookDTO, Book>();
        
        // Category
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<CreateCategoryDTO, Category>();
        CreateMap<UpdateCategoryDTO, Category>();

        
    }
}
