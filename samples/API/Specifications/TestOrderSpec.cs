using Specification.Lite;

namespace API.Specifications;

public class TestOrderSpec : Specification<TestRelatedEntity>
{
    public TestOrderSpec() => Query
        .Where(entity => entity.Id < 4)
        .Include(entity => entity.Nested)
        .OrderByDescending(entity => entity.Department)
        .ThenByDescending(entity => entity.Salary)
        .Skip(1)
        .Take(1);
}
