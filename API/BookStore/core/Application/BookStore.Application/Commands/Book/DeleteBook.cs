using BookStore.Application.Exceptions;
using BookStore.Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookStore.Application.Commands.Book;

public class DeleteBook : IRequest<Unit>
{
    public int BookId { get; set; }

    public class DeleteBookHandler : IRequestHandler<DeleteBook, Unit>
    {
        private readonly IBaseRepository<Domain.Entities.Book> _repository;
        private readonly ILogger<DeleteBookHandler> _logger;

        public DeleteBookHandler(IBaseRepository<Domain.Entities.Book> repository, ILogger<DeleteBookHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Handles the deletion of a book.
        /// </summary>
        /// <param name="request">The request containing the ID of the book to be deleted.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<Unit> Handle(DeleteBook request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByIdAsync(request.BookId);
            if (book == null)
            {
                _logger.LogWarning($"Book with ID {request.BookId} not found.");
                throw new BookNotFoundException(request.BookId);
            }

            await _repository.DeleteAsync(book);
            _logger.LogInformation($"Book with ID {request.BookId} deleted successfully.");

            return Unit.Value;
        }
    }
}