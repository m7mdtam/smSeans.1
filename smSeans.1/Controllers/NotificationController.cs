using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationController(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetNotificationById(int id)
    {
        var notification = await _notificationRepository.GetNotificationByIdAsync(id);
        if (notification == null)
            return NotFound();

        return Ok(notification);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNotification([FromBody] NotificationCreateDto notificationDto)
    {
        if (notificationDto == null)
        {
            return BadRequest("Notification data is required.");
        }

        var notification = new Notification
        {
            user_id = notificationDto.user_id,
            message = notificationDto.message,
            created_at = DateTime.Now
        };

        await _notificationRepository.CreateNotificationAsync(notification);

        return CreatedAtAction(nameof(GetNotificationById), new { id = notification.Id }, notification);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNotification(int id, [FromBody] NotificationUpdateDto notificationDto)
    {
        if (notificationDto == null || id <= 0)
        {
            return BadRequest("Invalid notification data.");
        }

        var notification = await _notificationRepository.GetNotificationByIdAsync(id);
        if (notification == null)
        {
            return NotFound();
        }

        notification.message = notificationDto.message;
        notification.read_status = notificationDto.read_status;

        await _notificationRepository.UpdateNotificationAsync(notification);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(int id)
    {
        var notification = await _notificationRepository.GetNotificationByIdAsync(id);
        if (notification == null)
        {
            return NotFound();
        }

        await _notificationRepository.DeleteNotificationAsync(id);

        return NoContent();
    }
}
