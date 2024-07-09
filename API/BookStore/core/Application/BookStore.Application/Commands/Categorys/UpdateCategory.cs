using AutoMapper;
using BookStore.Domain.DTO;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Interfaces;
using MediatR;

namespace BookStore.Application.Commands.Categorys;

public class UpdateCategory : IRequest<CategoryDTO>
{
    public int Id { get; set; }
    public UpdateCategoryDTO Category { get; set; }
}

public class UpdateCategoryHandler : IRequestHandler<UpdateCategory, CategoryDTO>
{
    private readonly IBaseRepository<Category> _repository;
    private readonly IMapper _mapper;

    // Constructor injection for repository and mapper
    public UpdateCategoryHandler(IBaseRepository<Category> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the update of an existing category.
    /// </summary>
    /// <param name="request">The request containing the category to be updated.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated category DTO.</returns>
    public async Task<CategoryDTO> Handle(UpdateCategory request, CancellationToken cancellationToken)
    {
        // Retrieve the existing category from the database
        var existingCategory = await _repository.GetByIdAsync(request.Id);
        if (existingCategory == null)
        {
            throw new KeyNotFoundException($"Category with ID {request.Id} not found.");
        }

        // Map the updated values onto the existing category entity
        var categoryToUpdate = _mapper.Map(request.Category, existingCategory);

        // Update the category in the database
        await _repository.UpdateAsync(categoryToUpdate);

        // Return the updated category as a DTO
        return _mapper.Map<CategoryDTO>(categoryToUpdate);
    }
}