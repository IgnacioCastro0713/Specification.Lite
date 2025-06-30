using System.Linq.Expressions;
using Specification.Lite;
using Specification.Lite.Common;
using Specification.Lite.Exceptions;

namespace Specification.Test;

public class SpecificationTests
{
    #region Test Specifications
    // Concrete implementation of Specification<TestEntity> for testing
    public class TestEntitySpecification : Specification<TestEntity>
    {
        public TestEntitySpecification() { }

        // Expose protected methods for testing
        public void PublicAddWhere(Expression<Func<TestEntity, bool>> criteriaExpression) => Where(criteriaExpression);

        public IncludeExpression<TestEntity> PublicAddInclude(Expression<Func<TestEntity, object>> includeExpression) =>
            Include(includeExpression);

        public void PublicApplyDistinct() => Distinct();

        public void PublicApplyDistinctBy<TKey>(Expression<Func<TestEntity, TKey>> keySelector) =>
            DistinctBy(keySelector);

        public void PublicAddOrderBy<TKey>(Expression<Func<TestEntity, TKey>> orderByExpression) =>
            OrderBy(orderByExpression);

        public void PublicAddOrderByDescending<TKey>(Expression<Func<TestEntity, TKey>> orderByDescendingExpression) =>
            OrderByDescending(orderByDescendingExpression);

        public void PublicAddThenBy<TKey>(Expression<Func<TestEntity, TKey>> thenByExpression) =>
            AddThenBy(thenByExpression);

        public void PublicAddThenByDescending<TKey>(Expression<Func<TestEntity, TKey>> thenByDescendingExpression) =>
            AddThenByDescending(thenByDescendingExpression);

        public void PublicAsTracking() => AsTracking();

        public void PublicAsNoTracking() => AsNoTracking();

        public void PublicSplitQuery() => SplitQuery();
    }

    // Concrete implementation of Specification<TestEntity, TestDto> for testing
    public class TestEntityDtoSpecification : Specification<TestEntity, TestDto>
    {
        public TestEntityDtoSpecification() { }

        // Expose protected methods for testing
        public void PublicApplySelect(Expression<Func<TestEntity, TestDto>> selector) => Select(selector);

        public void PublicApplySelectMany(Expression<Func<TestEntity, IEnumerable<TestDto>>> selector) =>
            SelectMany(selector);
    }
    #endregion

    #region Specification<TEntity> Tests
    [Fact]
    public void AddWhere_AddsExpressionToList()
    {
        // Arrange
        var spec = new TestEntitySpecification();
        Expression<Func<TestEntity, bool>> expression = e => e.Id > 0;

        // Act
        spec.PublicAddWhere(expression);

        // Assert
        Assert.Single(spec.CriteriaExpressions);
        Assert.Equal(expression, spec.CriteriaExpressions[0]);
    }

    [Fact]
    public void AddInclude_AddsIncludePathToList()
    {
        // Arrange
        var spec = new TestEntitySpecification();
        Expression<Func<TestEntity, object>> expression = e => e.Name;

        // Act
        IncludeExpression<TestEntity> result = spec.PublicAddInclude(expression);

        // Assert
        Assert.Single(spec.IncludePaths);
        Assert.Equal(result, spec.IncludePaths[0]);
        Assert.NotNull(result.Expression);
    }

    [Fact]
    public void ApplyDistinct_SetsIsDistinct()
    {
        // Arrange
        var spec = new TestEntitySpecification();

        // Act
        spec.PublicApplyDistinct();

        // Assert
        Assert.True(spec.IsDistinct);
    }

    [Fact]
    public void ApplyDistinct_WithDistinctByAlreadySet_ThrowsException()
    {
        // Arrange
        var spec = new TestEntitySpecification();
        spec.PublicApplyDistinctBy(e => e.Id);

        // Act & Assert
        Assert.Throws<ConcurrentDistinctException>(() => spec.PublicApplyDistinct());
    }

    [Fact]
    public void ApplyDistinctBy_SetsDistinctBySelector()
    {
        // Arrange
        var spec = new TestEntitySpecification();
        Expression<Func<TestEntity, int>> keySelector = e => e.Id;

        // Act
        spec.PublicApplyDistinctBy(keySelector);

        // Assert
        Assert.NotNull(spec.DistinctBySelector);
    }

    [Fact]
    public void ApplyDistinctBy_WithDistinctAlreadySet_ThrowsException()
    {
        // Arrange
        var spec = new TestEntitySpecification();
        spec.PublicApplyDistinct();

        // Act & Assert
        Assert.Throws<ConcurrentDistinctException>(() => spec.PublicApplyDistinctBy(e => e.Id));
    }

    [Fact]
    public void AddOrderBy_AddsOrderExpressionToList()
    {
        // Arrange
        var spec = new TestEntitySpecification();
        Expression<Func<TestEntity, int>> keySelector = e => e.Id;

        // Act
        spec.PublicAddOrderBy(keySelector);

        // Assert
        Assert.Single(spec.OrderByExpressions);
        Assert.Equal(OrderTypeEnum.OrderBy, spec.OrderByExpressions[0].OrderType);
    }

