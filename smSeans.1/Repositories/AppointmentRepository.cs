using Dapper;
using System.Data;
using System.Threading.Tasks;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly DapperContext _context;

    public AppointmentRepository(DapperContext context)
    {
        _context = context;
    }

    // Get an appointment by ID
    public async Task<Appointment> GetAppointmentByIdAsync(int id)
    {
        var query = "SELECT * FROM Appointment WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<Appointment>(query, new { Id = id });
        }
    }

    // Create a new appointment and return the number of rows affected
    public async Task<int> CreateAppointmentAsync(Appointment appointment)
    {
        var query = @"INSERT INTO Appointment (member_id, assistant_id, appointment_date, status, created_at) 
                      VALUES (@member_id, @assistant_id, @appointment_date, @status, @created_at)";

        using (var connection = _context.CreateConnection())
        {
            return await connection.ExecuteAsync(query, new
            {
                appointment.member_id,
                appointment.assistant_id,
                appointment.appointment_date,
                appointment.status,
                appointment.created_at
            });
        }
    }

    // Update an existing appointment and return true if successful
    public async Task<bool> UpdateAppointmentAsync(Appointment appointment)
    {
        var query = @"UPDATE Appointment SET member_id = @member_id, assistant_id = @assistant_id, 
                      appointment_date = @appointment_date, status = @status WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new
            {
                appointment.member_id,
                appointment.assistant_id,
                appointment.appointment_date,
                appointment.status,
                appointment.Id
            });

            return rowsAffected > 0;
        }
    }

    // Delete an appointment and return true if successful
    public async Task<bool> DeleteAppointmentAsync(int id)
    {
        var query = "DELETE FROM Appointment WHERE Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }

    
}
