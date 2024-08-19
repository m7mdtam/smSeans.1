using Dapper;
using System.Data;
using System.Threading.Tasks;

public class RoleRepository : IRoleRepository
{
    private readonly DapperContext _context;

    public RoleRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Role> GetRoleByIdAsync(int id)
    {
        var query = "SELECT * FROM Role WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<Role>(query, new { Id = id });
        }
    }

    public async Task<int> CreateRoleAsync(Role role)
    {
        var query = "INSERT INTO Role (role_name, description) VALUES (@role_name, @description); SELECT CAST(SCOPE_IDENTITY() AS INT)";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleAsync<int>(query, role);
        }
    }

    public async Task<bool> UpdateRoleAsync(Role role)
    {
        var query = "UPDATE Role SET role_name = @role_name, description = @description WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, role);
            return rowsAffected > 0;
        }
    }

    public async Task<bool> DeleteRoleAsync(int id)
    {
        var query = "DELETE FROM Role WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
