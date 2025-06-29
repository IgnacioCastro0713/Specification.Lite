using Microsoft.EntityFrameworkCore;

namespace API;

public class TestDbContext(DbContextOptions<TestDbContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: $"TestDb-{Guid.NewGuid()}");
    }

    public DbSet<TestEntity> TestEntities { get; set; } = null!;
    public DbSet<TestEntityWithRelation> TestEntityWithRelations { get; set; } = null!;
    public DbSet<TestRelatedEntity> TestRelatedEntities { get; set; } = null!;
    public DbSet<TestNestedEntity> TestNestedEntities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestEntityWithRelation>()
            .HasOne(e => e.Related)
            .WithMany();

        modelBuilder.Entity<TestRelatedEntity>()
            .HasOne(e => e.Nested)
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
    public TestNestedEntity? Nested { get; set; }

}

public class TestNestedEntity
{
    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;
}
