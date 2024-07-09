using AutoMapper;
using BookStore.Domain.DTO;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Interfaces;
using MediatR;

namespace BookStore.Application.Queries.Books;

public class GetAllBook : IRequest<List<BookDTO>>
{
    public class GetBooksHandler : IRequestHandler<GetAllBook, List<BookDTO>>
    {
        private readonly IBaseRepository<Book> _repository;
        private readonly IMapper _mapper;

        public GetBooksHandler(IBaseRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the request to get all books.
        /// </summary>
        /// <param name="request">The request object (not used in this method, but required by the interface).</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation, containing a list of BookDTOs.</returns>
        public async Task<List<BookDTO>> Handle(GetAllBook request, CancellationToken cancellationToken)
        {
            try
            {
                var getAllBooks = await _repository.GetAllAsync();
                // Maps the list of Book entities to a list of BookDTOs
                var results = _mapper.Map<List<BookDTO>>(getAllBooks);
                return results;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving books", ex);
            }
        }
    }
};