    [Fact]
    public void AddOrderByDescending_AddsOrderExpressionToList()
    {
        // Arrange
        var spec = new TestEntitySpecification();
        Expression<Func<TestEntity, int>> keySelector = e => e.Id;

        // Act
        spec.PublicAddOrderByDescending(keySelector);

        // Assert
        Assert.Single(spec.OrderByExpressions);
        Assert.Equal(OrderTypeEnum.OrderByDescending, spec.OrderByExpressions[0].OrderType);
    }

    [Fact]
    public void AddThenBy_AddsOrderExpressionToList()
    {
        // Arrange
        var spec = new TestEntitySpecification();
        Expression<Func<TestEntity, int>> keySelector = e => e.Id;

        // Act
        spec.PublicAddThenBy(keySelector);

        // Assert
        Assert.Single(spec.OrderByExpressions);
        Assert.Equal(OrderTypeEnum.ThenBy, spec.OrderByExpressions[0].OrderType);
    }

    [Fact]
    public void AddThenByDescending_AddsOrderExpressionToList()
    {
        // Arrange
        var spec = new TestEntitySpecification();
        Expression<Func<TestEntity, int>> keySelector = e => e.Id;

        // Act
        spec.PublicAddThenByDescending(keySelector);

        // Assert
        Assert.Single(spec.OrderByExpressions);
        Assert.Equal(OrderTypeEnum.ThenByDescending, spec.OrderByExpressions[0].OrderType);
    }

    [Fact]
    public void AsTracking_SetsIsAsTracking()
    {
        // Arrange
        var spec = new TestEntitySpecification();

        // Act
        spec.PublicAsTracking();

        // Assert
        Assert.True(spec.IsAsTracking);
    }

    [Fact]
    public void AsTracking_WithNoTrackingAlreadySet_ThrowsException()
    {
        // Arrange
        var spec = new TestEntitySpecification();
        spec.PublicAsNoTracking();

        // Act & Assert
        Assert.Throws<ConcurrentTrackingException>(() => spec.PublicAsTracking());
    }

    [Fact]
    public void AsNoTracking_SetsIsAsNoTracking()
    {
        // Arrange
        var spec = new TestEntitySpecification();

        // Act
        spec.PublicAsNoTracking();

        // Assert
        Assert.True(spec.IsAsNoTracking);
    }

    [Fact]
    public void AsNoTracking_WithTrackingAlreadySet_ThrowsException()
    {
        // Arrange
        var spec = new TestEntitySpecification();
        spec.PublicAsTracking();

        // Act & Assert
        Assert.Throws<ConcurrentTrackingException>(() => spec.PublicAsNoTracking());
    }

    [Fact]
    public void SplitQuery_SetsIsAsSplitQuery()
    {
        // Arrange
        var spec = new TestEntitySpecification();

        // Act
        spec.PublicSplitQuery();

        // Assert
        Assert.True(spec.IsAsSplitQuery);
    }
    #endregion

    #region Specification<TEntity, TResult> Tests
    [Fact]
    public void ApplySelect_SetsSelector()
    {
        // Arrange
        var spec = new TestEntityDtoSpecification();
        Expression<Func<TestEntity, TestDto>> selector = e => new TestDto { Id = e.Id, Name = e.Name };

        // Act
        spec.PublicApplySelect(selector);

        // Assert
        Assert.Equal(selector, spec.Selector);
    }

    [Fact]
    public void ApplySelect_WithSelectManyAlreadySet_ThrowsException()
    {
        // Arrange
        var spec = new TestEntityDtoSpecification();
        spec.PublicApplySelectMany(e => new[] { new TestDto { Id = e.Id, Name = e.Name } });

        // Act & Assert
        Assert.Throws<ConcurrentSelectorsException>(() =>
            spec.PublicApplySelect(e => new TestDto { Id = e.Id, Name = e.Name }));
    }

    [Fact]
    public void ApplySelectMany_SetsSelectManySelector()
    {
        // Arrange
        var spec = new TestEntityDtoSpecification();
        Expression<Func<TestEntity, IEnumerable<TestDto>>> selector =
            e => new[] { new TestDto { Id = e.Id, Name = e.Name } };

        // Act
        spec.PublicApplySelectMany(selector);

        // Assert
        Assert.Equal(selector, spec.SelectManySelector);
    }

    [Fact]
    public void ApplySelectMany_WithSelectAlreadySet_ThrowsException()
    {
        // Arrange
        var spec = new TestEntityDtoSpecification();
        spec.PublicApplySelect(e => new TestDto { Id = e.Id, Name = e.Name });

        // Act & Assert
        Assert.Throws<ConcurrentSelectorsException>(() =>
            spec.PublicApplySelectMany(e => new[] { new TestDto { Id = e.Id, Name = e.Name } }));
    }
    #endregion
}