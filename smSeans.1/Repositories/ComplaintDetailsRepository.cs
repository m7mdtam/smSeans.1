using Dapper;
using System.Data;
using System.Threading.Tasks;

public class ComplaintDetailsRepository : IComplaintDetailsRepository
{
    private readonly DapperContext _context;

    public ComplaintDetailsRepository(DapperContext context)
    {
        _context = context;
    }

    // Retrieve ComplaintDetails by ID
    public async Task<ComplaintDetails> GetComplaintDetailsByIdAsync(int id)
    {
        var query = "SELECT * FROM ComplaintDetails WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<ComplaintDetails>(query, new { Id = id });
        }
    }

    // Create a new ComplaintDetails record and return the number of rows affected
    public async Task<int> CreateComplaintDetailsAsync(ComplaintDetails complaintDetails)
    {
        var query = "INSERT INTO ComplaintDetails (ComplaintId, ComplaintExplanation, IsImportant, IsArchived, Status, CreatedAt, Solution, timeToBeSolved) " +
                    "VALUES (@ComplaintId, @ComplaintExplanation, @IsImportant, @IsArchived, @Status, @CreatedAt, @Solution, @timeToBeSolved)";

        using (var connection = _context.CreateConnection())
        {
            return await connection.ExecuteAsync(query, new
            {
                complaintDetails.complaintId,
                complaintDetails.complaintExplanation,
                complaintDetails.isImportant,
                complaintDetails.isArchived,
                complaintDetails.status,
                complaintDetails.CreatedAt,
                complaintDetails.solution,
                complaintDetails.timeToBeSolved
            });
        }
    }

    // Update an existing ComplaintDetails record and return true if successful
    public async Task<bool> UpdateComplaintDetailsAsync(ComplaintDetails complaintDetails)
    {
        var query = "UPDATE ComplaintDetails SET ComplaintId = @ComplaintId, ComplaintExplanation = @ComplaintExplanation, IsImportant = @IsImportant, " +
                    "IsArchived = @IsArchived, Status = @Status, CreatedAt = @CreatedAt, Solution = @Solution, timeToBeSolved = @timeToBeSolved " +
                    "WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                complaintDetails.complaintId,
                complaintDetails.complaintExplanation,
                complaintDetails.isImportant,
                complaintDetails.isArchived,
                complaintDetails.status,
                complaintDetails.CreatedAt,
                complaintDetails.solution,
                complaintDetails.timeToBeSolved,
                complaintDetails.Id
            });

            return rowsAffected > 0;
        }
    }

    // Delete a ComplaintDetails record and return true if successful
    public async Task<bool> DeleteComplaintDetailsAsync(int id)
    {
        var query = "DELETE FROM ComplaintDetails WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
