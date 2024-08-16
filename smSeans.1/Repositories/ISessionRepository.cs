public interface ISessionRepository
{
    Task<Session> GetSessionByIdAsync(int id);
    Task<int> CreateSessionAsync(Session session); // Returns the created session's ID
    Task<bool> UpdateSessionAsync(Session session); // Returns success status
    Task<bool> DeleteSessionAsync(int id); // Returns success status
}
