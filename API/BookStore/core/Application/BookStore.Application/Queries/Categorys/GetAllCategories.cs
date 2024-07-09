using AutoMapper;
using BookStore.Domain.DTO;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Interfaces;
using MediatR;

namespace BookStore.Application.Queries.Categorys;

public class GetAllCategories : IRequest<List<CategoryDTO>> { }

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategories, List<CategoryDTO>>
{
    private readonly IBaseRepository<Category> _repository;
    private readonly IMapper _mapper;
    
    public GetAllCategoriesHandler(IBaseRepository<Category> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Asynchronously handles the request to get all categories.
    /// </summary>
    /// <param name="request">The request object.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation, containing a list of CategoryDTOs.</returns>
    public async Task<List<CategoryDTO>> Handle(GetAllCategories request, CancellationToken cancellationToken)
    {
        var getAllCategories = await _repository.GetAllAsync();
        var results = _mapper.Map<List<CategoryDTO>>(getAllCategories);
        return results;
    }
}