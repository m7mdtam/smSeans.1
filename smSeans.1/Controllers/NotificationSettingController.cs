using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class NotificationSettingController : ControllerBase
{
    private readonly INotificationSettingRepository _notificationSettingRepository;

    public NotificationSettingController(INotificationSettingRepository notificationSettingRepository)
    {
        _notificationSettingRepository = notificationSettingRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetNotificationSettingById(int id)
    {
        var notificationSetting = await _notificationSettingRepository.GetNotificationSettingByIdAsync(id);
        if (notificationSetting == null)
            return NotFound();

        return Ok(notificationSetting);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNotificationSetting([FromBody] NotificationSettingCreateDto notificationSettingDto)
    {
        if (notificationSettingDto == null)
        {
            return BadRequest("NotificationSetting data is required.");
        }

        var notificationSetting = new NotificationSetting
        {
            user_id = notificationSettingDto.user_id,
            email_notifications = notificationSettingDto.email_notifications,
            sms_notifications = notificationSettingDto.sms_notifications
        };

        await _notificationSettingRepository.CreateNotificationSettingAsync(notificationSetting);

        return CreatedAtAction(nameof(GetNotificationSettingById), new { id = notificationSetting.Id }, notificationSetting);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNotificationSetting(int id, [FromBody] NotificationSettingUpdateDto notificationSettingDto)
    {
        if (notificationSettingDto == null || id <= 0)
        {
            return BadRequest("Invalid NotificationSetting data.");
        }

        var notificationSetting = await _notificationSettingRepository.GetNotificationSettingByIdAsync(id);
        if (notificationSetting == null)
        {
            return NotFound();
        }

        notificationSetting.email_notifications = notificationSettingDto.email_notifications;
        notificationSetting.sms_notifications = notificationSettingDto.sms_notifications;

        await _notificationSettingRepository.UpdateNotificationSettingAsync(notificationSetting);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotificationSetting(int id)
    {
        var notificationSetting = await _notificationSettingRepository.GetNotificationSettingByIdAsync(id);
        if (notificationSetting == null)
        {
            return NotFound();
        }

        await _notificationSettingRepository.DeleteNotificationSettingAsync(id);

        return NoContent();
    }
}
