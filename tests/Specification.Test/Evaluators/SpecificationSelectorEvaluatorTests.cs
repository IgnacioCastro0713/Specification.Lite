using System.Linq.Expressions;
using API;
using Moq;
using Specification.Lite.Evaluators;
using Specification.Lite.Exceptions;

namespace Specification.Test.Evaluators;

public class SpecificationSelectorEvaluatorTests
{
    [Fact]
    public void ApplySelectors_WithSelectSelector_AppliesSelector()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity, TestDto>>();
        Expression<Func<TestEntity, TestDto>> selector = entity => new TestDto { Id = entity.Id, Name = entity.Name };
        mockSpecification.Setup(s => s.Selector).Returns(selector);
        mockSpecification.Setup(s => s.ManySelector).Returns((Expression<Func<TestEntity, IEnumerable<TestDto>>>?)null);

        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test1" },
            new() { Id = 2, Name = "Test2" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act
        var result = query.Selectors(mockSpecification.Object).ToList();

        // Assert
        Assert.Equal(entities.Count, result.Count);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Test1", result[0].Name);
        Assert.Equal(2, result[1].Id);
        Assert.Equal("Test2", result[1].Name);
    }

    [Fact]
    public void ApplySelectors_WithSelectManySelector_AppliesSelectMany()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity, string>>();
        mockSpecification.Setup(s => s.Selector).Returns((Expression<Func<TestEntity, string>>?)null);
        Expression<Func<TestEntity, IEnumerable<string>>> selectManySelector = entity => entity.Name.Split(new[] { ',' })
            .Select(n => n.Trim());
        mockSpecification.Setup(s => s.ManySelector).Returns(selectManySelector);

        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test1, Test2" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act
        var result = query.Selectors(mockSpecification.Object).ToList();

        // Assert
        Assert.Equal(2, result.Count); // Because we split one entity into two strings
        Assert.Equal("Test1", result[0]);
        Assert.Equal("Test2", result[1]);
    }

    [Fact]
    public void ApplySelectors_WithNoSelectors_ThrowsException()
    {
        // Arrange
        var mockSpecification = new Mock<Lite.ISpecification<TestEntity, TestDto>>();
        mockSpecification.Setup(s => s.Selector).Returns((Expression<Func<TestEntity, TestDto>>?)null);
        mockSpecification.Setup(s => s.ManySelector).Returns((Expression<Func<TestEntity, IEnumerable<TestDto>>>?)null);

        var entities = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test1" }
        };
        IQueryable<TestEntity> query = entities.AsQueryable();

        // Act & Assert
        Assert.Throws<DuplicateSelectorsException>(() => query.Selectors(mockSpecification.Object));
    }
}
