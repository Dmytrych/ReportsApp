using System.Linq;
using ReportsApp.Authentication.Dto;
using ReportsApp.WebApi.Controllers.Converters;
using ReportsApp.WebApi.Controllers.Domain.Dto;

namespace ReportsApp.WebApi.Controllers.Domain.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserContext _userContext;
        private readonly IDtoConverter<User, UserClientDto> _userConverter;
        
        public UserRepository(IUserContext userContext, IDtoConverter<User, UserClientDto> userConverter)
        {
            _userContext = userContext;
            _userConverter = userConverter;
        }

        public UserClientDto GetUser(string login, string password)
        {
            var foundUser = _userContext.Users.FirstOrDefault(user => user.Login == login && user.Password == password);
            return _userConverter.Convert(foundUser);
        }

        public UserClientDto AddUser(UserClientDto user)
        {
            var existingUser = _userContext.Users
                .FirstOrDefault(usr => usr.Login == user.Login || usr.Password == user.Password);

            if (existingUser != null)
            {
                return null;
            }

            var addedUser = _userContext.Users.Add(_userConverter.Convert(user));
            _userContext.SaveChanges();

            return _userConverter.Convert(addedUser.Entity);
        }
    }
}