using Azure;
using ECommerce.BAL.Contractors;
using ECommerce.BAL.Models.Requests;
using ECommerce.Common.Constants.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _user;
        public UserController(IUserService user) => _user = user;

        [Authorize(Roles = "Admin"), HttpGet("GetUsers")]
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

        [Authorize, HttpPost("SaveUser")]
        public async Task<IActionResult> SaveUserAsync([FromForm] SaveUserRequest request)
        {
            try
            {
                await _user.SaveUserAsync(request);
                return Ok(Success.SAVED_USER);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous, HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUserAsync(RegisterUserRequest request)
        {
            try
            {
                await _user.RegisterUserAsync(request);
                return Ok(Success.REGISTERED_USER);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous, HttpPost("LoginUser")]
        public IActionResult LoginUser(LoginUserRequest request)
        {
            try
            {
                var response = _user.LoginUser(request);
                if (response == null)
                    return NotFound();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
