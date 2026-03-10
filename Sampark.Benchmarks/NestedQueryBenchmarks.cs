using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Text;

// 3-level nesting: Person → Entity → Entity.Children
//
// Resolvers used:
//   personsPaged  — [UsePaging] [UseProjection]  (cursor-based, first N)
//   personsAll    — [UseProjection]               (full result set)
//
// Each resolver drives EF to JOIN entities and then self-join
// entities again for children only when those fields appear in
// the selection set (HotChocolate projection).

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[SimpleJob(warmupCount: 3, iterationCount: 100)]
[ArtifactsPath(@"C:\bdn")]
public class NestedQueryBenchmarks
{
    // ── Level 2: Person → Entity ────────────────────────────────────────────

    private const string QueryPersonsWithEntity_Paged = """
        { "query": "{ personsPaged(first: 20) { nodes { personId firstName lastName entity { entityId name code } } pageInfo { hasNextPage endCursor } } }" }
        """;

    private const string QueryPersonsWithEntity_All = """
        { "query": "{ personsAll { personId firstName lastName entity { entityId name code } } }" }
        """;

    // ── Level 3: Person → Entity → Entity.Children ─────────────────────────

    private const string QueryPersonsWithEntityAndChildren_Paged = """
        { "query": "{ personsPaged(first: 20) { nodes { personId firstName lastName entity { entityId name code children { entityId name code } } } pageInfo { hasNextPage endCursor } } }" }
        """;

    private const string QueryPersonsWithEntityAndChildren_All = """
        { "query": "{ personsAll { personId firstName lastName entity { entityId name code children { entityId name code } } } }" }
        """;

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

    [Benchmark(Baseline = true, Description = "L2 paginated — Person → Entity with 100 iteration")]
    public async Task<int> PersonWithEntity_Paged()
        => (await PostGraphQL(QueryPersonsWithEntity_Paged)).Length;

    [Benchmark(Description = "L2 no paging — Person → Entity with 100 iteration")]
    public async Task<int> PersonWithEntity_All()
        => (await PostGraphQL(QueryPersonsWithEntity_All)).Length;

    [Benchmark(Description = "L3 paginated — Person → Entity → Children with 100 iteration")]
    public async Task<int> PersonWithEntityAndChildren_Paged()
        => (await PostGraphQL(QueryPersonsWithEntityAndChildren_Paged)).Length;

    [Benchmark(Description = "L3 no paging — Person → Entity → Children with 100 iteration")]
    public async Task<int> PersonWithEntityAndChildren_All()
        => (await PostGraphQL(QueryPersonsWithEntityAndChildren_All)).Length;

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
