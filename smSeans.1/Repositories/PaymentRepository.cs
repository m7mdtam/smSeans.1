using Dapper;
using System.Data;
using System.Threading.Tasks;

public class PaymentRepository : IPaymentRepository
{
    private readonly DapperContext _context;

    public PaymentRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Payment> GetPaymentByIdAsync(int id)
    {
        const string query = "SELECT * FROM Payments WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<Payment>(query, new { Id = id });
        }
    }

    public async Task<int> CreatePaymentAsync(Payment payment)
    {
        const string query = @"
            INSERT INTO Payments (user_id, amount, payment_date, method)
            VALUES (@user_id, @amount, @payment_date, @method);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleAsync<int>(query, payment);
        }
    }

    public async Task<bool> UpdatePaymentAsync(Payment payment)
    {
        const string query = @"
            UPDATE Payments
            SET user_id = @user_id, amount = @amount, payment_date = @payment_date, method = @method
            WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, payment);
            return rowsAffected > 0;
        }
    }

    public async Task<bool> DeletePaymentAsync(int id)
    {
        const string query = "DELETE FROM Payments WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
