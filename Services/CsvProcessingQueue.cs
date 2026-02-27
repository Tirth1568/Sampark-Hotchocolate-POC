using System.Threading.Channels;

namespace Sampark.Services;

public record CsvProcessingJob(string FileName, string Content);

public interface ICsvProcessingQueue
{
    ValueTask QueueAsync(CsvProcessingJob job, CancellationToken cancellationToken);
    ValueTask<CsvProcessingJob> DequeueAsync(CancellationToken cancellationToken);
}

public class CsvProcessingQueue : ICsvProcessingQueue
{
    private readonly Channel<CsvProcessingJob> _channel = Channel.CreateUnbounded<CsvProcessingJob>();

    public ValueTask QueueAsync(CsvProcessingJob job, CancellationToken cancellationToken)
        => _channel.Writer.WriteAsync(job, cancellationToken);

    public ValueTask<CsvProcessingJob> DequeueAsync(CancellationToken cancellationToken)
        => _channel.Reader.ReadAsync(cancellationToken);
}
