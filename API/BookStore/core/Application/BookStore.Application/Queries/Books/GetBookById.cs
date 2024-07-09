using AutoMapper;
using BookStore.Application.Exceptions;
using BookStore.Domain.DTO;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Interfaces;
using MediatR;

namespace BookStore.Application.Queries.Books;
public class GetBookById : IRequest<BookDTO>
{
    public int BookId { get; set; }

    public class GetBookByIdHandler : IRequestHandler<GetBookById, BookDTO>
    {
        private readonly IBaseRepository<Book> _repository;
        private readonly IMapper _mapper;

        public GetBookByIdHandler(IBaseRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the request to get a book by its ID.
        /// </summary>
        /// <param name="request">The request containing the ID of the book to retrieve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation, containing the BookDTO of the requested book.</returns>
        public async Task<BookDTO> Handle(GetBookById request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByIdAsync(request.BookId);
            if (book == null)
            {
                throw new BookNotFoundException(request.BookId);
            }

            // Maps the Book entity to a BookDTO
            var bookDTO = _mapper.Map<BookDTO>(book);
            return bookDTO;
        }
    }
}