using AutoMapper;
using BookStore.Domain.DTO;
using BookStore.Infrastructure.Interfaces;
using MediatR;

namespace BookStore.Application.Commands.Book;

public class CreateBook : IRequest<BookDTO>

{
    public CreateBookDTO? Book { get; set; }


    public class CreateBookHandler : IRequestHandler<CreateBook, BookDTO>
    {
        
        private readonly IBaseRepository<Domain.Entities.Book> _repository;
        private readonly IMapper _mapper;

        public CreateBookHandler(IBaseRepository<Domain.Entities.Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BookDTO> Handle(CreateBook request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Domain.Entities.Book>(request.Book);
            await _repository.AddAsync(book);
            return _mapper.Map<BookDTO>(book);

        }
    }
}