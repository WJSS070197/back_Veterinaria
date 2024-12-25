using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIG_VETERINARIA.Abstrations.Applications;
using SIG_VETERINARIA.Abstrations.Services;
using SIG_VETERINARIA.Dtos.Auth;
using SIG_VETERINARIA.Dtos.Common;
using SIG_VETERINARIA.Dtos.User;

namespace SIG_VETERINARIA.Applications.User
{
    public class UserApplications:IUserApplications
    {
        private IUserServices _IUserServices;

        public UserApplications(IUserServices iUserServices)
        {
            _IUserServices = iUserServices;
        }

        public async Task<ResultDto<int>> Create(UserCreateRequestDto request)
        {
           return await _IUserServices.Create(request);
        }

        public async Task<ResultDto<int>> Delete(DeleteDto request)
        {
            return await _IUserServices.Delete(request);
        }

        public async Task<ResultDto<UserListResposeDto>> GetAll()
        {
            return await _IUserServices.GetAll();
        }

        public async Task<AuthResponseDto> Login(LoginRequestDto request)
        {
            return await _IUserServices.Login(request);
        }
    }
}
