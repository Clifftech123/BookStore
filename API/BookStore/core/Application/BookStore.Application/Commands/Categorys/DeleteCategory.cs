using BookStore.Application.Exceptions;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Categorys;

public class DeleteCategory : IRequest<Unit>
{
    public int CategoryId { get; set; }
}

public class DeleteCategoryHandler : IRequestHandler<DeleteCategory, Unit>
{
    private readonly IBaseRepository<Category> _repository;
    private readonly ILogger<DeleteCategoryHandler> _logger;

    public DeleteCategoryHandler(IBaseRepository<Category> repository, ILogger<DeleteCategoryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the deletion of a category.
    /// </summary>
    /// <param name="request">The request containing the ID of the category to be deleted.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, signaling the completion of the deletion.</returns>
    public async Task<Unit> Handle(DeleteCategory request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.CategoryId);
        if (category == null)
        {
            _logger.LogWarning($"Category with ID {request.CategoryId} not found.");
            throw new CategoryNotFoundException(request.CategoryId);
        }

        await _repository.DeleteAsync(category);
        _logger.LogInformation($"Category with ID {request.CategoryId} deleted successfully.");

        return Unit.Value;
    }
}