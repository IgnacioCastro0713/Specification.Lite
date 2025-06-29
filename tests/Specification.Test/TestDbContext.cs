using Microsoft.EntityFrameworkCore;

namespace Specification.Test;

/// <summary>
/// In-memory database context for testing Entity Framework Core functionality
/// </summary>
public class TestDbContext(DbContextOptions<TestDbContext> options) : DbContext(options)
{
    public DbSet<TestEntity> TestEntities { get; set; } = null!;
    public DbSet<TestEntityWithRelation> TestEntityWithRelations { get; set; } = null!;
    public DbSet<TestRelatedEntity> TestRelatedEntities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestEntityWithRelation>()
            .HasOne(e => e.Related)
            .WithMany();

        base.OnModelCreating(modelBuilder);
    }
}


public class TestEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class TestDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}


public class TestEntityWithRelation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public TestRelatedEntity? Related { get; set; }
}

public class TestRelatedEntity
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
}