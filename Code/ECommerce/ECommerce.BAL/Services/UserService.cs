using ECommerce.BAL.Contractors;
using ECommerce.BAL.Models.DTOs;
using ECommerce.Common.Constants.Messages;
using ECommerce.DAL.Contractors;
using ECommerce.DAL.DataAccess.Entities;

namespace ECommerce.BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileService _file;
        public UserService(IUnitOfWork uow, IFileService file)
        {
            _uow = uow;
            _file = file;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var result = await _uow.UserRepository.GetAllAsync();
            if (result == null)
                throw new Exception(ErrorMessage.GET_USERS);

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
            data.InternalID = isAdd ? Guid.NewGuid() : data.InternalID;

            //Upload Profile
            if(data.Image != null)
                data.ImagePath = await _file.UploadFileAsync(data.InternalID, "profile", data.Image);

            if (isAdd)
                await _uow.UserRepository.InsertAsync(new User
                {
                    InternalID = data.InternalID,
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
                        //data.InternalID,
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
