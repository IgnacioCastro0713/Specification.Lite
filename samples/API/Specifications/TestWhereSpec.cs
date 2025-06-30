using Specification.Lite;

namespace API.Specifications;

public class TestWhereSpec : Specification<TestEntity>
{
    public TestWhereSpec()
    {
        AddWhere(entity => entity.Id == 4 || entity.Id == 1);
        AddWhere(entity => entity.Id == 4);
        ApplyDistinctBy(entity => entity.Name);
    }
}
