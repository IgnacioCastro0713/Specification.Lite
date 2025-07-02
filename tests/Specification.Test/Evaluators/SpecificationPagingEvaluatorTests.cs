using API;
using Moq;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class SpecificationPagingEvaluatorTests
{
    [Fact]
    public void ApplyPaging_WithNoSkipOrTake_ReturnsOriginalQuery()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity>>();
        mockSpecification.Setup(s => s.Skip).Returns(0);
        mockSpecification.Setup(s => s.Take).Returns(-1);

        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test1" },
            new() { Id = 2, Name = "Test2" },
            new() { Id = 3, Name = "Test3" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act
        var result = query.Paging(mockSpecification.Object).ToList();

        // Assert
        Assert.Equal(entities.Count, result.Count);
    }

    [Fact]
    public void ApplyPaging_WithSkip_SkipsSpecifiedNumberOfItems()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity>>();
        mockSpecification.Setup(s => s.Skip).Returns(1);
        mockSpecification.Setup(s => s.Take).Returns(-1);

        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test1" },
            new() { Id = 2, Name = "Test2" },
            new() { Id = 3, Name = "Test3" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act
        var result = query.Paging(mockSpecification.Object).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(2, result[0].Id);
        Assert.Equal(3, result[1].Id);
    }

    [Fact]
    public void ApplyPaging_WithTake_TakesSpecifiedNumberOfItems()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity>>();
        mockSpecification.Setup(s => s.Skip).Returns(0);
        mockSpecification.Setup(s => s.Take).Returns(2);

        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test1" },
            new() { Id = 2, Name = "Test2" },
            new() { Id = 3, Name = "Test3" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act
        var result = query.Paging(mockSpecification.Object).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].Id);
        Assert.Equal(2, result[1].Id);
    }

    [Fact]
    public void ApplyPaging_WithSkipAndTake_AppliesBothCorrectly()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity>>();
        mockSpecification.Setup(s => s.Skip).Returns(1);
        mockSpecification.Setup(s => s.Take).Returns(1);

        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test1" },
            new() { Id = 2, Name = "Test2" },
            new() { Id = 3, Name = "Test3" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act
        var result = query.Paging(mockSpecification.Object).ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal(2, result[0].Id);
    }
}
