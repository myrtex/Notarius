using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NotariusBack.Repository;
using NotariusBack.Repository.Entity;
using NotariusBack.Repository.Entity.Enums;
using NotariusBack.Service;
using NotariusBack.Service.ModelDto;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
            int Id = userService.IsValidJWT(Request);
            if (Id != -1)
            {
                return Ok(new { token = GenerateJWT(Id) });
            }
            return Unauthorized();
        }

        private string GenerateJWT(int id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(UserService.ServerInfo.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            var claims = new List<Claim>()
            {
                new Claim("jti", id.ToString())
            };
            JwtSecurityToken token = new JwtSecurityToken(UserService.ServerInfo.Issuer, UserService.ServerInfo.Audience, claims, DateTime.Now, DateTime.Now.AddSeconds(UserService.ServerInfo.TokenLifetime), credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}