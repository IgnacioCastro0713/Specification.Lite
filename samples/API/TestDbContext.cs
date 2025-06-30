using Microsoft.EntityFrameworkCore;

namespace API;

public class TestDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "TestDb");
    }

    public DbSet<TestEntity> TestEntities { get; set; } = null!;
    public DbSet<TestEntityWithRelation> TestEntityWithRelations { get; set; } = null!;
    public DbSet<TestRelatedEntity> TestRelatedEntities { get; set; } = null!;
    public DbSet<TestNestedEntity> TestNestedEntities { get; set; } = null!;
    public DbSet<TestDeepEntity> TestDeepEntities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestEntityWithRelation>()
            .HasOne(e => e.Related)
            .WithMany();

        modelBuilder.Entity<TestRelatedEntity>()
            .HasOne(e => e.Nested)
            .WithMany();

        modelBuilder.Entity<TestNestedEntity>()
            .HasMany(e => e.Deeps)
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
    public int? RelatedId { get; set; }
}

public class TestRelatedEntity
{
    public int Id { get; set; }
    public string Department { get; set; } = string.Empty;
    public int Salary { get; set; }
    public TestNestedEntity? Nested { get; set; }
    public int? NestedId { get; set; }
}

public class TestNestedEntity
{
    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public List<TestDeepEntity> Deeps { get; set; } = [];
}

public class TestDeepEntity
{
    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;
}
