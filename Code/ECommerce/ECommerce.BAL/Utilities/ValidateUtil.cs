using ECommerce.BAL.Contractors;
using ECommerce.BAL.Extensions;
using ECommerce.BAL.Models.DTOs;
using ECommerce.BAL.Models.enums;
using ECommerce.Common.Constants.Messages;
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
                    return Error.ATTR_USR_NO_CHANGES_MADE;
            }

            if (type == ProcessType.Add || changedProperties.Exists(data => data == dto.Username.GetType().Name))
                if (_uow.UserRepository.IsExist(data => data.Username == dto.Username))
                    return Error.ATTR_USR_USERNAME_EXIST;

            if (type == ProcessType.Add || changedProperties.Exists(data => data == dto.Email.GetType().Name))
                if (_uow.UserRepository.IsExist(data => data.Email == dto.Email))
                    return Error.ATTR_USR_EMAIL_EXIST;

            return string.Empty; /*No error found*/
        }

        public List<string> GetChangedProperty<T1, T2>(T1 newDto, T2 oldDto)
        {
            var changedProperties = new List<string>();

            if (newDto == null || oldDto == null)
                throw new Exception(Error.PARAMETERS_NULL);

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
