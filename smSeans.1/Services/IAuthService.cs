using System.Threading.Tasks;
using Dapper;
using System.Data;
using NPOI.SS.Formula.Functions;

public interface IAuthService
{
    Task<string> AuthenticateAsync(string email, string password);
    Task<T> UpdateUserLoginDetailsAsync(int userId);
}
