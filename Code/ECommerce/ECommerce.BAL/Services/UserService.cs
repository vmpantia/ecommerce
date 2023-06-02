using ECommerce.BAL.Contractors;
using ECommerce.BAL.Extensions;
using ECommerce.BAL.Models.DTOs;
using ECommerce.BAL.Models.Requests;
using ECommerce.Common.Constants;
using ECommerce.Common.Constants.Messages;
using ECommerce.DAL.Contractors;
using ECommerce.DAL.DataAccess.Entities;

namespace ECommerce.BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileUtil _file;
        private readonly IEmailUtil _email;
        private readonly IJwtUtil _jwt;
        public UserService(IUnitOfWork uow, 
                            IFileUtil file, 
                            IEmailUtil email,
                            IJwtUtil jwt)
        {
            _uow = uow;
            _file = file;
            _email = email;
            _jwt = jwt;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var result = await _uow.UserRepository.GetAllAsync();
            if (result == null)
                throw new Exception(Error.GET_USRS_NULL);

            return result.Select(data =>
            {
                var dto = data.ConvertDTO();
                //Get URL Path
                dto.Profile = _file.GetURLFilePath(data.InternalID, data.Profile);
                return dto;
            });
        }

        public async Task SaveUserAsync(SaveUserRequest request)
        {
            if (request == null)
                throw new Exception(Error.SAVE_USR_REQUEST_NULL);

            var isAdd = request.inputUser.InternalID == Guid.Empty; /*Check if user is for add or edit*/
            request.inputUser.InternalID = isAdd ? Guid.NewGuid() : request.inputUser.InternalID;

            //Upload Profile
            if (request.formProfile != null)
                request.inputUser.Profile = await _file.UploadFileAsync(request.inputUser.InternalID, FileType.PROFILE, request.formProfile);

            if (isAdd)
                await _uow.UserRepository.InsertAsync(new User
                {
                    InternalID = request.inputUser.InternalID,
                    Username = request.inputUser.Username,
                    Email = request.inputUser.Email,
                    Password = request.inputUser.Password,
                    Role = request.inputUser.Role,
                    Profile = request.inputUser.Profile,
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
                        request.inputUser.Profile,
                        request.inputUser.Status,
                        //request.inputUser.CreatedDate,
                        ModifiedDate = DateTime.Now
                    });
            await _uow.SaveAsync();
        }

        public async Task RegisterUserAsync(RegisterUserRequest request)
        {
            if (request == null) /*Check if the request is null or empty*/
                throw new Exception(Error.REG_USR_REQUEST_NULL);

            if (_uow.UserRepository.IsExist(data => data.Username == request.Username ||
                                                    data.Email == request.Email)) /*Check if the email or username is already exist*/
                throw new Exception(Error.ATTR_USR_LOGON_NAME_EXIST);

            await _uow.UserRepository.InsertAsync(new User
            {
                InternalID = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                Role = "User",
                Profile = null,
                Status = Status.INVALID_INT,
                CreatedDate = DateTime.Now,
                ModifiedDate = null
            });

            await _uow.SaveAsync();
            //TODO: Send Activation Email
        }

        public LoginDTO LoginUser(LoginUserRequest request)
        {
            if (request == null) /*Check if the request is null or empty*/
                throw new Exception(Error.LOGIN_USR_REQUEST_NULL);

            var result = _uow.UserRepository.GetByCondition(data => (data.Username == request.LogonName ||
                                                                   data.Email == request.LogonName) &&
                                                                  data.Password == request.Password);

            if (!result.Any()) /*Check if user is exist based on the credentials*/
                throw new Exception(Error.NO_DATA_FOUND);

            if (result.First().Status < Status.ENABLED_INT) /*Check if user is invalid (not activated)*/
                throw new Exception(Error.ATTR_USR_STATUS_NOT_ACTIVATED);

            if (result.First().Status > Status.ENABLED_INT) /*Check if user is not able to login*/
                throw new Exception(Error.ATTR_USR_STATUS_NOT_ENABLED);

            var user = result.First();
            return new LoginDTO
            {
                Email = user.Email,
                AccessToken = _jwt.GenerateAccesToken(user)
            };
        }
    }
}
