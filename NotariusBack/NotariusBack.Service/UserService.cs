using Microsoft.AspNetCore.Http;
using NotariusBack.Repository;
using NotariusBack.Repository.Entity;
using NotariusBack.Repository.Entity.Enums;
using NotariusBack.Service.ModelDto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace NotariusBack.Service
{
    public class UserService
    {
        UserRepository repository;

        public UserService()
        {
            repository = new UserRepository();
        }

        public async Task Register(UserDto model)
        {
            if ((await repository.Get(model.Name)) == null)
            {
                if (model.Password.Length < 4)
                {
                    throw new ArgumentException();
                }
                SHA512 sha = SHA512.Create();
                string hash = Encoding.UTF8.GetString(sha.ComputeHash(Encoding.UTF8.GetBytes(model.Password)));
                User user = new User() { Name = model.Name, Password = hash, Role = model.Role };
                await repository.Register(user);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public async Task<int?> Login(LoginDto model)
        {
            User user = await repository.Get(model.Name);
            if (user != null)
            {
                SHA512 sha = SHA512.Create();
                string hash = Encoding.UTF8.GetString(sha.ComputeHash(Encoding.UTF8.GetBytes(model.Password)));
                if (user.Password == hash)
                {
                    return await repository.Login(model.Name);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public async Task<UserRoleEnum?> GetRole(int id)
        {
            return await repository.GetRole(id);
        }

        public int IsValidJWT(HttpRequest request)
        {
            if (request.IsHttps)
            {
                IHeaderDictionary header = request.Headers;
                string s = header["Authorization"];
                if (s != null)
                {
                    JwtSecurityTokenHandler jwt = new JwtSecurityTokenHandler();
                    if (s.Length > 7 && jwt.CanReadToken(s.Substring(7)))
                    {
                        JwtSecurityToken token = jwt.ReadJwtToken((s).Substring(7));
                        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ServerInfo.Secret));
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
                        var claims = new List<Claim>()
                        {
                            new Claim("jti", token.Id)
                        };
                        JwtSecurityToken validtoken = new JwtSecurityToken(token.Issuer, token.Audiences.First(), claims, token.ValidFrom, token.ValidTo, credentials);
                        validtoken = (new JwtSecurityTokenHandler()).ReadJwtToken(new JwtSecurityTokenHandler().WriteToken(validtoken));
                        if (token.Issuer == ServerInfo.Issuer && token.Audiences.First() == ServerInfo.Audience && token.RawSignature == validtoken.RawSignature && token.ValidTo < DateTime.Now)
                        {
                            return Convert.ToInt32(token.Id);
                        }
                    }
                }
            }
            return -1;
        }

        public async Task<bool> IsAccess(HttpRequest request, UserRoleEnum?[] userRole) 
        {
            int id = IsValidJWT(request);
            return id == -1 ? false : userRole.ToList().Contains(await GetRole(id));
        }

        public static class ServerInfo
        {
            public static readonly string Issuer = "Igor";

            public static readonly string Audience = "Fedor";

            public static readonly int TokenLifetime = 3600;

            public static string Secret;

            static ServerInfo()
            {
                FileStream file = new FileStream("Secret.TTST", FileMode.Open);
                StreamReader reader = new StreamReader(file);
                Secret = reader.ReadToEnd();
                reader.Close();
                file.Close();
            }
        }
    }
}