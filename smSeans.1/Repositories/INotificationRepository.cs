using System.Threading.Tasks;

public interface INotificationRepository
{
    // Retrieve a Notification record by its ID
    Task<Notification> GetNotificationByIdAsync(int id);

    // Create a new Notification record
    Task<int> CreateNotificationAsync(Notification notification);

    // Update an existing Notification record
    Task<bool> UpdateNotificationAsync(Notification notification);

    // Delete a Notification record by its ID
    Task<bool> DeleteNotificationAsync(int id);
}
