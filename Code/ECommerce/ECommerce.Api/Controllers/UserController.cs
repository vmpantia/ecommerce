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
        private readonly IWebHostEnvironment _environment;
        public UserController(IUserService user, IWebHostEnvironment environment)
        {
            _user = user;
            _environment = environment;
        }

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
                data.ImagePath = await UploadImage(data.Image);
                await _user.SaveUserAsync(data);
                return Ok("User data has been saved successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<string> UploadImage(IFormFile? file)
        {
            if(file == null || file.Length <= 0) 
                return string.Empty;

            if(string.IsNullOrEmpty(_environment.WebRootPath))
                _environment.WebRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            var directoryPath = _environment.WebRootPath + "\\Profile\\";
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var filePath = directoryPath + file.FileName;
            using (FileStream fs = System.IO.File.Create(filePath)) 
            {
                await file.CopyToAsync(fs);
                fs.Flush();
                return filePath;
            }
        }
    }
}
