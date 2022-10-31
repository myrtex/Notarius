using Microsoft.AspNetCore.Mvc;
using NotariusBack.Repository.Entity.Enums;
using NotariusBack.Repository.Entity;
using NotariusBack.Service;
using NotariusBack.Service.ModelDto;

namespace NotariusBack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        ServiceService serviceService;
        UserService userService;

        public ServiceController()
        {
            serviceService = new ServiceService();
            userService = new UserService();
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get(string name)
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Notarius, UserRoleEnum.Administrator }))
            {
                Repository.Entity.Service service;
                try
                {
                    service = await serviceService.Get(name);
                }
                catch (ArgumentException e)
                {
                    return UnprocessableEntity(e.Message);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
                if (service != null)
                {
                    return Ok(service);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        public async Task<ActionResult<string>> UpdatePrice(int id, int? price, double? commission)
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Financer }))
            {
                try
                {
                   await serviceService.UpdatePrice(new ServiceDto() 
                   { Price = price.HasValue ? price.Value : -1, Commission = commission.HasValue ? commission.Value : -1 }, id);
                }
                catch (ArgumentException e)
                {
                    return UnprocessableEntity(e.Message);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> Add(ServiceDto service)
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Notarius }))
            {
                try
                {
                    await serviceService.Add(service);
                }
                catch (ArgumentException e)
                {
                    return UnprocessableEntity(e.Message);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        public async Task<ActionResult<string>> Update(int id, int? price, double? commission, string description)
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Notarius }))
            {
                try
                {
                    await serviceService.Update(new ServiceDto() {Price = price.HasValue ? price.Value : -1, Commission = commission.HasValue ? commission.Value : -1, Description = description}, id);
                }
                catch (ArgumentException e)
                {
                    return UnprocessableEntity(e.Message);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete]
        public async Task<ActionResult<string>> Delete(int id)
        {
            if (await userService.IsAccess(Request, new UserRoleEnum?[] { UserRoleEnum.Notarius }))
            {
                try
                {
                    await serviceService.Delete(id);
                }
                catch (ArgumentException e)
                {
                    return UnprocessableEntity(e.Message);
                }
                catch (Exception)
                {
                    return BadRequest();
                }
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
