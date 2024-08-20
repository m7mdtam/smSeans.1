using Dapper;
using System.Data;
using System.Threading.Tasks;

public class UserRepository : IUserRepository
{
    private readonly DapperContext _context;

    public UserRepository(DapperContext context)
    {
        _context = context;
    }

    // Get a user by their ID
    public async Task<User> GetUserByIdAsync(int id)
    {
        var query = "SELECT * FROM [USER] WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<User>(query, new { Id = id });
        }
    }

    // Get a user by their email
    public async Task<User> GetUserByEmailAsync(string email)
    {
        var query = "SELECT * FROM [USER] WHERE email = @email";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<User>(query, new { email });
        }
    }

    // Create a new user
    public async Task<int> CreateUserAsync(User user)
    {
        if (string.IsNullOrEmpty(user.password))
        {
            throw new ArgumentException("Password cannot be null or empty", nameof(user.password));
        }

        var query = @"INSERT INTO [USER] (username, email, password, role_name, google_id, created_at, updated_at, last_login_date, session_count, phone_number, first_name, last_name) 
                  VALUES (@username, @email, @password, @role_name, @google_id, @created_at, @updated_at, @last_login_date, @session_count, @phone_number, @first_name, @last_name);
                  SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleAsync<int>(query, new
            {
                user.username,
                user.email,
                user.password,
                user.google_id,
                user.role_name,
                user.created_at,
                user.updated_at,
                last_login_date = user.last_login_date ?? (object)DBNull.Value,
                session_count = user.session_count ?? (object)DBNull.Value,
                user.phone_number,
                user.first_name, user.last_name,
            });
        }
   
    }



    // Update an existing user
    public async Task<bool> UpdateUserAsync(User user)
    {
        if (user == null)
        {
            return false;
        }

        var query = @"UPDATE [USER] 
                  SET username = @username, 
                      email = @email, 
                      password = @password, 
                      role_name = @role_name, 
                      updated_at = @updated_at,
                      last_login_date = @last_login_date,
                      session_count = @session_count 
                  WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                username = user.username,
                email = user.email,
                password = user.password,
                role_name = user.role_name,
                updated_at = user.updated_at,
                last_login_date = user.last_login_date ?? (object)DBNull.Value,
                session_count = user.session_count ?? (object)DBNull.Value,
                Id = user.Id
            });

            return rowsAffected > 0;
        }
    }


    // Delete a user by their ID
    public async Task<bool> DeleteUserAsync(int id)
    {
        var query = "DELETE FROM [USER] WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }

    
}
