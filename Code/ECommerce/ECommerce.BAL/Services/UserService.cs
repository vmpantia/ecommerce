﻿using ECommerce.BAL.Contractors;
using ECommerce.BAL.Models.DTOs;
using ECommerce.BAL.Models.Requests;
using ECommerce.Common;
using ECommerce.Common.Constants;
using ECommerce.Common.Constants.Messages;
using ECommerce.Common.Utils;
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
        private readonly IConfiguration _config;
        public UserService(IUnitOfWork uow,
                           IConfiguration config)
        {
            _uow = uow;
            _config = config;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var result = await _uow.UserRepository.GetAllAsync();
            if (result == null)
                throw new Exception(Error.GET_USRS_NULL);

            return result.Select(data => new UserDTO
            {
                InternalID = data.InternalID,
                Username = data.Username,
                Email = data.Email,
                Password = data.Password,
                Role = data.Role,
                Profile = FileUtil.GetURLFilePath(data.Profile),
                Status = data.Status,
                StatusDescription = Parser.ParseStatus(data.Status),
                CreatedDate = data.CreatedDate,
                ModifiedDate = data.ModifiedDate
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
                request.inputUser.Profile = await FileUtil.UploadFileAsync(request.inputUser.InternalID, FileType.PROFILE, request.formProfile);

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

        public UserDTO LoginUser(LoginUserRequest request)
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
            return new UserDTO
            {
                InternalID = user.InternalID,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                Profile = FileUtil.GetURLFilePath(user.Profile),
                Status = user.Status,
                StatusDescription = Parser.ParseStatus(user.Status),
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
