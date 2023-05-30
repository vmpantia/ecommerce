using ECommerce.BAL.Models.DTOs;
using Microsoft.AspNetCore.Http;

namespace ECommerce.BAL.Models.Requests
{
    public class SaveUserRequest : BaseRequest
    {
        public UserDTO inputUser { get; set; }
        public IFormFile formProfile { get; set; }
    }
}
