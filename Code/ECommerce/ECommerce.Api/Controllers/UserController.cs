using ECommerce.BAL.Contractors;
using ECommerce.BAL.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;
        public UserController(IUserService user) => _user = user;

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsersAsync()
        {
            try
            {
                var response = await _user.GetUsersAsync();
                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SaveUser")]
        public async Task<IActionResult> SaveUserAsync([FromForm] UserDTO data)
        {
            try
            {
                await _user.SaveUserAsync(data);
                return Ok("User data has been saved successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
