using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIG_VETERINARIA.Dtos.User
{
    public class UserListResposeDto
    {
        public int id { set; get; }
        public string username { set; get; }
        public string password { set; get; }
        public int role_id { set; get; }
    }
}
