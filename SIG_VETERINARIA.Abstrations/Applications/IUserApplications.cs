using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIG_VETERINARIA.Dtos.Auth;
using SIG_VETERINARIA.Dtos.Common;
using SIG_VETERINARIA.Dtos.User;

namespace SIG_VETERINARIA.Abstrations.Applications
{
    public interface IUserApplications
    {
        public Task<ResultDto<UserListResposeDto>> GetAll();
        public Task<ResultDto<int>> Create(UserCreateRequestDto request);
        public Task<ResultDto<int>> Delete(DeleteDto request);
        public Task<AuthResponseDto> Login(LoginRequestDto request);
    }
}
