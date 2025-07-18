﻿using API;
using Moq;
using Specification.Lite;
using Specification.Lite.Evaluators;

namespace Specification.Test.Evaluators;

public class AsNoTrackingEvaluatorTests
{
    [Fact]
    public void Evaluate_AppliesAsNoTracking_WhenSpecificationRequestsIt()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity> { new TestEntity { Id = 1 } }.AsQueryable();
        var mockSpec = new Mock<ISpecification<TestEntity>>();
        mockSpec.Setup(s => s.AsNoTracking).Returns(true);

        AsNoTrackingEvaluator evaluator = AsNoTrackingEvaluator.Instance;

        // Act
        IQueryable<TestEntity> result = evaluator.Query(data, mockSpec.Object);

        // Assert
        Assert.IsAssignableFrom<IQueryable<TestEntity>>(result);
    }

    [Fact]
    public void Evaluate_DoesNotApplyAsNoTracking_WhenSpecificationDoesNotRequestIt()
    {
        // Arrange
        IQueryable<TestEntity> data = new List<TestEntity> { new TestEntity { Id = 1 } }.AsQueryable();
        var mockSpec = new Mock<ISpecification<TestEntity>>();
        mockSpec.Setup(s => s.AsNoTracking).Returns(false);

        AsNoTrackingEvaluator evaluator = AsNoTrackingEvaluator.Instance;

        // Act
        IQueryable<TestEntity> result = evaluator.Query(data, mockSpec.Object);

        // Assert
        Assert.IsAssignableFrom<IQueryable<TestEntity>>(result);
    }
}
