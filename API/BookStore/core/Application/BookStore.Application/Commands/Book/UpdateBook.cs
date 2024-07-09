using AutoMapper;
using BookStore.Domain.DTO;
using BookStore.Infrastructure.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Book;

public class UpdateBook : IRequest<BookDTO>
{
    public required BookDTO Book { get; set; }



    public class UpdateBookHandler : IRequestHandler<UpdateBook, BookDTO>
    {
        private readonly IBaseRepository<Domain.Entities.Book> _repository;
        private readonly IMapper _mapper;

        public UpdateBookHandler(IBaseRepository<Domain.Entities.Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BookDTO> Handle(UpdateBook request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Domain.Entities.Book>(request.Book);
            await _repository.UpdateAsync(book);
            return request.Book;
        }
    }
}