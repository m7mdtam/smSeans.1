using Dapper;
using System.Data;
using System.Threading.Tasks;

public class NotificationSettingRepository : INotificationSettingRepository
{
    private readonly DapperContext _context;

    public NotificationSettingRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<NotificationSetting> GetNotificationSettingByIdAsync(int id)
    {
        var query = "SELECT * FROM NotificationSetting WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<NotificationSetting>(query, new { Id = id });
        }
    }

    public async Task<int> CreateNotificationSettingAsync(NotificationSetting notificationSetting)
    {
        var query = "INSERT INTO NotificationSetting (user_id, email_notifications, sms_notifications, push_notifications) " +
                    "VALUES (@user_id, @email_notifications, @sms_notifications, @push_notifications); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS INT)";

        using (var connection = _context.CreateConnection())
        {
            var id = await connection.QuerySingleAsync<int>(query, notificationSetting);
            return id;
        }
    }

    public async Task<bool> UpdateNotificationSettingAsync(NotificationSetting notificationSetting)
    {
        var query = "UPDATE NotificationSetting SET " +
                    "user_id = @user_id, " +
                    "email_notifications = @email_notifications, " +
                    "sms_notifications = @sms_notifications, " +
                    "push_notifications = @push_notifications " +
                    "WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, notificationSetting);
            return rowsAffected > 0;
        }
    }

    public async Task<bool> DeleteNotificationSettingAsync(int id)
    {
        var query = "DELETE FROM NotificationSetting WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
