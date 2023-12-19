using Country_API.Models;

namespace Country_API.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<string>> Login(string username, string password);
    }
}
