using Microsoft.Extensions.Caching.Memory;

namespace Sampark.Services;

public class CsvProcessingWorker(
    ICsvProcessingQueue queue,
    IMemoryCache cache,
    ILogger<CsvProcessingWorker> logger) : BackgroundService
{
    private const string ProcessedJobsCacheKey = "csv:processed-jobs";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("CSV worker started");

        while (!stoppingToken.IsCancellationRequested)
        {
            var job = await queue.DequeueAsync(stoppingToken);

            var lines = job.Content
                .Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            if (lines.Count == 0)
            {
                logger.LogWarning("Received empty csv for {FileName}", job.FileName);
                continue;
            }

            var headers = lines[0].Split(',').Select(h => h.Trim()).ToArray();
            var rowCount = Math.Max(0, lines.Count - 1);

            logger.LogInformation(
                "Processed CSV {FileName}. Headers: {Headers}. Rows: {RowCount}",
                job.FileName,
                string.Join('|', headers),
                rowCount);

            var processed = cache.Get<List<string>>(ProcessedJobsCacheKey) ?? [];
            processed.Add($"{DateTime.UtcNow:o} - {job.FileName} ({rowCount} rows)");

            cache.Set(ProcessedJobsCacheKey, processed, TimeSpan.FromHours(1));
        }
    }
}
