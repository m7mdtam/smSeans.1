using System.Threading.Tasks;
using Dapper;
using System.Data;
using NPOI.SS.Formula.Functions;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<string> AuthenticateAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null || user.password != password)
        {
            return null; // Invalid credentials
        }

        // Update login details
        await UpdateUserLoginDetailsAsync(user.Id);

        // Generate JWT token or any other token
        var token = _tokenService.GenerateToken(user);
        return token;
    }

    public async Task<T> UpdateUserLoginDetailsAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user != null) { 



        // Increment session count and update last login date
        user.session_count = (user.session_count ?? 0) + 1;
        user.last_login_date = DateTime.UtcNow;

        // Log the changes for debugging
        Console.WriteLine($"Updating user {userId}: Session Count = {user.session_count}, Last Login Date = {user.last_login_date}");

        // Save the updated user details
        await _userRepository.UpdateUserAsync(user);

    }
        return null;
        
        
            // Log if user was not found
            Console.WriteLine($"User with ID {userId} not found.");
        
    }


}
