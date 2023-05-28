using ECommerce.BAL.Contractors;
using ECommerce.BAL.Models.Requests;
using ECommerce.Common.Constants.Messages;
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
        public async Task<IActionResult> SaveUserAsync([FromForm] SaveUserRequest request)
        {
            try
            {
                await _user.SaveUserAsync(request);
                return Ok(SuccessMessage.SAVED_USER);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUserAsync(RegisterUserRequest request)
        {
            try
            {
                await _user.RegisterUserAsync(request);
                return Ok(SuccessMessage.REGISTERED_USER);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
