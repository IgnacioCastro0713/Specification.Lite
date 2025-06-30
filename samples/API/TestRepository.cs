using API.Specifications;
using Microsoft.EntityFrameworkCore;
using Specification.Lite.Extensions;

namespace API;

public class TestRepository : ITestRepository
{
    public TestRepository()
    {
        using var context = new TestDbContext();
        // Seed data for TestEntity
        context.TestEntities.AddRange(
            new TestEntity { Id = 1, Name = "Basic Entity 1" },
            new TestEntity { Id = 2, Name = "Basic Entity 2" },
            new TestEntity { Id = 3, Name = "Basic Entity 3" },
            new TestEntity { Id = 4, Name = "Basic Entity 4" },
            new TestEntity { Id = 5, Name = "Basic Entity 5" }
        );

        // Seed data for TestNestedEntity (deepest entities first)
        context.TestNestedEntities.AddRange(
            new TestNestedEntity { Id = 1, Value = "Nested Value 1" },
            new TestNestedEntity { Id = 2, Value = "Nested Value 2" },
            new TestNestedEntity { Id = 3, Value = "Nested Value 3" }
        );

        // Seed data for TestRelatedEntity (middle level entities)
        context.TestRelatedEntities.AddRange(
            new TestRelatedEntity { Id = 1, Description = "Related Description 1", NestedId = 1 },
            new TestRelatedEntity { Id = 2, Description = "Related Description 2", NestedId = 2 },
            new TestRelatedEntity { Id = 3, Description = "Related Description 3", NestedId = 3 },
            new TestRelatedEntity { Id = 4, Description = "Related with no nested", NestedId = null }
        );

        // Seed data for TestEntityWithRelation (top level entities)
        context.TestEntityWithRelations.AddRange(
            new TestEntityWithRelation { Id = 1, Name = "Relation Entity 1", RelatedId = 1 },
            new TestEntityWithRelation { Id = 2, Name = "Relation Entity 2", RelatedId = 2 },
            new TestEntityWithRelation { Id = 3, Name = "Relation Entity 3", RelatedId = 3 },
            new TestEntityWithRelation { Id = 4, Name = "Relation Entity with no relation", RelatedId = null }
        );

        context.SaveChanges();
    }



    public async Task<List<TestEntity>> GetWhere()
    {
        await using var context = new TestDbContext();

        var spec = new TestWhereSpec();
        List<TestEntity> list = await context.TestEntities.ToListAsync(spec);

        return list;
    }
}

public interface ITestRepository
{
    Task<List<TestEntity>> GetWhere();
}
