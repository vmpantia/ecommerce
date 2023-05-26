using ECommerce.BAL.Models.DTOs;

namespace ECommerce.BAL.Contractors
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task SaveUserAsync(UserDTO data);
    }
}