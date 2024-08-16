using System.Threading.Tasks;

public interface IComplaintDetailsRepository
{
    // Retrieve a ComplaintDetails record by its ID
    Task<ComplaintDetails> GetComplaintDetailsByIdAsync(int id);

    // Create a new ComplaintDetails record
    Task<int> CreateComplaintDetailsAsync(ComplaintDetails complaintDetails);

    // Update an existing ComplaintDetails record
    Task<bool> UpdateComplaintDetailsAsync(ComplaintDetails complaintDetails);

    // Delete a ComplaintDetails record by its ID
    Task<bool> DeleteComplaintDetailsAsync(int id);
}
