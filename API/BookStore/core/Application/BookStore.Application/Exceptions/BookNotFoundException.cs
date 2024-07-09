namespace BookStore.Application.Exceptions;

public class BookNotFoundException: NotFoundException
{
    public BookNotFoundException(int bookId) : base($"Book with id: {bookId} not found.")
    {
    } 
}