using Specification.Lite;

namespace API.Specifications;

public class TestWhereSpec : Specification<TestEntity>
{
    public TestWhereSpec()
    {
        Where(entity => entity.Id == 4 || entity.Id == 1);
        Where(entity => entity.Id == 4);
    }
}
