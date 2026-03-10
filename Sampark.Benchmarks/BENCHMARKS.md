# Sampark GraphQL Benchmarks

HTTP-level benchmarks measuring end-to-end GraphQL query latency and memory
allocation against a live API, using [BenchmarkDotNet](https://benchmarkdotnet.org/) 0.15.8.

---

## Prerequisites

| # | Action |
|---|---|
| 1 | PostgreSQL running with the Sampark schema seeded |
| 2 | API started in a separate terminal |
| 3 | Run benchmarks in **Release** mode only |

```bash
# Terminal 1 — start the API
cd C:\work\.net\Sampark
dotnet run --launch-profile http

# Terminal 2 — run benchmarks
cd C:\work\.net\Sampark\Sampark.Benchmarks
dotnet run -c Release
```

> **Note:** BDN job artifacts are written to `C:\bdn` (set via `[ArtifactsPath]`)
> to avoid the MAX_PATH recursion caused by the benchmark project living inside
> the main project tree.

---

## Project Structure

```
Sampark.Benchmarks/
├── Program.cs                  ← switch active benchmark class here
├── PersonQueryBenchmarks.cs    ← projection / filter benchmarks on Person
├── EntityQueryBenchmarks.cs    ← pagination vs no-pagination on Entity
├── NestedQueryBenchmarks.cs    ← multi-level nesting (Person → Entity → Children)
└── BENCHMARKS.md               ← this file
```

### Switching benchmark classes

Edit `Program.cs` to point to the class you want to run:

```csharp
BenchmarkRunner.Run<PersonQueryBenchmarks>();    // projection / filter
BenchmarkRunner.Run<EntityQueryBenchmarks>();    // pagination comparison
BenchmarkRunner.Run<NestedQueryBenchmarks>();    // nested joins
```

---

## Common Configuration

All classes share these BDN attributes unless noted:

| Attribute | Value | Purpose |
|---|---|---|
| `[MemoryDiagnoser]` | — | Reports GC allocations (Gen0/1/2, allocated bytes) |
| `[Orderer]` | `FastestToSlowest` | Results sorted by mean latency |
| `[SimpleJob]` | `warmupCount: 3` | 3 warmup iterations discarded before measurement |
| `[SimpleJob]` | `iterationCount: 100` | 100 measured iterations per benchmark |
| `[ArtifactsPath]` | `C:\bdn` | BDN job output outside the project tree |

---

## Benchmark Classes

### 1. `PersonQueryBenchmarks` — Projection & Filter Cost

**Goal:** Quantify the cost of different projection widths and server-side
filtering on the `persons` resolver.

| Method | Description | GraphQL resolver |
|---|---|---|
| `GetAllPersons` *(baseline)* | 4 scalar fields | `persons` |
| `GetPersonsSmallProjection` | 2 scalar fields | `persons` |
| `GetPersonsLargeProjection` | ~20 scalar fields | `persons` |
| `GetPersonsFilteredByGender` | WHERE `gender = "M"` | `persons` |
| `GetPersonById` | Single row by PK | `persons` |

**GraphQL queries:**

```graphql
# Baseline (4 fields)
{ persons { personId firstName lastName gender } }

# Small projection (2 fields)
{ persons { personId firstName } }

# Large projection (~20 fields)
{ persons { personId bapsId gender firstName middleName lastName prefix suffix
            recordType maritalStatus defaultMandalId departmentId
            defaultMandalName homePhoneMasked cellphoneMasked emailMasked
            addressMasked stateCode countryName relation sevaCategory } }

# Filtered by gender
{ persons(where: { gender: { eq: "M" } }) { personId firstName lastName stateCode } }

# Single row by ID
{ persons(where: { personId: { eq: 1 } }) { personId firstName lastName emailMasked } }
```

**What to look for:** Allocation growth between small and large projections;
latency delta between filtered and unfiltered full-table scans.

---

### 2. `EntityQueryBenchmarks` — Pagination vs No Pagination

**Goal:** Measure the overhead introduced by HotChocolate's cursor-based
connection paging (`[UsePaging]`) versus a plain `[UseProjection]` resolver,
both with and without the self-referential children join.

| Method | Description | Resolver | Children join |
|---|---|---|---|
| `WithPagination` *(baseline)* | first 20, flat | `entities` | ✗ |
| `WithoutPagination` | all rows, flat | `entitiesAll` | ✗ |
| `WithPaginationAndChildren` | first 20 + children | `entities` | ✓ |
| `WithoutPaginationAndChildren` | all rows + children | `entitiesAll` | ✓ |

**GraphQL queries:**

```graphql
# Paginated — flat
{ entities(first: 20) {
    nodes { entityId name code is_active }
    pageInfo { hasNextPage endCursor }
} }

# No pagination — flat
{ entitiesAll { entityId name code is_active } }

# Paginated — with children
{ entities(first: 20) {
    nodes { entityId name code is_active
            children { entityId name code } }
    pageInfo { hasNextPage endCursor }
} }

# No pagination — with children
{ entitiesAll { entityId name code is_active
                children { entityId name code } }
```

**GraphQL resolvers (GenericQuery.cs):**

```csharp
// entities — cursor paging
[UsePaging] [UseProjection] [UseFiltering]
public IQueryable<Entity> Entities(...)

// entitiesAll — no paging
[UseProjection] [UseFiltering]
public IQueryable<Entity> EntitiesAll(...)
```

**What to look for:** Latency added by connection-type wrapping; whether
fetching 20 paginated rows with children is faster than fetching all rows flat.

---

### 3. `NestedQueryBenchmarks` — Multi-level Join Depth

**Goal:** Isolate the SQL JOIN cost of each additional nesting level.
The query chain is `Person → Entity → Entity.Children`.

| Method | Level | Paging | SQL joins |
|---|---|---|---|
| `PersonWithEntity_Paged` *(baseline)* | L2 | ✓ first 20 | persons ⟶ entities |
| `PersonWithEntity_All` | L2 | ✗ | persons ⟶ entities |
| `PersonWithEntityAndChildren_Paged` | L3 | ✓ first 20 | persons ⟶ entities ⟶ entities (self) |
| `PersonWithEntityAndChildren_All` | L3 | ✗ | persons ⟶ entities ⟶ entities (self) |

**GraphQL queries:**

```graphql
# L2 — paginated
{ personsPaged(first: 20) {
    nodes { personId firstName lastName
            entity { entityId name code } }
    pageInfo { hasNextPage endCursor }
} }

# L2 — no paging
{ personsAll { personId firstName lastName
               entity { entityId name code } } }

# L3 — paginated
{ personsPaged(first: 20) {
    nodes { personId firstName lastName
            entity { entityId name code
                     children { entityId name code } } }
    pageInfo { hasNextPage endCursor }
} }

# L3 — no paging
{ personsAll { personId firstName lastName
               entity { entityId name code
                        children { entityId name code } } } }
```

**GraphQL resolvers (GenericQuery.cs):**

```csharp
// personsPaged — cursor paging + projection
[UsePaging] [UseProjection] [UseFiltering]
public IQueryable<Person> PersonsPaged(...)

// personsAll — projection only
[UseProjection] [UseFiltering]
public IQueryable<Person> PersonsAll(...)
```

**Model relationships:**

```
Person.EntityId  ──FK──►  Entity.EntityId
Entity.parent_entity_id  ──FK──►  Entity.EntityId  (self-referential)
```

**What to look for:** Latency delta between L2 and L3 quantifies the cost of
the extra self-join on the `entities` table. Compare paged L3 vs unpaged L3 to
see whether limiting to 20 persons meaningfully reduces the children join cost.

---

## Reading BDN Output

```
| Method                          | Mean     | Error   | StdDev  | Ratio | Gen0   | Allocated |
|-------------------------------- |---------:|--------:|--------:|------:|-------:|----------:|
| PersonWithEntity_Paged          | 12.34 ms | 0.23 ms | 0.21 ms |  1.00 |  62.50 |  245.6 KB |
| PersonWithEntityAndChildren_All | 38.12 ms | 0.71 ms | 0.66 ms |  3.09 | 187.50 |  812.3 KB |
```

| Column | Meaning |
|---|---|
| `Mean` | Average latency over all iterations |
| `Error` | Half-width of 99.9% confidence interval |
| `StdDev` | Standard deviation — high values indicate network jitter |
| `Ratio` | Relative to the baseline (1.00) |
| `Gen0` | GC gen-0 collections per 1000 iterations |
| `Allocated` | Managed heap bytes allocated per operation |

---

## Known Issues

| Issue | Cause | Fix applied |
|---|---|---|
| `MAX_PATH` during `dotnet run -c Release` | BDN job folder nested inside project `bin\`, which is inside `Sampark\` tree; MSBuild glob creates recursive path > 260 chars | `[ArtifactsPath(@"C:\bdn")]` on every benchmark class |
| `NU1605` package downgrade | `Sampark.csproj` previously referenced `BenchmarkDotNet 0.15.8`; benchmark project referenced `0.14.0` via `<ProjectReference>` | Removed BDN from `Sampark.csproj`; aligned benchmark project to `0.15.8` |
