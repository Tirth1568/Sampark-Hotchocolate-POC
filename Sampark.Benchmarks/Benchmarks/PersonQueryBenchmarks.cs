using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Microsoft.EntityFrameworkCore;
using Sampark.Data;
using Sampark.GraphQL;
using Sampark.Models;

namespace Sampark.Benchmarks.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class PersonQueryBenchmarks
{
    private readonly TestQuery _testQuery = new();
    private readonly GenericQuery<Person> _genericPersonQuery = new();

    private DbContextOptions<SamparkDbContext> _dbOptions = default!;
    private string _databaseName = default!;

    [Params(1_000, 10_000)]
    public int PersonCount { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _databaseName = $"sampark-bench-{Guid.NewGuid()}";
        _dbOptions = new DbContextOptionsBuilder<SamparkDbContext>()
            .UseInMemoryDatabase(_databaseName)
            .Options;

        using var seedContext = new SamparkDbContext(_dbOptions);
        var now = DateTime.UtcNow;
        var persons = Enumerable.Range(1, PersonCount)
            .Select(i => new Person
            {
                BapsId = $"BAPS-{i:D6}",
                FirstName = "FirstName",
                LastName = $"LastName-{i}",
                EmailMasked = $"person{i}@example.org",
                CellphoneMasked = "(000) 000-0000",
                City = "Ahmedabad",
                State = "GJ",
                CountryName = "India",
                IsActive = i % 2 == 0,
                CreatedAt = now,
                UpdatedAt = now
            })
            .ToList();

        seedContext.Persons.AddRange(persons);
        seedContext.SaveChanges();
    }

    [Benchmark]
    public int PersonsQuery_CountAll()
    {
        using var context = new SamparkDbContext(_dbOptions);
        return _testQuery.Persons(context).Count();
    }

    [Benchmark]
    public int PersonsQuery_ToList()
    {
        using var context = new SamparkDbContext(_dbOptions);
        return _testQuery.Persons(context).ToList().Count;
    }

    [Benchmark]
    public int GenericGetPerson_ToList()
    {
        using var context = new SamparkDbContext(_dbOptions);
        return _genericPersonQuery.Get(context).ToList().Count;
    }

    [Benchmark]
    public int PersonsQuery_ActiveOnly_ToList()
    {
        using var context = new SamparkDbContext(_dbOptions);
        return _testQuery.Persons(context).Where(p => p.IsActive).ToList().Count;
    }
}
