using API.Specifications;
using Microsoft.EntityFrameworkCore;
using Specification.Lite;

namespace API;

public class TestRepository(ILogger<TestRepository> logger, TestDbContext context) : ITestRepository
{
    public async Task<List<TestEntity>> GetWhere()
    {
        logger.LogInformation("Fetching entities with where clause");
        Console.WriteLine("--- Before EF Core query ---"); // <--- Add this!

        List<TestEntity> list = await context.TestEntities
            .TagWith("ConsoleAppTag")
            .ToListAsync();
        Console.WriteLine("--- After EF Core query ---");  // <--- Add this!
        logger.LogInformation("Finished fetching entities.");
        //var spec = new TestWhereSpec();
        //List<TestEntity> list = await context.TestEntities
        //    .WithSpecification(spec)
        //    .ToListAsync();

        return list;
    }


    public async Task<List<TestEntityWithRelation>> GetWithInclude()
    {
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


        //List<TestRelatedEntity> list = await context.TestRelatedEntities
        //    .Where(entity => entity.Id < 4)
        //    .OrderByDescending(entity => entity.Department)
        //    .ThenByDescending(entity => entity.Salary)
        //    .Skip(1)
        //    .Take(1)
        //    .ToListAsync();

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
