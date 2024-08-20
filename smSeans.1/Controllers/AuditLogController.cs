using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
[Authorize]

[Route("api/[controller]")]
[ApiController]
public class AuditLogController : ControllerBase
{
    private readonly IAuditLogRepository _auditLogRepository;

    public AuditLogController(IAuditLogRepository auditLogRepository)
    {
        _auditLogRepository = auditLogRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuditLogById(int id)
    {
        var auditLog = await _auditLogRepository.GetAuditLogByIdAsync(id);
        if (auditLog == null)
            return NotFound();

        return Ok(auditLog);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuditLog([FromBody] AuditLogCreateDto auditLogDto)
    {
        if (auditLogDto == null)
        {
            return BadRequest("AuditLog data is required.");
        }

        var auditLog = new AuditLog
        {
            user_id = auditLogDto.user_id,
            action = auditLogDto.action,
            details = auditLogDto.details
        };

        await _auditLogRepository.CreateAuditLogAsync(auditLog);

        return CreatedAtAction(nameof(GetAuditLogById), new { id = auditLog.Id }, auditLog);
    }
}
