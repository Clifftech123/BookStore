using AutoMapper;
using BookStore.Application.Exceptions;
using BookStore.Domain.DTO;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Interfaces;
using MediatR;
namespace BookStore.Application.Queries.Categorys;


public class GetCategoryById : IRequest<CategoryDTO>
{
    public int CategoryId { get; set; }
}

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, CategoryDTO>
{
    private readonly IBaseRepository<Category> _repository;
    private readonly IMapper _mapper;

    public GetCategoryByIdHandler(IBaseRepository<Category> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Asynchronously handles the request to get a category by its ID.
    /// </summary>
    /// <param name="request">The request containing the ID of the category to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation, containing the CategoryDTO of the requested category.</returns>
    public async Task<CategoryDTO> Handle(GetCategoryById request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.CategoryId);
        if (category == null)
        {
            throw new CategoryNotFoundException(request.CategoryId);
        }

        // Maps the Category entity to a CategoryDTO
        var categoryDTO = _mapper.Map<CategoryDTO>(category);
        return categoryDTO;
    }
}