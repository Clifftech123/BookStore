using AutoMapper;
using BookStore.Domain.DTO;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Categorys;

public class CreateCategory : IRequest<CategoryDTO>
{
    public CreateCategoryDTO Category { get; set; }
}

public class CreateCategoryHandler : IRequestHandler<CreateCategory, CategoryDTO>
{
    private readonly IBaseRepository<Category> _repository;
    private readonly IMapper _mapper;

    // Constructor injection for repository and mapper
    public CreateCategoryHandler(IBaseRepository<Category> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the creation of a new category.
    /// </summary>
    /// <param name="request">The category creation request containing the DTO.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation, containing the created CategoryDTO.</returns>
    public async Task<CategoryDTO> Handle(CreateCategory request, CancellationToken cancellationToken)
    {
        // Maps the CreateCategoryDTO to a Category entity
        var category = _mapper.Map<Category>(request.Category);
        
        // Adds the new category to the repository and saves it to the database
        await _repository.AddAsync(category);
        
        // Maps the newly created Category entity back to a CategoryDTO to return
        return _mapper.Map<CategoryDTO>(category);
    }
};