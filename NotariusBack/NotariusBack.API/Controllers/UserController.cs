using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotariusBack.Repository.Entity;
using NotariusBack.Service;
using NotariusBack.Service.ModelDto;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace NotariusBack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        UserService userService;

        public UserController()
        {
            userService = new UserService();
        }

        [HttpGet]
        public async Task<ActionResult> Register(UserDto model)
        {
            try
            {
                await userService.Register(model);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult<string>> Login(LoginDto model)
        {
            int? userId;
            try
            {
                userId = await userService.Login(model);
            }
            catch (ArgumentException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            if(userId.HasValue)
            {
                return Ok(GenerateJWT(userId.Value));
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("ResetToken")]
        [HttpGet]
        public async Task<IActionResult> ResetToken()
        {
            int Id = isValidJWT(Request);
            if (Id != -1)
            {
                return Ok(new { token = await GenerateJWT(Id) });
            }
            return Unauthorized();
        }

        private async Task<string> GenerateJWT(int id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ServerInfo.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            var claims = new List<Claim>()
            {
                new Claim("jti", id.ToString())
            };
            JwtSecurityToken token = new JwtSecurityToken(ServerInfo.Issuer, ServerInfo.Audience, claims, DateTime.Now, DateTime.Now.AddSeconds(ServerInfo.TokenLifetime), credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static int isValidJWT(HttpRequest request)
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

        private static class ServerInfo
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