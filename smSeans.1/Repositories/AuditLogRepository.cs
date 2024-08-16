using Dapper;
using System.Data;
using System.Threading.Tasks;

public class AuditLogRepository : IAuditLogRepository
{
    private readonly DapperContext _context;

    public AuditLogRepository(DapperContext context)
    {
        _context = context;
    }

    // Get an AuditLog by ID
    public async Task<AuditLog> GetAuditLogByIdAsync(int id)
    {
        var query = "SELECT * FROM AuditLog WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<AuditLog>(query, new { Id = id });
        }
    }

    // Create a new AuditLog and return the number of rows affected
    public async Task<int> CreateAuditLogAsync(AuditLog auditLog)
    {
        var query = "INSERT INTO AuditLog (user_id, action, details, created_at) " +
                    "VALUES (@user_id, @action, @details, @created_at)";

        using (var connection = _context.CreateConnection())
        {
            return await connection.ExecuteAsync(query, new
            {
                auditLog.user_id,
                auditLog.action,
                auditLog.details,
                auditLog.created_at
            });
        }
    }

    // Update an existing AuditLog and return true if successful
    public async Task<bool> UpdateAuditLogAsync(AuditLog auditLog)
    {
        var query = "UPDATE AuditLog SET user_id = @user_id, action = @action, details = @details, " +
                    "created_at = @created_at WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                auditLog.user_id,
                auditLog.action,
                auditLog.details,
                auditLog.created_at,
                auditLog.Id
            });

            return rowsAffected > 0;
        }
    }

    // Delete an AuditLog and return true if successful
    public async Task<bool> DeleteAuditLogAsync(int id)
    {
        var query = "DELETE FROM AuditLog WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }

    }

