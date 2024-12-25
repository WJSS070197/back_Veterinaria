using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIG_VETERINARIA.Abstrations.Repository;
using SIG_VETERINARIA.Abstrations.Services;
using SIG_VETERINARIA.Dtos.Auth;
using SIG_VETERINARIA.Dtos.Common;
using SIG_VETERINARIA.Dtos.User;
using SIG_VETERINARIA.Repository.User;

namespace SIG_VETERINARIA.Services.User
{
    public class UserServices:IUserServices
    {
        private IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultDto<int>> Create(UserCreateRequestDto request)
        {
            return await _userRepository.Create(request);
        }

        public async Task<ResultDto<int>> Delete(DeleteDto request)
        {
           return await _userRepository.Delete(request);
        }

        public async Task<ResultDto<UserListResposeDto>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<AuthResponseDto> Login(LoginRequestDto request)
        {
          return await _userRepository.Login(request);
        }
    }
}
