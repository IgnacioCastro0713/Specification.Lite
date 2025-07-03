using API;
using Specification.Lite;
using Specification.Lite.Evaluators;
using Specification.Lite.Exceptions;

namespace Specification.Test.Evaluators;

public class SpecificationSelectorEvaluatorTests
{
    [Fact]
    public void Selectors_ShouldApplySelector()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto>
        {
            Selector = e => new TestDto { Id = e.Id, Name = e.Name }
        };

        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" }
        }.AsQueryable();

        // Act
        IQueryable<TestDto> result = query.Selectors(specification);

        // Assert
        var projectedResult = result.ToList();
        Assert.Equal(2, projectedResult.Count);
        Assert.Equal("Entity1", projectedResult[0].Name);
        Assert.Equal("Entity2", projectedResult[1].Name);
    }

    [Fact]
    public void Selectors_ShouldApplyManySelector()
    {
        // Arrange
        var specification = new Specification<TestNestedEntity, TestDto>();
        specification.Query.SelectMany(e => e.Deeps.Select(r => new TestDto { Id = r.Id, Name = r.Value }));

        IQueryable<TestNestedEntity> query = new List<TestNestedEntity>
        {
            new()
            {
                Id = 1,
                Value = "Entity1",
                Deeps =
                [
                    new TestDeepEntity { Id = 101, Value = "Related1" },
                    new TestDeepEntity { Id = 102, Value = "Related2" }
                ]
            },
            new()
            {
                Id = 2,
                Value = "Entity2",
                Deeps = [new TestDeepEntity { Id = 201, Value = "Related3" }]
            }
        }.AsQueryable();

        // Act
        IQueryable<TestDto> result = query.Selectors(specification);

        // Assert
        var projectedResult = result.ToList();
        Assert.Equal(3, projectedResult.Count);
        Assert.Contains(projectedResult, dto => dto.Name == "Related1");
        Assert.Contains(projectedResult, dto => dto.Name == "Related2");
        Assert.Contains(projectedResult, dto => dto.Name == "Related3");
    }

    [Fact]
    public void Selectors_ShouldThrowSelectorNotFoundException_WhenNoSelectorIsDefined()
    {
        // Arrange
        var specification = new Specification<TestEntity, TestDto>();

        IQueryable<TestEntity> query = new List<TestEntity>
        {
            new() { Id = 1, Name = "Entity1" },
            new() { Id = 2, Name = "Entity2" }
        }.AsQueryable();

        // Act & Assert
        Assert.Throws<SelectorNotFoundException>(() => query.Selectors(specification));
    }
}
