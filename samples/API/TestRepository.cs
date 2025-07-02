using API.Specifications;
using Microsoft.EntityFrameworkCore;
using Specification.Lite.Evaluators;

namespace API;

public class TestRepository : ITestRepository
{
    public TestRepository()
    {
        using var context = new TestDbContext();
        // Seed data for TestEntity

        if (!context.TestEntities.Any())
        {
            context.TestEntities.AddRange(
                new TestEntity { Id = 1, Name = "Basic Entity 1" },
                new TestEntity { Id = 2, Name = "Basic Entity 2" },
                new TestEntity { Id = 3, Name = "Basic Entity 3" },
                new TestEntity { Id = 4, Name = "Basic Entity 4" },
                new TestEntity { Id = 5, Name = "Basic Entity 5" }
            );
        }


        // Seed data for TestDeepEntities (deep entities first)



        // Seed data for TestNestedEntity (deepest entities first)

        if (!context.TestNestedEntities.Any())
        {
            context.TestNestedEntities.AddRange(
                new TestNestedEntity
                {
                    Id = 1,
                    Value = "Nested Value 1",
                    Deeps = [
                    new TestDeepEntity { Id = 1, Value = "Deep Value 1" },
                    new TestDeepEntity { Id = 2, Value = "Deep Value 2" },
                    new TestDeepEntity { Id = 3, Value = "Deep Value 3" }]
                }
            );
        }


        // Seed data for TestRelatedEntity (middle level entities)

        if (!context.TestRelatedEntities.Any())
        {
            context.TestRelatedEntities.AddRange(
                new TestRelatedEntity { Id = 1, Department = "HR", Salary = 60000, NestedId = 1 },
                new TestRelatedEntity { Id = 2, Department = "HR", Salary = 55000, NestedId = 2 },
                new TestRelatedEntity { Id = 3, Department = "IT", Salary = 70000, NestedId = 3 },
                new TestRelatedEntity { Id = 4, Department = "Sales", Salary = 65000, NestedId = null },
                new TestRelatedEntity { Id = 5, Department = "Sales", Salary = 72000, NestedId = null }
            );
        }

        // Seed data for TestEntityWithRelation (top level entities)
        if (!context.TestEntityWithRelations.Any())
        {
            context.TestEntityWithRelations.AddRange(
                new TestEntityWithRelation { Id = 1, Name = "Relation Entity 1", RelatedId = 1 },
                new TestEntityWithRelation { Id = 2, Name = "Relation Entity 2", RelatedId = 2 },
                new TestEntityWithRelation { Id = 3, Name = "Relation Entity 3", RelatedId = 3 },
                new TestEntityWithRelation { Id = 4, Name = "Relation Entity with no relation", RelatedId = null }
            );
        }

        context.SaveChanges();
    }


    public async Task<List<TestEntity>> GetWhere()
    {
        await using var context = new TestDbContext();

        var spec = new TestWhereSpec();
        List<TestEntity> list = await context.TestEntities
            .ToListAsync(spec);

        return list;
    }


    public async Task<List<TestEntityWithRelation>> GetWithInclude()
    {
        await using var context = new TestDbContext();
        //List<TestEntityWithRelation> list = await context.TestEntityWithRelations
        //    .Include(e => e.Related)
        //    .ThenInclude(e => e!.Nested)
        //    .ThenInclude(e => e!.Deeps)
        //    .ThenInclude(e => e.MoreDeep)
        //    .ToListAsync();
        var spec = new TestIncludeSpec();
        List<TestEntityWithRelation> list = await context.TestEntityWithRelations
            .WithSpecification(spec)
            .ToListAsync();
        return list;
    }


    public async Task<List<TestRelatedEntity>> GetWithOrder()
    {
        await using var context = new TestDbContext();

        var spec = new TestOrderSpec();
        List<TestRelatedEntity> list = await context.TestRelatedEntities
            .WithSpecification(spec)
            .ToListAsync();
        return list;
    }
}

public interface ITestRepository
{
    Task<List<TestEntity>> GetWhere();
    Task<List<TestEntityWithRelation>> GetWithInclude();
    Task<List<TestRelatedEntity>> GetWithOrder();
}
