using API.Specifications;
using Microsoft.EntityFrameworkCore;
using Specification.Lite;

namespace API;

public class TestRepository(TestDbContext context) : ITestRepository
{
    public async Task<List<TestEntity>> GetWhere()
    {
        var spec = new TestWhereSpec();
        List<TestEntity> list = await context.TestEntities
            .WithSpecification(spec)
            .ToListAsync();

        return list;
    }


    public async Task<List<TestEntityWithRelation>> GetWithInclude()
    {
        var spec = new TestIncludeSpec();
        List<TestEntityWithRelation> list = await context.TestEntityWithRelations
            .WithSpecification(spec)
            .ToListAsync();
        return list;
    }


    public async Task<List<TestRelatedEntity>> GetWithOrder()
    {
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
