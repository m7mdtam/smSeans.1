using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
[Authorize]

[Route("api/[controller]")]
[ApiController]
public class ErrorLogController : ControllerBase
{
    private readonly IErrorLogRepository _errorLogRepository;

    public ErrorLogController(IErrorLogRepository errorLogRepository)
    {
        _errorLogRepository = errorLogRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetErrorLogById(int id)
    {
        var errorLog = await _errorLogRepository.GetErrorLogByIdAsync(id);
        if (errorLog == null)
            return NotFound();

        return Ok(errorLog);
    }

    [HttpPost]
    public async Task<IActionResult> CreateErrorLog([FromBody] ErrorLogCreateDto errorLogDto)
    {
        if (errorLogDto == null)
        {
            return BadRequest("ErrorLog data is required.");
        }

        var errorLog = new ErrorLog
        {
            error_message = errorLogDto.error_message,
            stack_trace = errorLogDto.stack_trace,
            created_at = DateTime.Now
        };

        await _errorLogRepository.CreateErrorLogAsync(errorLog);

        return CreatedAtAction(nameof(GetErrorLogById), new { id = errorLog.Id }, errorLog);
    }
}
