using BookStore.Application.Commands.Categorys;
using BookStore.Application.Queries.Categorys;
using BookStore.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Controllers;

 [Authorize]

public class CategoryController : BaseApiController
{
    /// <summary>
    /// Handles the uniform response for all action methods in this controller.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="result">The result to be handled.</param>
    /// <returns>An IActionResult based on the result's value.</returns>
    private IActionResult HandleResponse<T>(T result)
    {
        if (result == null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="model">The category data transfer object containing the new category's details.</param>
    /// <returns>A response indicating the result of the create operation.</returns>
    [HttpPost("categories")]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryDTO model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await Mediator.Send(new CreateCategory { Category = model });
        return HandleResponse(result);
    }

    /// <summary>
    /// Retrieves all categories.
    /// </summary>
    /// <returns>A response containing all categories.</returns>
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategoriesAsync()
    {
        var result = await Mediator.Send(new GetAllCategories());
        return HandleResponse(result);
    }

    /// <summary>
    /// Retrieves a specific category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to retrieve.</param>
    /// <returns>A response containing the requested category.</returns>
    [HttpGet("categories/{id:int}")]
    public async Task<IActionResult> GetCategoryByIdAsync(int id)
    {
        var result = await Mediator.Send(new GetCategoryById() { CategoryId = id });
        return HandleResponse(result);
    }

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="id">The ID of the category to update.</param>
    /// <param name="model">The category data transfer object containing the updated details.</param>
    /// <returns>A response indicating the result of the update operation.</returns>
    [HttpPut("categories/{id:int}")]
    public async Task<IActionResult> UpdateCategoryAsync([FromRoute] int id, [FromBody] UpdateCategoryDTO model)
    {
        var result = await Mediator.Send(new UpdateCategory { Id = id, Category = model });
        return HandleResponse(result);
    }

    /// <summary>
    /// Deletes a category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to delete.</param>
    /// <returns>A response indicating the result of the delete operation.</returns>
    [HttpDelete("categories/{id:int}")]
    public async Task<IActionResult> DeleteCategoryAsync([FromRoute] int id)
    {
        var result = await Mediator.Send(new DeleteCategory { CategoryId = id });
        return HandleResponse(result);
    }
}