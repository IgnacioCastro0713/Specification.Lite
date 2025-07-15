using API;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddDbContext<TestDbContext>(options =>
{
    string currentDirectory = Directory.GetCurrentDirectory();
    string dbPath = Path.Combine(currentDirectory, "TESTDB.db");
    options.UseSqlite($"Data Source={dbPath}");
    options.LogTo(
            Console.WriteLine,
            [DbLoggerCategory.Database.Command.Name], LogLevel.Information)
        .EnableSensitiveDataLogging();
});


WebApplication app = builder.Build();


using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;
    TestDbContext context = services.GetRequiredService<TestDbContext>();
    ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        if (!context.TestEntities.Any())
        {
            logger.LogInformation("Seeding TestEntities...");
            context.TestEntities.AddRange(
                new TestEntity { Id = 1, Name = "Basic Entity 1" },
                new TestEntity { Id = 2, Name = "Basic Entity 2" },
                new TestEntity { Id = 3, Name = "Basic Entity 3" },
                new TestEntity { Id = 4, Name = "Basic Entity 4" },
                new TestEntity { Id = 5, Name = "Basic Entity 5" }
            );
            context.SaveChanges();
        }

        if (!context.TestNestedEntities.Any())
        {
            logger.LogInformation("Seeding TestNestedEntities...");
            context.TestNestedEntities.AddRange(
                new TestNestedEntity
                {
                    Id = 1,
                    Value = "Nested Value 1",
                    Deeps =
                    [
                        new TestDeepEntity { Id = 1, Value = "Deep Value 1" },
                        new TestDeepEntity { Id = 2, Value = "Deep Value 2" },
                        new TestDeepEntity { Id = 3, Value = "Deep Value 3" }
                    ]
                }
            );
            context.SaveChanges();
        }
        if (!context.TestRelatedEntities.Any())
        {
            logger.LogInformation("Seeding TestRelatedEntities...");
            context.TestRelatedEntities.AddRange(
                new TestRelatedEntity { Id = 1, Department = "HR", Salary = 60000, NestedId = 1 },
                new TestRelatedEntity { Id = 2, Department = "HR", Salary = 55000, NestedId = 2 },
                new TestRelatedEntity { Id = 3, Department = "IT", Salary = 70000, NestedId = 3 },
                new TestRelatedEntity { Id = 4, Department = "Sales", Salary = 65000, NestedId = null },
                new TestRelatedEntity { Id = 5, Department = "Sales", Salary = 72000, NestedId = null }
            );
            context.SaveChanges();
        }

        if (!context.TestEntityWithRelations.Any())
        {
            logger.LogInformation("Seeding TestEntityWithRelations...");
            context.TestEntityWithRelations.AddRange(
                new TestEntityWithRelation { Id = 1, Name = "Relation Entity 1", RelatedId = 1 },
                new TestEntityWithRelation { Id = 2, Name = "Relation Entity 2", RelatedId = 2 },
                new TestEntityWithRelation { Id = 3, Name = "Relation Entity 3", RelatedId = 3 },
                new TestEntityWithRelation { Id = 4, Name = "Relation Entity with no relation", RelatedId = null }
            );
            context.SaveChanges();
        }


        logger.LogInformation("Database seeding completed.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while seeding the in-memory database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
