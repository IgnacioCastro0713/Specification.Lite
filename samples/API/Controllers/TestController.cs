using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController(ITestRepository repository) : ControllerBase
{
    [HttpGet(nameof(Where))]
    public async Task<List<TestEntity>> Where()
    {
        return await repository.GetWhere();
    }

    [HttpGet(nameof(GetWithInclude))]
    public async Task<List<TestEntityWithRelation>> GetWithInclude()
    {
        return await repository.GetWithInclude();
    }
}
