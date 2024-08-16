using System.Threading.Tasks;

public interface IErrorLogRepository
{
    // Retrieve an ErrorLog record by its ID
    Task<ErrorLog> GetErrorLogByIdAsync(int id);

    // Create a new ErrorLog record
    Task<int> CreateErrorLogAsync(ErrorLog errorLog);

    // Update an existing ErrorLog record
    Task<bool> UpdateErrorLogAsync(ErrorLog errorLog);

    // Delete an ErrorLog record by its ID
    Task<bool> DeleteErrorLogAsync(int id);
}
