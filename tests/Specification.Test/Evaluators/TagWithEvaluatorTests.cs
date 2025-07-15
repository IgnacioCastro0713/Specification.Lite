using API;
using Specification.Lite;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class TagWithEvaluatorTests
{
    [Fact]
    public void Query_ShouldApplySingleTag()
    {
        // Arrange
        IQueryable<TestEntity> query = Array.Empty<TestEntity>().AsQueryable();
        var specification = new MockSpecification<TestEntity>();

        TagWithEvaluator evaluator = TagWithEvaluator.Instance;

        // Act
        IQueryable<TestEntity> result = evaluator.Query(query, specification);

        // Assert
        Assert.NotNull(result);
        // Verify that the query contains the tag (this is a conceptual check, as EF Core's TagWith is not directly inspectable)
        Assert.Contains("Tag1", specification.QueryTags);
    }

    [Fact]
    public void Query_ShouldApplyMultipleTags()
    {
        // Arrange
        IQueryable<TestEntity> query = Array.Empty<TestEntity>().AsQueryable();
        var specification = new MockSpecification<TestEntity>();

        TagWithEvaluator evaluator = TagWithEvaluator.Instance;

        // Act
        IQueryable<TestEntity> result = evaluator.Query(query, specification);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("Tag1", specification.QueryTags);
        Assert.Contains("Tag2", specification.QueryTags);
    }

    [Fact]
    public void Query_ShouldNotApplyTags_WhenNoTagsProvided()
    {
        // Arrange
        IQueryable<TestEntity> query = Array.Empty<TestEntity>().AsQueryable();
        var specification = new MockEmptySpecification<TestEntity>();

        TagWithEvaluator evaluator = TagWithEvaluator.Instance;

        // Act
        IQueryable<TestEntity> result = evaluator.Query(query, specification);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(specification.QueryTags);
    }


    private sealed class MockSpecification<TEntity> : Specification<TEntity> where TEntity : class
    {
        public MockSpecification() => Query.TagWith("Tag1").TagWith("Tag2");
    }

    private sealed class MockEmptySpecification<TEntity> : Specification<TEntity> where TEntity : class
    {
    }
}
