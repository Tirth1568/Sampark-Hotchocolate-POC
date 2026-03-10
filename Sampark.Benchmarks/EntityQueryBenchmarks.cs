using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Text;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[SimpleJob(warmupCount: 3, iterationCount: 100)]
[ArtifactsPath(@"C:\bdn")]
public class EntityQueryBenchmarks
{
    // ── Active benchmarks ───────────────────────────────────────────────────

    // With pagination — flat (no children)
    private const string QueryWithPagination = """
        { "query": "{ entities(first: 20) { nodes { entityId name code is_active } pageInfo { hasNextPage endCursor } } }" }
        """;

    // Without pagination — flat (no children)
    private const string QueryWithoutPagination = """
        { "query": "{ entitiesAll { entityId name code is_active } }" }
        """;

    // With pagination — includes children (self-referential join)
    private const string QueryWithPaginationAndChildren = """
        { "query": "{ entities(first: 20) { nodes { entityId name code is_active children { entityId name code } } pageInfo { hasNextPage endCursor } } }" }
        """;

    // Without pagination — includes children (self-referential join)
    private const string QueryWithoutPaginationAndChildren = """
        { "query": "{ entitiesAll { entityId name code is_active children { entityId name code } } }" }
        """;

    // ── Commented-out projection / filter benchmarks (run separately) ───────
    //
    // private const string QueryAllEntities =
    //     """{ "query": "{ entities { entityId name code is_active } }" }""";
    //
    // private const string QueryEntitiesSmallProjection =
    //     """{ "query": "{ entities { entityId name } }" }""";
    //
    // private const string QueryEntitiesLargeProjection =
    //     """{ "query": "{ entities { entityId division_id code name parent_entity_id division_geo_level_id phone email uuid is_active created_at created_by updated_at updated_by } }" }""";
    //
    // private const string QueryEntitiesFiltered =
    //     """{ "query": "{ entities(where: { is_active: { eq: 1 } }) { entityId name code division_id } }" }""";
    //
    // private const string QueryEntityById =
    //     """{ "query": "{ entities(where: { entityId: { eq: 1 } }) { entityId name code email phone } }" }""";
    //
    // [Benchmark(Baseline = true, Description = "All entities — 4 fields")]
    // public async Task<int> GetAllEntities() => (await PostGraphQL(QueryAllEntities)).Length;
    //
    // [Benchmark(Description = "All entities — 2 fields (small projection)")]
    // public async Task<int> GetEntitiesSmallProjection() => (await PostGraphQL(QueryEntitiesSmallProjection)).Length;
    //
    // [Benchmark(Description = "All entities — all scalar fields (large projection)")]
    // public async Task<int> GetEntitiesLargeProjection() => (await PostGraphQL(QueryEntitiesLargeProjection)).Length;
    //
    // [Benchmark(Description = "Filter entities by is_active (WHERE clause)")]
    // public async Task<int> GetEntitiesFilteredByActive() => (await PostGraphQL(QueryEntitiesFiltered)).Length;
    //
    // [Benchmark(Description = "Single entity by ID (best-case latency)")]
    // public async Task<int> GetEntityById() => (await PostGraphQL(QueryEntityById)).Length;

    // ── Setup ───────────────────────────────────────────────────────────────

    private HttpClient _client = null!;

    [GlobalSetup]
    public void Setup()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5127/"),
            Timeout = TimeSpan.FromSeconds(30)
        };
        _client.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    // ── Benchmarks ──────────────────────────────────────────────────────────

    [Benchmark(Baseline = true, Description = "Paginated — flat (no children)")]
    public async Task<int> WithPagination()
        => (await PostGraphQL(QueryWithPagination)).Length;

    [Benchmark(Description = "No pagination — flat (no children)")]
    public async Task<int> WithoutPagination()
        => (await PostGraphQL(QueryWithoutPagination)).Length;

    [Benchmark(Description = "Paginated — with children (self-referential join)")]
    public async Task<int> WithPaginationAndChildren()
        => (await PostGraphQL(QueryWithPaginationAndChildren)).Length;

    [Benchmark(Description = "No pagination — with children (self-referential join)")]
    public async Task<int> WithoutPaginationAndChildren()
        => (await PostGraphQL(QueryWithoutPaginationAndChildren)).Length;

    // ── Cleanup ─────────────────────────────────────────────────────────────

    [GlobalCleanup]
    public void Cleanup() => _client.Dispose();

    // ── Helpers ─────────────────────────────────────────────────────────────

    private async Task<string> PostGraphQL(string body)
    {
        using var content = new StringContent(body, Encoding.UTF8, "application/json");
        using var response = await _client.PostAsync("graphql", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
