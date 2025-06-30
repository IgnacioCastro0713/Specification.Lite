using Specification.Lite;

namespace API.Specifications;

public class TestOrderSpec : Specification<TestRelatedEntity>
{
    public TestOrderSpec() =>
        OrderByDescending(entity => entity.Department)
            .ThenByDescending(entity => entity.Salary);
}
