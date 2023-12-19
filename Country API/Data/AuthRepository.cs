using Country_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Country_API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _config;
        public AuthRepository(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            User user = new()
            {
                Username = _config.GetSection("Credentials:Username").Value,
                Password = _config.GetSection("Credentials:Password").Value
            };

            if (user.Username != username)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (user.Password != password)
            {
                response.Success = false;
                response.Message = "Wrong Password.";
            }
            else
            {
                response.Data = CreateToken(user);//user.Id.ToString();
            }
            return response;
        }
        private string CreateToken(User user)
        {
            try
            {
                var claims = new List<Claim>
                {
                 new Claim(ClaimTypes.Name , user.Username)
                };
                var appSettingsToken = _config.GetSection("AppSettings:Token").Value;
                if (appSettingsToken is null)
                {
                    throw new Exception("AppSettings is null");
                }
                SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));
                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
