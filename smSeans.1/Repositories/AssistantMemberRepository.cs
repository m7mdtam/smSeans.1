using Dapper;
using System.Data;
using System.Threading.Tasks;

public class AssistantMemberRepository : IAssistantMemberRepository
{
    private readonly DapperContext _context;

    public AssistantMemberRepository(DapperContext context)
    {
        _context = context;
    }

    // Get an assistant member by ID
    public async Task<AssistantMember> GetAssistantMemberByIdAsync(int id)
    {
        var query = "SELECT * FROM AssistantMember WHERE assignment_id = @assignment_id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<AssistantMember>(query, new { assignment_id = id });
        }
    }

    // Create a new assistant member and return the number of rows affected
    public async Task<int> CreateAssistantMemberAsync(AssistantMember assistantMember)
    {
        var query = @"INSERT INTO AssistantMember (assistant_id, member_id, assigned_at) 
                      VALUES (@assistant_id, @member_id, @assigned_at)";

        using (var connection = _context.CreateConnection())
        {
            return await connection.ExecuteAsync(query, new
            {
                assistantMember.assistant_id,
                assistantMember.member_id,
                assistantMember.assigned_at
            });
        }
    }

    // Update an existing assistant member and return true if successful
    public async Task<bool> UpdateAssistantMemberAsync(AssistantMember assistantMember)
    {
        var query = @"UPDATE AssistantMember SET assistant_id = @assistant_id, 
                      member_id = @member_id, assigned_at = @assigned_at 
                      WHERE assignment_id = @assignment_id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                assistantMember.assistant_id,
                assistantMember.member_id,
                assistantMember.assigned_at,
                assistantMember.assignment_id
            });

            return rowsAffected > 0;
        }
    }

    // Delete an assistant member and return true if successful
    public async Task<bool> DeleteAssistantMemberAsync(int id)
    {
        var query = "DELETE FROM AssistantMember WHERE assignment_id = @assignment_id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new { assignment_id = id });
            return rowsAffected > 0;
        }
    }

}