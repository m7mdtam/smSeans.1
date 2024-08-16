using System.Threading.Tasks;

public interface IAuthService
{
    Task<string> AuthenticateAsync(string email, string password);
}
