using ECommerce.BAL.Models.DTOs;
using ECommerce.DAL.Contractors;

namespace ECommerce.BAL.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _uow;
        public UserService(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var result = await _uow.UserRepository.GetAllAsync();
            if (result == null)
                throw new Exception("Error in fetching of users information in the system.");

            return result.Select(data => new UserDTO
            {
                InternalID = data.InternalID,
                Username = data.Username,
                Email = data.Email,
                Password = data.Password,
                Role = data.Role,
                ImagePath = data.ImagePath,
                Status = data.Status,
                StatusDescription = "Enabled",
                CreatedDate = data.CreatedDate,
                ModifiedDate = data.ModifiedDate
            });
        }
    }
}
