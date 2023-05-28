using ECommerce.BAL.Contractors;
using ECommerce.BAL.Models.DTOs;
using ECommerce.BAL.Models.Requests;
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
                Profile = new FileDTO
                {
                    FileName = data.Profile,
                    UrlFilePath = _file.GetURLFilePath(data.Profile),
                },
                Status = data.Status,
                StatusDescription = "Enabled",
                CreatedDate = data.CreatedDate,
                ModifiedDate = data.ModifiedDate
            });
        }

        public async Task SaveUserAsync(SaveUserRequest request)
        {
            if (request == null)
                throw new Exception(ErrorMessage.SAVE_USER_REQUEST_EMPTY);

            var isAdd = request.inputUser.InternalID == Guid.Empty;
            request.inputUser.InternalID = isAdd ? Guid.NewGuid() : request.inputUser.InternalID;

            //Upload Profile
            if(request.inputUser.Profile?.File != null)
                request.inputUser.Profile.FileName = await _file.UploadFileAsync(request.inputUser.InternalID, "Profile", request.inputUser.Profile.File);

            if (isAdd)
                await _uow.UserRepository.InsertAsync(new User
                {
                    InternalID = request.inputUser.InternalID,
                    Username = request.inputUser.Username,
                    Email = request.inputUser.Email,
                    Password = request.inputUser.Password,
                    Role = request.inputUser.Role,
                    Profile = request.inputUser.Profile?.FileName,
                    Status = request.inputUser.Status,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = null
                });
            else
                await _uow.UserRepository.UpdateAsync(request.inputUser.InternalID,
                    new
                    {
                        //request.inputUser.InternalID,
                        request.inputUser.Username,
                        request.inputUser.Email,
                        request.inputUser.Password,
                        request.inputUser.Role,
                        Profile = request.inputUser.Profile?.FileName,
                        request.inputUser.Status,
                        request.inputUser.CreatedDate,
                        ModifiedDate = DateTime.Now
                    });
            await _uow.SaveAsync();
        }

        public async Task RegisterUserAsync(RegisterUserRequest request)
        {
            if (request == null)
                throw new Exception(ErrorMessage.REGISTER_USER_REQUEST_EMPTY);

            await _uow.UserRepository.InsertAsync(new User
            {
                InternalID = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                Role = "User",
                Profile = null,
                Status = 0,
                CreatedDate = DateTime.Now,
                ModifiedDate = null
            });
            await _uow.SaveAsync();

            //TODO: Send Activation Email
        }
    }
}
