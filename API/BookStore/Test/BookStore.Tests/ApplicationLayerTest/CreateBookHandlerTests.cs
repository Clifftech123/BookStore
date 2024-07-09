using AutoMapper;
using BookStore.Application.Commands.Book;
using BookStore.Domain.DTO;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Interfaces;
using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Tests.ApplicationLayerTest;

public class CreateBookHandlerTests
{
    [Fact]
    public async Task Handle_GivenValidBook_CreatesBookSuccessfully()
    {
        // Arrange
        var mockRepository = new Mock<IBaseRepository<Book>>();
        var mockMapper = new Mock<IMapper>();
        var bookDTO = new BookDTO { /* Fill with expected data */ };
        var createBookDTO = new CreateBookDTO { /* Fill with input data */ };
        var book = new Book { /* Fill with data that should be saved */ };

        mockMapper.Setup(m => m.Map<Book>(It.IsAny<CreateBookDTO>())).Returns(book);
        mockMapper.Setup(m => m.Map<BookDTO>(It.IsAny<Book>())).Returns(bookDTO);
        mockRepository.Setup(r => r.AddAsync(It.IsAny<Book>())).Returns((Task<Book>)Task.CompletedTask);

        var handler = new CreateBook.CreateBookHandler(mockRepository.Object, mockMapper.Object);

        // Act
        var result = await handler.Handle(new CreateBook { Book = createBookDTO }, CancellationToken.None);

        // Assert
        Xunit.Assert.Equal(bookDTO, result);
        mockRepository.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Once);
        mockMapper.Verify(m => m.Map<BookDTO>(It.IsAny<Book>()), Times.Once);
    }
}