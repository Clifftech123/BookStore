using BookStore.Application.Commands.Book;
using BookStore.Application.Queries.Books;
using BookStore.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Controllers;

[   Authorize]
public class BookController : BaseApiController
{
    /// <summary>
    /// Creates a new book.
    /// </summary>
    /// <param name="model">The book data transfer object containing the new book's details.</param>
    /// <returns>A response indicating the result of the create operation.</returns>
    [HttpPost("books")]
    public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookDTO model)
    {
        return Ok(await Mediator.Send(new CreateBook { Book = model }));
    }

    /// <summary>
    /// Retrieves all books.
    /// </summary>
    /// <returns>A response containing a list of all books.</returns>
    [HttpGet("books")]
    public async Task<IActionResult> GetBooksAsync()
    {
        return Ok(await Mediator.Send(new GetAllBook()));
    }

    /// <summary>
    /// Retrieves a book by its ID.
    /// </summary>
    /// <param name="id">The ID of the book to retrieve.</param>
    /// <returns>A response containing the requested book, or NotFound if it does not exist.</returns>
    [HttpGet("books/{id:int}")]
    public async Task<IActionResult> GetBookByIdAsync(int id)
    {
        var result = await Mediator.Send(new GetBookById { BookId = id });
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    /// <summary>
    /// Updates a book.
    /// </summary>
    /// <param name="model">The book data transfer object containing the updated book's details.</param>
    /// <returns>A response indicating the result of the update operation.</returns>
    [HttpPut("books")]
    public async Task<IActionResult> UpdateBookAsync([FromBody] BookDTO model)
    {
        return Ok(await Mediator.Send(new UpdateBook { Book = model }));
    }

    /// <summary>
    /// Deletes a book by its ID.
    /// </summary>
    /// <param name="id">The ID of the book to delete.</param>
    /// <returns>A response indicating the result of the delete operation.</returns>
    [HttpDelete("books/{id:int}")]
    public async Task<IActionResult> DeleteBookAsync([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new DeleteBook { BookId = id }));
    }
}