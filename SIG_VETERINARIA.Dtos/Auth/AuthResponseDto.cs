using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIG_VETERINARIA.Dtos.User;

namespace SIG_VETERINARIA.Dtos.Auth
{
    public class AuthResponseDto
    {
        public Boolean IsSuccess { get; set; }
        public UserDetailResponseDto User { get; set; }
        public string Token { get; set; }
    }
}
