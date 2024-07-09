using AutoMapper;
using BookStore.Application.Commands.Categorys;
using BookStore.Domain.DTO;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Interfaces;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BookStore.Tests.ApplicationLayerTest;

public class CreateCategoryHandlerTests
{
    [Fact]
    public async Task Handle_GivenValidCategory_CreatesCategorySuccessfully()
    {
        // Arrange
        var expectedCategory = new Category { /* Initialize properties as needed */ };
        var mockRepository = new Mock<IBaseRepository<Category>>();
        var mockMapper = new Mock<IMapper>();
        var categoryDTO = new CategoryDTO
        {
            // Fill with expected data
        };
        var createCategoryDTO = new CreateCategoryDTO
        {
            // Fill with input data
        };
        var category = new Category
        {
            // Fill with data that should be saved
        };

        mockMapper.Setup(m => m.Map<Category>(It.IsAny<CreateCategoryDTO>())).Returns(category);
        mockMapper.Setup(m => m.Map<CategoryDTO>(It.IsAny<Category>())).Returns(categoryDTO);
        expectedCategory = new Category { /* Initialize properties as needed */ };
        mockRepository.Setup(r => r.AddAsync(It.IsAny<Category>())).ReturnsAsync(expectedCategory);

        var handler = new CreateCategoryHandler(mockRepository.Object, mockMapper.Object);

        // Act
        var result = await handler.Handle(new CreateCategory { Category = createCategoryDTO }, CancellationToken.None);

        // Assert
        Xunit.Assert.Equal(categoryDTO, result);
        mockRepository.Verify(r => r.AddAsync(It.IsAny<Category>()), Times.Once);
        mockMapper.Verify(m => m.Map<CategoryDTO>(It.IsAny<Category>()), Times.Once);
    }
}