using Dapper;
using System.Data;
using System.Threading.Tasks;

public class SessionRepository : ISessionRepository
{
    private readonly DapperContext _context;

    public SessionRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Session> GetSessionByIdAsync(int id)
    {
        var query = "SELECT * FROM Sessions WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<Session>(query, new { Id = id });
        }
    }

    public async Task<int> CreateSessionAsync(Session session)
    {
        var query = "INSERT INTO Sessions (user_id, session_date, credits_used, trainer_id, isCompleted, PlannedDuration, ActualDuration) " +
                    "VALUES (@user_id, @session_date, @credits_used, @trainer_id, @isCompleted, @PlannedDuration, @ActualDuration); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS INT)";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleAsync<int>(query, session);
        }
    }

    public async Task<bool> UpdateSessionAsync(Session session)
    {
        var query = "UPDATE Sessions SET user_id = @user_id, session_date = @session_date, credits_used = @credits_used, " +
                    "trainer_id = @trainer_id, isCompleted = @isCompleted, PlannedDuration = @PlannedDuration, ActualDuration = @ActualDuration " +
                    "WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, session);
            return rowsAffected > 0;
        }
    }

    public async Task<bool> DeleteSessionAsync(int id)
    {
        var query = "DELETE FROM Sessions WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
