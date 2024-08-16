using System.Threading.Tasks;

public interface IPromotionRepository
{
    Task<Promotion> GetPromotionByIdAsync(int id);
    Task<int> CreatePromotionAsync(Promotion promotion); // Returns the created ID
    Task<bool> UpdatePromotionAsync(Promotion promotion); // Returns success status
    Task<bool> DeletePromotionAsync(int id); // Returns success status
}
