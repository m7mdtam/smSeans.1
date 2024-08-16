using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class SessionController : ControllerBase
{
    private readonly ISessionRepository _sessionRepository;

    public SessionController(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSessionById(int id)
    {
        var session = await _sessionRepository.GetSessionByIdAsync(id);
        if (session == null)
            return NotFound();

        return Ok(session);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSession([FromBody] SessionCreateDto sessionDto)
    {
        if (sessionDto == null)
        {
            return BadRequest("Session data is required.");
        }

        var session = new Session
        {
            user_id = sessionDto.user_id,
            session_date = sessionDto.session_date,
            credits_used = sessionDto.credits_used,
            trainer_id = sessionDto.trainer_id,
            isCompleted = sessionDto.isCompleted,
            PlannedDuration = sessionDto.PlannedDuration,
            ActualDuration = sessionDto.ActualDuration
        };

        var createdId = await _sessionRepository.CreateSessionAsync(session);

        return CreatedAtAction(nameof(GetSessionById), new { id = createdId }, session);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSession(int id, [FromBody] SessionUpdateDto sessionDto)
    {
        if (sessionDto == null || id <= 0)
        {
            return BadRequest("Invalid session data.");
        }

        var session = await _sessionRepository.GetSessionByIdAsync(id);
        if (session == null)
        {
            return NotFound();
        }

        session.user_id = sessionDto.user_id;
        session.session_date = sessionDto.session_date;
        session.credits_used = sessionDto.credits_used;
        session.trainer_id = sessionDto.trainer_id;
        session.isCompleted = sessionDto.isCompleted;
        session.PlannedDuration = sessionDto.PlannedDuration;
        session.ActualDuration = sessionDto.ActualDuration;

        await _sessionRepository.UpdateSessionAsync(session);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSession(int id)
    {
        var session = await _sessionRepository.GetSessionByIdAsync(id);
        if (session == null)
        {
            return NotFound();
        }

        await _sessionRepository.DeleteSessionAsync(id);

        return NoContent();
    }
}
