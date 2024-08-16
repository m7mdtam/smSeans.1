using Dapper;
using System.Data;
using System.Threading.Tasks;

public class PromotionRepository : IPromotionRepository
{
    private readonly DapperContext _context;

    public PromotionRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Promotion> GetPromotionByIdAsync(int id)
    {
        var query = "SELECT * FROM Promotions WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<Promotion>(query, new { Id = id });
        }
    }

    public async Task<int> CreatePromotionAsync(Promotion promotion)
    {
        var query = "INSERT INTO Promotions (title, description, start_date, end_date, user_id) " +
                    "VALUES (@title, @description, @start_date, @end_date, @user_id); " +
                    "SELECT CAST(SCOPE_IDENTITY() AS INT)";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleAsync<int>(query, promotion);
        }
    }

    public async Task<bool> UpdatePromotionAsync(Promotion promotion)
    {
        var query = "UPDATE Promotions SET title = @title, description = @description, " +
                    "start_date = @start_date, end_date = @end_date, user_id = @user_id " +
                    "WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var affectedRows = await connection.ExecuteAsync(query, promotion);
            return affectedRows > 0;
        }
    }

    public async Task<bool> DeletePromotionAsync(int id)
    {
        var query = "DELETE FROM Promotions WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
            return affectedRows > 0;
        }
    }
}
