using ReportsApp.Authentication.Dto;
using ReportsApp.WebApi.Controllers.Domain.Dto;

namespace ReportsApp.WebApi.Controllers.Converters
{
    public class UserDtoConverter : IDtoConverter<User, UserClientDto>
    {
        public UserClientDto Convert(User dto)
        {
            if (dto == null)
            {
                return null;
            }
            
            return new()
            {
                Id = dto.Id,
                Login = dto.Login,
                Password = dto.Password
            };
        }

        public User Convert(UserClientDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            
            return new()
            {
                Id = dto.Id,
                Login = dto.Login,
                Password = dto.Password
            };
        }
    }
}