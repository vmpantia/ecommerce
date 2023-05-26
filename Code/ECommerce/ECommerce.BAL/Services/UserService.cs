using ECommerce.BAL.Models.DTOs;
using ECommerce.DAL.Contractors;
using ECommerce.DAL.DataAccess.Entities;

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

        public async Task SaveUserAsync(UserDTO data)
        {
            var isAdd = data.InternalID == Guid.Empty;
            if(isAdd)
                await _uow.UserRepository.InsertAsync(new User
                {
                    InternalID = Guid.NewGuid(),
                    Username = data.Username,
                    Email = data.Email,
                    Password = data.Password,
                    Role = data.Role,
                    ImagePath = data.ImagePath,
                    Status = data.Status,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = null
                });
            else
                await _uow.UserRepository.UpdateAsync(data.InternalID, 
                    new 
                    {
                        //InternalID = Guid.NewGuid(),
                        data.Username,
                        data.Email,
                        data.Password,
                        data.Role,
                        data.ImagePath,
                        data.Status,
                        data.CreatedDate,
                        ModifiedDate = DateTime.Now
                    });
            await _uow.SaveAsync();
        }
    }
}
