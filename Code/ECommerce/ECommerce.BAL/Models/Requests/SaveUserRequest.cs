using ECommerce.BAL.Models.DTOs;

namespace ECommerce.BAL.Models.Requests
{
    public class SaveUserRequest : BaseRequest
    {
        public UserDTO inputUser { get; set; }
    }
}
