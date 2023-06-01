using ECommerce.BAL.Models.DTOs;
using ECommerce.DAL.DataAccess.Entities;

namespace ECommerce.BAL.Contractors
{
    public interface IJwtUtil
    {
        string GenerateAccesToken(User user);
    }
}