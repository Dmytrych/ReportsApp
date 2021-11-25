using ReportsApp.Authentication.Dto;
using ReportsApp.WebApi.Controllers.Domain.UserRepository;

namespace ReportsApp.WebApi.Controllers.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        
        public AuthService(IUserRepository repository)
        {
            _repository = repository;
        }
        
        public UserClientDto Register(UserClientDto user)
        {
            if (user == null || IsUserValid(user))
            {
                return null;
            }

            var resultingUser = _repository.AddUser(user);

            return resultingUser;
        }
        
        public UserClientDto Login(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var resultingUser = _repository.GetUser(login, password);

            return resultingUser;
        }

        private bool IsUserValid(UserClientDto user)
            => string.IsNullOrEmpty(user.Login)
               || string.IsNullOrEmpty(user.Password);
    }
}