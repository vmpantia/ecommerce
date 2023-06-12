using ECommerce.BAL.Models.DTOs;
using ECommerce.BAL.Models.Requests;

namespace ECommerce.BAL.Contractors
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task SaveUserAsync(SaveUserRequest data);
        Task RegisterUserAsync(RegisterUserRequest data);
        ClientDTO LoginUser(LoginUserRequest request);
    }
}