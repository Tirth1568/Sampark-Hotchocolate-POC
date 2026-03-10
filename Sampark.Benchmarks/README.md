# Sampark BenchmarkDotNet Performance Suite

This benchmark project uses [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) to measure real .NET query performance for the existing GraphQL query implementation in `GenericQuery`/`TestQuery`.

## What's measured

Benchmarks target the Person query path (larger dataset) and run against an in-memory EF Core store seeded during benchmark setup:

- `PersonsQuery_CountAll`
- `PersonsQuery_ToList`
- `GenericGetPerson_ToList`
- `PersonsQuery_ActiveOnly_ToList`

`PersonCount` is parameterized with `1,000` and `10,000` rows to compare scale impact.

## Run benchmarks

```bash
dotnet run --project Sampark.Benchmarks/Sampark.Benchmarks.csproj -c Release
```

BenchmarkDotNet writes detailed reports under:

- `Sampark.Benchmarks/bin/Release/net8.0/BenchmarkDotNet.Artifacts/results`
