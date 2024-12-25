using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIG_VETERINARIA.Abstrations.Applications;
using SIG_VETERINARIA.Dtos.Auth;

namespace SIG_VETERINARIA.Api.Controllers
{

    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserApplications _userApplications;

        public AuthController(IUserApplications userApplications)
        {
            _userApplications = userApplications;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginRequestDto request)
        {
            try
            {
                var res=await _userApplications.Login(request);
                return Ok(res);
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
    }
}
