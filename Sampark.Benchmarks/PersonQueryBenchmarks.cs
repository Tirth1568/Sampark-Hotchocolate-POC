using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Text;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[SimpleJob(warmupCount: 3, iterationCount: 10)]
public class PersonQueryBenchmarks
{
    // ── Queries under test ──────────────────────────────────────────────────

    // Baseline: minimal fields
    private const string QueryAllPersons = """
        { "query": "{ persons { personId firstName lastName gender } }" }
        """;

    // Smaller projection: only 2 fields
    private const string QueryPersonsSmallProjection = """
        { "query": "{ persons { personId firstName } }" }
        """;

    // Larger projection: all scalar fields
    private const string QueryPersonsLargeProjection = """
        { "query": "{ persons { personId bapsId  gender firstName middleName lastName prefix suffix recordType maritalStatus defaultMandalId departmentId defaultMandalName homePhoneMasked cellphoneMasked emailMasked addressMasked stateCode countryName relation sevaCategory } }" }
        """;

    // Filtered: server-side WHERE
    private const string QueryPersonsFiltered = """
        { "query": "{ persons(where: { gender: { eq: \"M\" } }) { personId firstName lastName stateCode } }" }
        """;

    // Single row by ID
    private const string QueryPersonById = """
        { "query": "{ persons(where: { personId: { eq: 1 } }) { personId firstName lastName emailMasked } }" }
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

    [Benchmark(Baseline = true, Description = "All persons — 4 fields")]
    public async Task<int> GetAllPersons()
        => (await PostGraphQL(QueryAllPersons)).Length;

    [Benchmark(Description = "All persons — 2 fields (small projection)")]
    public async Task<int> GetPersonsSmallProjection()
        => (await PostGraphQL(QueryPersonsSmallProjection)).Length;

    [Benchmark(Description = "All persons — all scalar fields (large projection)")]
    public async Task<int> GetPersonsLargeProjection()
        => (await PostGraphQL(QueryPersonsLargeProjection)).Length;

    [Benchmark(Description = "Filter persons by gender (WHERE clause)")]
    public async Task<int> GetPersonsFilteredByGender()
        => (await PostGraphQL(QueryPersonsFiltered)).Length;

    [Benchmark(Description = "Single person by ID (best-case latency)")]
    public async Task<int> GetPersonById()
        => (await PostGraphQL(QueryPersonById)).Length;

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
