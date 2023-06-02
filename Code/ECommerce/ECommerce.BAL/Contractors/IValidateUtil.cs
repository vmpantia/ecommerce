using ECommerce.BAL.Models.DTOs;
using ECommerce.BAL.Models.enums;

namespace ECommerce.BAL.Contractors
{
    public interface IValidateUtil
    {
        List<string> GetChangedProperty<T1, T2>(T1 newDto, T2 oldDto);
        Task<string> ValidateUserAsync(UserDTO dto, ProcessType type);
    }
}