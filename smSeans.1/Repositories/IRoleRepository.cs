public interface IRoleRepository
{
    Task<Role> GetRoleByIdAsync(int id);
    Task<int> CreateRoleAsync(Role role); // Returns the created role's ID
    Task<bool> UpdateRoleAsync(Role role); // Returns success status
    Task<bool> DeleteRoleAsync(int id); // Returns success status
}
