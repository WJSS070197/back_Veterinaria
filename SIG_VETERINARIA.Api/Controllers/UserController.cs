using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIG_VETERINARIA.Abstrations.Applications;
using SIG_VETERINARIA.Dtos.Common;
using SIG_VETERINARIA.Dtos.User;

namespace SIG_VETERINARIA.Api.Controllers
{

    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserApplications _userApplication;

        public UserController(IUserApplications userApplication)
        {
            _userApplication = userApplication;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAll()
        {

            try
            {
                var res = await _userApplication.GetAll();
                return Ok(res);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create(UserCreateRequestDto request)
        {
            try
            {
                var result=await _userApplication.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> Delete(DeleteDto request)
        {
            try
            {
                var result=await _userApplication.Delete(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
