# Specification.Lite

[![NuGet](https://img.shields.io/nuget/dt/Specification.Lite.svg)](https://www.nuget.org/packages/Specification.Lite) 
[![NuGet](https://img.shields.io/nuget/vpre/Specification.Lite.svg)](https://www.nuget.org/packages/Specification.Lite)
[![GitHub](https://img.shields.io/github/license/IgnacioCastro0713/Specification.Lite?style=flat-square)](https://github.com/IgnacioCastro0713/Specification.Lite/blob/main/LICENSE)

Specification.Lite is a lightweight library designed to simplify the implementation of the Specification pattern in .NET applications. It provides a flexible and reusable way to define and apply business rules, filtering, ordering, and projections to queries.

## Features

### 1. Specification Pattern
- Define reusable business rules and query logic using specifications.
- Supports filtering, ordering, projections, and includes.

### 2. Query Extensions
- Apply specifications directly to `IQueryable` objects.
- Supports asynchronous operations like `ToListAsync`, `FirstOrDefaultAsync`, `SingleOrDefaultAsync`, and `AnyAsync`.

### 3. Include Expressions
- Define navigation properties to include in queries using `Include` and `ThenInclude`.

### 4. Ordering
- Apply ordering to queries using `OrderBy` and `OrderByDescending`.

### 5. Projection
- Transform entities into DTOs or other result types using `Select` and `SelectMany`.

### 6. Tracking
- Control entity tracking behavior with `AsTracking` and `AsNoTracking`.

### 7. Integration with Entity Framework
- Seamlessly integrates with Entity Framework for database queries.

## NuGet package:

```pwsh
dotnet add package Specification.Lite --version 0.0.1
```
or
```xml
<PackageReference Include="Specification.Lite" Version="0.0.1" />
```


## Contributing

Contributions, issues, and feature requests are welcome\! Feel free to check.