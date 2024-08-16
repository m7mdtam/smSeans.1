using System;
using System.Threading.Tasks;

public interface IComplaintRepository
{
    Task<Complaints> GetComplaintByIdAsync(int id);
    Task<int> CreateComplaintAsync(Complaints complaint);
    Task<bool> UpdateComplaintAsync(Complaints complaint);
    Task<bool> DeleteComplaintAsync(int id);
}
