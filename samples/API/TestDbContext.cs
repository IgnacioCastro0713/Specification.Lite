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

        // Seed data for TestEntity
        modelBuilder.Entity<TestEntity>().HasData(
            new TestEntity { Id = 1, Name = "Basic Entity 1" },
            new TestEntity { Id = 2, Name = "Basic Entity 2" },
            new TestEntity { Id = 3, Name = "Basic Entity 3" },
            new TestEntity { Id = 4, Name = "Basic Entity 4" },
            new TestEntity { Id = 5, Name = "Basic Entity 5" }
        );

        // Seed data for TestNestedEntity (deepest entities first)
        modelBuilder.Entity<TestNestedEntity>().HasData(
            new TestNestedEntity { Id = 1, Value = "Nested Value 1" },
            new TestNestedEntity { Id = 2, Value = "Nested Value 2" },
            new TestNestedEntity { Id = 3, Value = "Nested Value 3" }
        );

        // Seed data for TestRelatedEntity (middle level entities)
        modelBuilder.Entity<TestRelatedEntity>().HasData(
            new TestRelatedEntity { Id = 1, Description = "Related Description 1", NestedId = 1 },
            new TestRelatedEntity { Id = 2, Description = "Related Description 2", NestedId = 2 },
            new TestRelatedEntity { Id = 3, Description = "Related Description 3", NestedId = 3 },
            new TestRelatedEntity { Id = 4, Description = "Related with no nested", NestedId = null }
        );

        // Seed data for TestEntityWithRelation (top level entities)
        modelBuilder.Entity<TestEntityWithRelation>().HasData(
            new TestEntityWithRelation { Id = 1, Name = "Relation Entity 1", RelatedId = 1 },
            new TestEntityWithRelation { Id = 2, Name = "Relation Entity 2", RelatedId = 2 },
            new TestEntityWithRelation { Id = 3, Name = "Relation Entity 3", RelatedId = 3 },
            new TestEntityWithRelation { Id = 4, Name = "Relation Entity with no relation", RelatedId = null }
        );

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
    public int? RelatedId { get; set; }
}

public class TestRelatedEntity
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public TestNestedEntity? Nested { get; set; }
    public int? NestedId { get; set; }
}

public class TestNestedEntity
{
    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;
}
