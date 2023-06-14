using ECommerce.BAL.Contractors;
using ECommerce.BAL.Extensions;
using ECommerce.BAL.Models.DTOs;
using ECommerce.BAL.Models.enums;
using ECommerce.BAL.Models.Requests;
using ECommerce.BAL.Utilities;
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
        private readonly IValidateUtil _validate;
        private readonly IPasswordUtil _password;
        public UserService(IUnitOfWork uow, 
                            IFileUtil file, 
                            IEmailUtil email,
                            IJwtUtil jwt,
                            IValidateUtil validate,
                            IPasswordUtil password)
        {
            _uow = uow;
            _file = file;
            _email = email;
            _jwt = jwt;
            _validate = validate;
            _password = password;
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

            var type = request.inputUser.InternalID == Guid.Empty ? ProcessType.Add : ProcessType.Edit; /*Check if user is for add or edit*/

            //Validate userDTO
            var valResult = await _validate.ValidateUserAsync(request.inputUser, type);
            if (!string.IsNullOrEmpty(valResult)) /*Check if valResult have value*/
                throw new Exception(valResult);

            //Generate new guid or use old guid
            var internalID = type == ProcessType.Add ? Guid.NewGuid() : request.inputUser.InternalID;

            //Upload Profile
            if (request.formProfile != null)
                request.inputUser.Profile = await _file.UploadFileAsync(internalID, FileType.PROFILE, request.formProfile);

            switch (type)
            {
                case ProcessType.Add:
                    await _uow.UserRepository.InsertAsync(new User
                    {
                        InternalID = internalID,
                        Username = request.inputUser.Username,
                        Email = request.inputUser.Email,
                        Password = _password.ParsePassword(request.inputUser.Password, true),
                        Role = request.inputUser.Role,
                        FirstName = request.inputUser.FirstName,
                        MiddleName = request.inputUser.MiddleName,
                        LastName = request.inputUser.LastName,
                        BirthDate = DateTime.Parse(request.inputUser.BirthDate),
                        Profile = request.inputUser.Profile,
                        Status = request.inputUser.Status,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = null
                    });
                    break;
                case ProcessType.Edit:
                    await _uow.UserRepository.UpdateAsync(internalID,
                        new
                        {
                            //request.inputUser.InternalID,
                            request.inputUser.Username,
                            request.inputUser.Email,
                            Password = _password.ParsePassword(request.inputUser.Password, true),
                            request.inputUser.Role,
                            request.inputUser.FirstName,
                            request.inputUser.MiddleName,
                            request.inputUser.LastName,
                            request.inputUser.BirthDate,
                            request.inputUser.Profile,
                            request.inputUser.Status,
                            //request.inputUser.CreatedDate,
                            ModifiedDate = DateTime.Now
                        });
                    break;
            }
            await _uow.SaveAsync();
        }

        public async Task RegisterUserAsync(RegisterUserRequest request)
        {
            if (request == null) /*Check if the request is null or empty*/
                throw new Exception(Error.REG_USR_REQUEST_NULL);

            if (_uow.UserRepository.IsExist(data => data.Username == request.inputUser.Username ||
                                                    data.Email == request.inputUser.Email)) /*Check if the email or username is already exist*/
                throw new Exception(Error.ATTR_USR_LOGON_NAME_EXIST);

            await _uow.UserRepository.InsertAsync(new User
            {
                InternalID = Guid.NewGuid(),
                Username = request.inputUser.Username,
                Email = request.inputUser.Email,
                Password = _password.ParsePassword(request.inputUser.Password, true),
                Role = "User",
                FirstName = request.inputUser.FirstName,
                MiddleName = request.inputUser.MiddleName,
                LastName = request.inputUser.LastName,
                BirthDate = DateTime.Parse(request.inputUser.BirthDate),
                Profile = null,
                Status = Status.INVALID_INT,
                CreatedDate = DateTime.Now,
                ModifiedDate = null
            });

            await _uow.SaveAsync();
            //TODO: Send Activation Email
        }

        public ClientDTO LoginUser(LoginUserRequest request)
        {
            if (request == null) /*Check if the request is null or empty*/
                throw new Exception(Error.LOGIN_USR_REQUEST_NULL);

            var result = _uow.UserRepository.GetByCondition(data => (data.Username == request.LogonName ||
                                                                     data.Email == request.LogonName) &&
                                                                     data.Password == _password.ParsePassword(request.Password, true));

            if (!result.Any()) /*Check if user is exist based on the credentials*/
                throw new Exception(Error.NO_DATA_FOUND);

            if (result.First().Status < Status.ENABLED_INT) /*Check if user is invalid (not activated)*/
                throw new Exception(Error.ATTR_USR_STATUS_NOT_ACTIVATED);

            if (result.First().Status > Status.ENABLED_INT) /*Check if user is not able to login*/
                throw new Exception(Error.ATTR_USR_STATUS_NOT_ENABLED);

            var user = result.First();
            return new ClientDTO
            {
                Name = string.Format(Format.NAME, user.LastName, user.FirstName),
                Email = user.Email,
                AccessToken = _jwt.GenerateAccesToken(user)
            };
        }
    }
}
