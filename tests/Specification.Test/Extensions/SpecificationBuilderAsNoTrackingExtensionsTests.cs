﻿using API;
using Specification.Lite;

namespace Specification.Test.Extensions;

public class SpecificationBuilderAsNoTrackingExtensionsTests
{
    [Fact]
    public void AsNoTracking_ShouldApplyAsNoTracking_WhenEnabled()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, TestDto>();

        // Act
        specification.Query.AsNoTracking();
        specificationResult.Query.AsNoTracking();

        // Assert
        Assert.True(specification.AsNoTracking);
        Assert.True(specificationResult.AsNoTracking);
    }

    [Fact]
    public void AsNoTracking_ShouldNotApplyAsNoTracking_WhenDisabled()
    {
        // Arrange
        var specification = new Specification<TestEntity>();
        var specificationResult = new Specification<TestEntity, TestDto>();

        // Act & Assert
        Assert.False(specification.AsNoTracking);
        Assert.False(specificationResult.AsNoTracking);
    }

    [Fact]
    public void AsNoTracking_ShouldNotAffectOtherProperties_ForEntityResultSpecification()
    {
        // Arrange
        var specificationResult = new Specification<TestEntity, TestDto>();

        // Act
        specificationResult.Query.AsNoTracking();

        // Assert
        Assert.True(specificationResult.AsNoTracking);
        Assert.False(specificationResult.AsTracking);
        Assert.False(specificationResult.AsNoTrackingWithIdentityResolution);
    }
}
