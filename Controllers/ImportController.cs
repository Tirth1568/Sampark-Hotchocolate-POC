using Microsoft.AspNetCore.Mvc;
using Sampark.Controllers.Requests;
using Sampark.Services;

namespace Sampark.Controllers;

[ApiController]
[Route("api/import")]
public class ImportController(ICsvProcessingQueue queue) : ControllerBase
{
    [HttpPost("csv")]
    [Consumes("multipart/form-data")]
    [RequestSizeLimit(10_000_000)]
    public async Task<IActionResult> UploadCsv([FromForm] CsvUploadRequest request, CancellationToken cancellationToken)
    {
        if (request.File is null || request.File.Length == 0)
        {
            return BadRequest("File is required");
        }

        if (!request.File.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("Only .csv files are supported");
        }

        using var reader = new StreamReader(request.File.OpenReadStream());
        var content = await reader.ReadToEndAsync(cancellationToken);

        await queue.QueueAsync(new CsvProcessingJob(request.File.FileName, content), cancellationToken);

        return Accepted(new
        {
            Message = "CSV accepted for background processing",
            request.File.FileName,
            request.File.Length
        });
    }
}
