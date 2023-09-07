using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGE.Models;
using SGE.Services.AuthService;

namespace SGE.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Register([FromForm] UserSignupDto model)
        {
            var response = new ApiResponse();
            try
            {
                bool result = await _authService.Register(model);
                if (result)
                {
                    response.Success = true;
                    response.Data = model;
                    return Ok(response);
                }
                throw new Exception("Something went wrong");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.ToString();
                return Ok(response);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ApiResponse>> Login(UserLoginDto model)
        {
            var response = new ApiResponse();
            try
            {
                var result = await _authService.Login(model);
                if (result.Any())
                {
                    response.Success = true;
                    response.Data = result;
                    return Ok(response);
                }
                throw new Exception("User Not Exist");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.ToString();
                return Ok(response);
            }
        }
    }
}

