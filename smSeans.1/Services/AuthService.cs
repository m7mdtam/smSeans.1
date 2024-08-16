using System;
using System.Threading.Tasks;

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
        if (user == null || !VerifyPassword(password, user.password))
            return null;

        return _tokenService.GenerateToken(user);
    }

    private bool VerifyPassword(string storedPassword, string enteredPassword)
    {
        // Implement your password verification logic here
        return storedPassword == enteredPassword;
    }
}
