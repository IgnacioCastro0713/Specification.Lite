# Specification.Lite
![Release](https://github.com/IgnacioCastro0713/Specification.Lite/actions/workflows/build-release.yml/badge.svg)
[![NuGet](https://img.shields.io/nuget/dt/Specification.Lite.svg)](https://www.nuget.org/packages/Specification.Lite)
[![NuGet](https://img.shields.io/nuget/vpre/Specification.Lite.svg)](https://www.nuget.org/packages/Specification.Lite)
[![GitHub](https://img.shields.io/github/license/IgnacioCastro0713/Specification.Lite?style=flat-square)](https://github.com/IgnacioCastro0713/Specification.Lite/blob/main/LICENSE)

**Specification.Lite** is a lightweight .NET library that streamlines the implementation of the Specification pattern. It helps you encapsulate, reuse, and combine business rules, predicates, and query logic in a flexible and maintainable way.

---

## Features

- **Specification Pattern:**  
  Define reusable business rules and query logic using strongly-typed specifications. Encapsulate complex predicates into composable objects.

- **Query Extensions:**  
  Apply specifications directly to `IQueryable` objects for filtering, ordering, and projecting data. Supports asynchronous LINQ operations like `ToListAsync`, `FirstOrDefaultAsync`, `SingleOrDefaultAsync`, and `AnyAsync`.

- **Include Expressions:**  
  Eagerly load related entities in queries using `Include` and `ThenInclude`, just like in Entity Framework.

- **Ordering:**  
  Easily apply ordering to queries with `OrderBy` and `OrderByDescending` methods on your specifications.

- **Projection:**  
  Transform entities to DTOs or other result types within the specification using `Select` and `SelectMany`.

- **Skip & Take:**  
  Effortlessly paginate query results using `Skip` and `Take` inside your specifications.

- **Tracking:**  
  Control whether entities are tracked by the context with `AsTracking`, `AsNoTracking` and `AsNoTrackingWithIdentityResolution` for optimal performance.

- **SplitQuery:**  
  Enables the use of EF Core’s `AsSplitQuery` to optimize queries containing multiple includes, preventing the cartesian explosion problem.

- **IgnoreQueryFilters:**  
  Allows you to bypass global query filters (such as soft delete or multi-tenancy) by applying `IgnoreQueryFilters` in your specifications.

- **IgnoreAutoIncludes:**  
  Prevents Entity Framework Core from automatically applying `Include` statements configured in the model by using `IgnoreAutoIncludes` in your specifications. This gives you full control over which related entities are included in your queries.

- **Entity Framework Integration:**  
  Seamlessly integrates with Entity Framework Core, making it easy to use specifications in your repositories or DbContext queries.

---

## Installation

Install via NuGet Package Manager:

```pwsh
dotnet add package Specification.Lite --version 1.2.0
```
Or add to your project file:

```xml
<PackageReference Include="Specification.Lite" Version="1.2.0" />
```

---

## Usage Examples

### Simple Example

Define a basic specification and use it to query active users:

```csharp
// Define a simple specification for active users
public class ActiveUsersSpecification : Specification<User>
{
    public ActiveUsersSpecification()
    {
        Query
            .Include(u => u.Orders)
            .Where(user => user.IsActive);
    }
}

// Usage in your repository or DbContext
var spec = new ActiveUsersSpecification();
var activeUsers = await dbContext.Users.WithSpecification(spec).ToListAsync();

// Or directly using the DbContext
var activeUsers = await dbContext.Users.ToListAsync(spec);
```

---

### Complex Example

Combine filtering, includes, ordering, projection, pagination, split queries, ignoring query filters, and no-tracking:

```csharp
// Complex specification: Get all active users who registered after 2024-01-01,
// include their orders (with order items), ordered by registration date descending,
// project to a custom DTO, paginate results, use split queries, ignore global query filters, and return as no-tracking.

public class RecentActiveUsersWithOrdersSpec : Specification<User, UserSummaryDto>
{
    public RecentActiveUsersWithOrdersSpec(DateTime since, int skip, int take)
    {
        Query
            .Where(u => u.IsActive && u.RegisteredAt >= since)
            .Include(u => u.Orders)
                .ThenInclude(o => o.OrderItems)
            .OrderByDescending(u => u.RegisteredAt)
            .Skip(skip)
            .Take(take)
            .AsNoTracking()
            .AsSplitQuery()
            .IgnoreQueryFilters()
            .Select(u => new UserSummaryDto
            {
                Id = u.Id,
                Name = u.Name,
                OrderCount = u.Orders.Count,
                TotalSpent = u.Orders.Sum(o => o.TotalAmount)
            });
    }
}

// Usage in your application code
var spec = new RecentActiveUsersWithOrdersSpec(new DateTime(2024, 1, 1), skip: 20, take: 10);
var summaries = await dbContext.Users.WithSpecification(spec).ToListAsync();

// Or directly using the DbContext
var summaries = await dbContext.Users.ToListAsync(spec);
```

See the [examples folder](./examples).

---

## Contributing

Contributions, issues, and feature requests are welcome! or open an issue to get started.
