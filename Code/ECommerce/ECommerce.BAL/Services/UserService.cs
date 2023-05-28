using ECommerce.BAL.Contractors;
using ECommerce.BAL.Models.DTOs;
using ECommerce.BAL.Models.Requests;
using ECommerce.Common.Constants.Messages;
using ECommerce.DAL.Contractors;
using ECommerce.DAL.DataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileService _file;
        private readonly IConfiguration _config;
        public UserService(IUnitOfWork uow, IFileService file, IConfiguration config)
        {
            _uow = uow;
            _file = file;
            _config = config;
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

        public UserDTO LoginUser(LoginUserRequest request)
        {
            if (request == null)
                throw new Exception(ErrorMessage.LOGIN_USER_REQUEST_EMPTY);

            var result = _uow.UserRepository.GetByCondition(data => (data.Username == request.LogonName ||
                                                                   data.Email == request.LogonName) &&
                                                                  data.Password == request.Password);
            if (!result.Any())
                throw new Exception(ErrorMessage.NO_DATA_FOUND);

            var user = result.First();
            return new UserDTO
            {
                InternalID = user.InternalID,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                Profile = new FileDTO
                {
                    FileName = user.Profile,
                    UrlFilePath = _file.GetURLFilePath(user.Profile),
                },
                Status = user.Status,
                StatusDescription = "Enabled",
                CreatedDate = user.CreatedDate,
                ModifiedDate = user.ModifiedDate
            };
        }

        public string GenerateAccesToken(UserDTO user)
        {
            //Get JWT Security Key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            //Get Credentials using JWT Security Key
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create Identity Claims such as Actor, Name, Email, Role and etc.
            var claims = new[]
            {
                new Claim(ClaimTypes.Actor, user.InternalID.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            //Create Security Token based on the JWT Issuer, JWT Audience, Claims, Expirations and Credentials
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims,
                                             expires: DateTime.Now.AddHours(1), signingCredentials: credentials);

            //Create User Access Token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
