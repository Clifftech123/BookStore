using AutoMapper;
using BookStore.Domain.DTO;
using BookStore.Infrastructure.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Book;

public class SearchBooksByName : IRequest<List<BookDTO>>
{
    public string SearchTerm { get; set; }
}

public class SearchBooksByNameHandler : IRequestHandler<SearchBooksByName, List<BookDTO>>
{
    private readonly IBaseRepository<Domain.Entities.Book> _repository;
    private readonly IMapper _mapper;

    public SearchBooksByNameHandler(IBaseRepository<Domain.Entities.Book> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<BookDTO>> Handle(SearchBooksByName request, CancellationToken cancellationToken)
    {
        var books = await _repository.SearchBooksAsync(request.SearchTerm);
        return _mapper.Map<List<BookDTO>>(books);
    }
}