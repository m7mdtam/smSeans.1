using Dapper;
using System.Data;
using System.Threading.Tasks;

public class TaskRepository : ITaskRepository
{
    private readonly DapperContext _context;

    public TaskRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Task> GetTaskByIdAsync(int id)
    {
        var query = "SELECT * FROM Tasks WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<Task>(query, new { Id = id });
        }
    }

    public async Task<int> CreateTaskAsync(Task task)
    {
        var query = "INSERT INTO Tasks (user_id, task_description, due_date, status, session_id, assigned_by, title) " +
                    "VALUES (@user_id, @task_description, @due_date, @status, @session_id, @assigned_by, @title); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS INT)";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleAsync<int>(query, task);
        }
    }

    public async Task<bool> UpdateTaskAsync(Task task)
    {
        var query = "UPDATE Tasks SET user_id = @user_id, task_description = @task_description, due_date = @due_date, " +
                    "status = @status, session_id = @session_id, assigned_by = @assigned_by, title = @title " +
                    "WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, task);
            return rowsAffected > 0;
        }
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var query = "DELETE FROM Tasks WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
