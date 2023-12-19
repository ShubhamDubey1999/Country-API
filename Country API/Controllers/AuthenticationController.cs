using Country_API.Data;
using Country_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Country_API.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthenticationController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(User request)
        {
            var response = await _authRepo.Login(request.Username, request.Password);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
