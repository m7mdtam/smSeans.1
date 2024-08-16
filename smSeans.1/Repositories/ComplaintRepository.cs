using Dapper;
using System.Data;
using System.Threading.Tasks;

public class ComplaintRepository : IComplaintRepository
{
    private readonly DapperContext _context;

    public ComplaintRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Complaints> GetComplaintByIdAsync(int id)
    {
        var query = "SELECT * FROM Complaints WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<Complaints>(query, new { Id = id });
        }
    }

    public async Task<int> CreateComplaintAsync(Complaints complaint)
    {
        var query = "INSERT INTO Complaints (ComplaintType, user_id) VALUES (@ComplaintType, @user_id)";

        using (var connection = _context.CreateConnection())
        {
            // Execute the query and get the number of rows affected
            var rowsAffected = await connection.ExecuteAsync(query, complaint);
            return rowsAffected; // Return the number of rows affected
        }
    }

    public async Task<bool> UpdateComplaintAsync(Complaints complaint)
    {
        var query = "UPDATE Complaints SET ComplaintType = @ComplaintType, user_id = @user_id WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, complaint);
            return rowsAffected > 0; // Return true if any rows were affected
        }
    }

    public async Task<bool> DeleteComplaintAsync(int id)
    {
        var query = "DELETE FROM Complaints WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0; // Return true if any rows were affected
        }
    }

   
}
