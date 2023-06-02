using ECommerce.BAL.Models.DTOs;
using ECommerce.Common;
using ECommerce.DAL.DataAccess.Entities;

namespace ECommerce.BAL.Extensions
{
    public static class UserExtension
    {
        public static UserDTO ConvertDTO(this User entity)
        {
            return new UserDTO
            {
                InternalID = entity.InternalID,
                Username = entity.Username,
                Email = entity.Email,
                Password = entity.Password,
                Role = entity.Role,
                Profile = entity.Profile,
                Status = entity.Status,
                StatusDescription = Parser.ParseStatus(entity.Status),
                CreatedDate = entity.CreatedDate,
                ModifiedDate = entity.ModifiedDate
            };
        }
    }
}
