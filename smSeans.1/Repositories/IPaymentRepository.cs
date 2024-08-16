public interface IPaymentRepository
{
    Task<Payment> GetPaymentByIdAsync(int id);
    Task<int> CreatePaymentAsync(Payment payment); // Returns the created ID
    Task<bool> UpdatePaymentAsync(Payment payment); // Returns success status
    Task<bool> DeletePaymentAsync(int id); // Returns success status
}
