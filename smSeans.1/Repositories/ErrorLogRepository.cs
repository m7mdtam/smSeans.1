using Dapper;
using System.Data;
using System.Threading.Tasks;

public class ErrorLogRepository : IErrorLogRepository
{
    private readonly DapperContext _context;

    public ErrorLogRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<ErrorLog> GetErrorLogByIdAsync(int id)
    {
        var query = "SELECT * FROM ErrorLog WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<ErrorLog>(query, new { Id = id });
        }
    }

    public async Task<int> CreateErrorLogAsync(ErrorLog errorLog)
    {
        var query = "INSERT INTO ErrorLog (error_message, stack_trace, created_at, user_id) " +
                    "VALUES (@error_message, @stack_trace, @created_at, @user_id)";

        using (var connection = _context.CreateConnection())
        {
            // Execute the query and return the number of rows affected
            var rowsAffected = await connection.ExecuteAsync(query, errorLog);
            return rowsAffected;
        }
    }

    public async Task<bool> UpdateErrorLogAsync(ErrorLog errorLog)
    {
        var query = "UPDATE ErrorLog SET error_message = @error_message, stack_trace = @stack_trace, created_at = @created_at, " +
                    "user_id = @user_id WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, errorLog);
            return rowsAffected > 0; // Return true if rows were affected
        }
    }

    public async Task<bool> DeleteErrorLogAsync(int id)
    {
        var query = "DELETE FROM ErrorLog WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0; // Return true if rows were affected
        }
    }
}
