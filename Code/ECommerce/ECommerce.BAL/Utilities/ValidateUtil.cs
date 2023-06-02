using ECommerce.BAL.Contractors;
using ECommerce.BAL.Extensions;
using ECommerce.BAL.Models.DTOs;
using ECommerce.BAL.Models.enums;
using ECommerce.DAL.Contractors;

namespace ECommerce.BAL.Utilities
{
    public class ValidateUtil : IValidateUtil
    {
        private readonly IUnitOfWork _uow;
        public ValidateUtil(IUnitOfWork uow) => _uow = uow;

        public async Task<string> ValidateUserAsync(UserDTO dto, ProcessType type)
        {
            var changedProperties = new List<string>();

            if (type == ProcessType.Edit)
            {
                var oldDto = await _uow.UserRepository.GetByIDAsync(dto.InternalID);
                changedProperties = GetChangedProperty(dto, oldDto.ConvertDTO());
                if (!changedProperties.Any())
                    return "No changes made in user.";
            }

            if (type == ProcessType.Add || changedProperties.Exists(data => data == dto.Username.GetType().Name))
                if (_uow.UserRepository.IsExist(data => data.Username == dto.Username))
                    return "The Username field is already exist in the system.";

            if (type == ProcessType.Add || changedProperties.Exists(data => data == dto.Email.GetType().Name))
                if (_uow.UserRepository.IsExist(data => data.Email == dto.Email))
                    return "The Email field is already exist in the system.";

            return string.Empty; /*No error found*/
        }

        public List<string> GetChangedProperty<T1, T2>(T1 newDto, T2 oldDto)
        {
            var changedProperties = new List<string>();

            if (newDto == null || oldDto == null)
                throw new Exception("Parameters cannot be null.");

            var properties = newDto.GetType().GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(newDto, null)?.ToString();
                var oldValue = property.GetValue(oldDto, null)?.ToString();

                if (newValue != oldValue)
                    changedProperties.Add(property.Name);
            }

            return changedProperties;
        }
    }
}
