using System;
using System.Threading.Tasks;
using CarDealer.Infrastructure.Bundles.User.DTO;
using CarDealer.Infrastructure.Bundles.User.Service;
using CarDealer.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace CarDealer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
            private readonly IUserService _iUserService;
            private readonly CarDealerContext _carDealerContext;

            public UserController(IUserService iUserService, CarDealerContext carDealerContext)
            {
                _iUserService = iUserService;
                _carDealerContext = carDealerContext;
            }

            [HttpGet("GetUser/{Id}")]
            public async Task<IActionResult> GetUserById(long id)
            {
                try
                {
                    var user = await _iUserService.GetById(id);
                    return Ok(user);
                }
                catch (NullReferenceException e)
                {
                    return NotFound($"Can't found user with id = {id}");
                }
            }
 
            [HttpGet("GetAllUsers")]
            public async Task<IActionResult> GetAllUsers()
            {
                var users = await _iUserService.GetAll();
                return Ok(users);
            }

            [HttpPost]
            public async Task<IActionResult> CreateAdvert([FromBody] UserDto userDto)
            {
                if (userDto == null)
                {
                    return BadRequest();
                }

                await _iUserService.Add(userDto);
                return Created("Succesfully created an user.", userDto);
            }

            [HttpPut("UpdateUser")]
            public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
            {
                if (userDto == null)
                {
                    return BadRequest();
                }

                await _iUserService.Update(userDto);
                return Ok($"Updated user with Login = {userDto.Login}");
            }

            [HttpDelete("DeleteUser/{id}")]

            public async Task<IActionResult> DeleteUser(long id)
            {
                await _iUserService.Delete(id);
                return Ok($"User with id = {id} deleted");
            }
    }
}