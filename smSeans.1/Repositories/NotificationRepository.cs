using Dapper;
using System.Data;
using System.Threading.Tasks;

public class NotificationRepository : INotificationRepository
{
    private readonly DapperContext _context;

    public NotificationRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Notification> GetNotificationByIdAsync(int id)
    {
        var query = "SELECT * FROM Notification WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<Notification>(query, new { Id = id });
        }
    }

    public async Task<int> CreateNotificationAsync(Notification notification)
    {
        var query = "INSERT INTO Notification (user_id, message, read_status, created_at) " +
                    "VALUES (@user_id, @message, @read_status, @created_at)";

        using (var connection = _context.CreateConnection())
        {
            // Execute the query and return the number of rows affected
            var rowsAffected = await connection.ExecuteAsync(query, notification);
            return rowsAffected;
        }
    }

    public async Task<bool> UpdateNotificationAsync(Notification notification)
    {
        var query = "UPDATE Notification SET user_id = @user_id, message = @message, read_status = @read_status, " +
                    "created_at = @created_at WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, notification);
            return rowsAffected > 0; // Return true if rows were affected
        }
    }

    public async Task<bool> DeleteNotificationAsync(int id)
    {
        var query = "DELETE FROM Notification WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0; // Return true if rows were affected
        }
    }
}
