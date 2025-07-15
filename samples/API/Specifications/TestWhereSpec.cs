using Specification.Lite;

namespace API.Specifications;

public class TestWhereSpec : Specification<TestEntity>
{
    public TestWhereSpec() =>
        Query
            .TagWith("Tag where in spec query 1")
            .TagWith("Tag where in spec query 2")
            .Where(entity => entity.Id == 4 || entity.Id == 1)
            .Where(entity => entity.Id == 4);
}
