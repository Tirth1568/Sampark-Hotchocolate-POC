# Sampark HotChocolate POC Overview

This proof-of-concept shows how to expose a PostgreSQL-backed data model through **HotChocolate GraphQL** in ASP.NET Core.

## What this POC demonstrates

- **Entity Framework Core + Npgsql** integration for database access.
- **GraphQL Query and Mutation wiring** with HotChocolate type extensions.
- **Reusable generic mutation base** (`GenericMutation<TEntity, TInput>`) to reduce CRUD duplication.
- **FluentValidation-based input validation** before writes.
- **Core relational model** for projects, people, entities, karyakars, and families.

## Request flow (high-level)

1. Client calls GraphQL endpoint (`/graphql`).
2. HotChocolate resolves query/mutation methods.
3. Resolver uses `SamparkDbContext` to read/write database records.
4. For mutations, input is validated with FluentValidation first.
5. EF Core persists changes to PostgreSQL.

## Key files

- `Program.cs`: service setup and GraphQL endpoint registration.
- `Data/SamparkDbContext.cs`: DbSets and relationship configuration.
- `GraphQL/GenericQuery.cs`: query resolvers for read operations.
- `GraphQL/GenericMutation.cs`: shared CRUD mutation implementation.
- `GraphQL/Mutations/*`: entity-specific mutation mappings.
- `GraphQL/Inputs/*` + `GraphQL/Validators/*`: DTOs and rules.
- `Models/*`: EF Core entity classes mapped to DB tables.

## Why this is a POC

The code focuses on proving the stack and architecture (GraphQL + EF Core + validation + relational modeling), rather than production concerns like authentication/authorization policies, pagination strategy, audit controls, and domain service layering.
