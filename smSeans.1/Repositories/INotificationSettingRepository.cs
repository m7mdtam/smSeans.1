public interface INotificationSettingRepository
{
    Task<NotificationSetting> GetNotificationSettingByIdAsync(int id);
    Task<int> CreateNotificationSettingAsync(NotificationSetting notificationSetting); // returns created ID
    Task<bool> UpdateNotificationSettingAsync(NotificationSetting notificationSetting); // returns success status
    Task<bool> DeleteNotificationSettingAsync(int id); // returns success status
}
