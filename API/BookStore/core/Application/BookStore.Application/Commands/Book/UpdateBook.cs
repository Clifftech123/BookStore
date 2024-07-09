using AutoMapper;
using BookStore.Domain.DTO;
using BookStore.Infrastructure.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Book;

public class UpdateBook : IRequest<BookDTO>
{
    public required  UpdateBookDTO Book { get; set; }
    
    public class UpdateBookHandler : IRequestHandler<UpdateBook, BookDTO>
    {
        private readonly IBaseRepository<Domain.Entities.Book> _repository;
        private readonly IMapper _mapper;

        public UpdateBookHandler(IBaseRepository<Domain.Entities.Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the update of an existing book.
        /// </summary>
        /// <param name="request">The request containing the book to be updated.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated book DTO.</returns>
        public async Task<BookDTO> Handle(UpdateBook request, CancellationToken cancellationToken)
        {
            var existingBook = await _repository.GetByIdAsync(request.Book.Id);
            if (existingBook == null)
            {
                throw new KeyNotFoundException($"Book with ID {request.Book.Id} not found.");
            }

            var bookToUpdate = _mapper.Map(request.Book, existingBook);
            await _repository.UpdateAsync(bookToUpdate);
            return _mapper.Map<BookDTO>(bookToUpdate);
        }
    }
}