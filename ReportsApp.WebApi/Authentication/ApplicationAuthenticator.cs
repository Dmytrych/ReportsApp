using ReportsApp.Authentication;
using ReportsApp.Authentication.Dto;
using ReportsApp.WebApi.Controllers.Domain.UserRepository;

namespace ReportsApp.WebApi.Authentication
{
    public class ApplicationAuthenticator : IApplicationAuthenticator
    {
        private readonly IUserRepository _userRepository;
        
        public ApplicationAuthenticator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public UserClientDto Authenticate(string token)
        {
            var credentials = token.Split('/');
            if (credentials.Length != 2)
            {
                return null;
            }

            var login = credentials[0];
            var password = credentials[1];
            return _userRepository.GetUser(login, password);
        }
    }
}