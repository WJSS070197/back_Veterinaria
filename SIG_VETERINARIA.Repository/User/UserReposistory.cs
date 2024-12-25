using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SIG_VETERINARIA.Abstrations.Repository;
using SIG_VETERINARIA.Dtos.Auth;
using SIG_VETERINARIA.Dtos.Common;
using SIG_VETERINARIA.Dtos.User;

namespace SIG_VETERINARIA.Repository.User
{
    public class UserReposistory : IUserRepository
    {
        private readonly IConfiguration _configuration; 
        private readonly string _connectionString;
        public UserReposistory(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("connection");
        }

        public async Task<ResultDto<int>> Create(UserCreateRequestDto request)
        {
            ResultDto<int> result = new ResultDto<int>();
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_id", request.id);
                    parameters.Add("@p_username", request.username);
                    parameters.Add("@p_password", request.password);
                    parameters.Add("@p_role_id", request.role_id);

                    using (var lectura = await cn.ExecuteReaderAsync("SP_CREATE_USER", parameters, commandType: CommandType.StoredProcedure)) 
                    {
                        while (lectura.Read())
                        {
                            result.Item = Convert.ToInt32(lectura["id"].ToString());
                            result.IsSuccess = Convert.ToInt32(lectura["id"].ToString())>0?true:false;
                            result.Message = Convert.ToInt32(lectura["id"].ToString()) > 0 ? "Informacion guardada correctamente" : "Informacion no se pudo guardar";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.MessegeException = ex.Message;
                result.IsSuccess = false;
            }

            return result;
        }

        public async Task<ResultDto<int>> Delete(DeleteDto request)
        {
            ResultDto<int> result = new ResultDto<int>();
            try
            {
                using (var cn=new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@p_id",request.id);

                    using (var lectura =await cn.ExecuteReaderAsync("SP_DELETE_USER", parameters, commandType: CommandType.StoredProcedure)) 
                    {
                        while (lectura.Read())
                        {
                            result.Item = Convert.ToInt32(lectura["id"].ToString());
                            result.IsSuccess = true;
                            result.Message = "FUE ELIMANDO CORRECTAMENTE";
                        }
                    }
                }

            }
            catch (Exception ex) {
                result.MessegeException = ex.Message;
                result.IsSuccess = false;
            }
            return result;
        }

        public async Task<ResultDto<UserListResposeDto>> GetAll()
        {
            ResultDto<UserListResposeDto> result = new ResultDto<UserListResposeDto>();
            List<UserListResposeDto> list = new List<UserListResposeDto>();

            try
            {
                using (var cn=new SqlConnection(_connectionString))
                {

                    list =(List<UserListResposeDto>) await cn.QueryAsync<UserListResposeDto>("SP_LIST_USERS", null, commandType: CommandType.StoredProcedure);

                }
                result.IsSuccess = list.Count>0?true:false;
                result.Message=list.Count>0?"Informacion encontrada":"No se encontro Informacion";
                result.Data = list;
                return result;
            }
            catch (Exception ex) { 
                
                result.IsSuccess = false;
                result.MessegeException=ex.Message;
            
            }
            return result;
        }

        public async Task<UserDetailResponseDto> GetUserByUsername(string username)
        {
           DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@p_username",username);

            using (var cn=new SqlConnection(_connectionString))
            {
                var query = await cn.QueryAsync<UserDetailResponseDto>("SP_GET_USER_BY_USERNAME", parameters, commandType: CommandType.StoredProcedure);

                if (query.Any())
                {
                    return query.First();
                }
                throw new Exception("Usuario o contraseña incorrecta");
            }

        }
        

        public async Task<UserDetailResponseDto> ValidateUser(LoginRequestDto request)
        {
            UserDetailResponseDto user = await GetUserByUsername(request.username);

            if (user.password == request.password)
            {
                return user;
            }
            throw new Exception("Usuario o contraseña incorrecta");
        }


        public async Task<TokenResponseDto> GenerateToken(UserDetailResponseDto request)
        {
            var key = _configuration.GetSection("JWTSetting:key").Value;
            var keyByte=Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(request.id)));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier,request.username));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier,Convert.ToString(request.role_id)));

            var credentials = new SigningCredentials(new SymmetricSecurityKey(keyByte), SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject=claims,
                Expires=DateTime.UtcNow.AddMinutes(60),
                SigningCredentials=credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(tokenConfig);

            return new TokenResponseDto { Token = token };

        }


        public async Task<AuthResponseDto> Login(LoginRequestDto request)
        {
            UserDetailResponseDto user = await ValidateUser(request);
            var token=await GenerateToken(user);
            return new AuthResponseDto { IsSuccess=true,User = user, Token = token.Token };
        }
    }
}
