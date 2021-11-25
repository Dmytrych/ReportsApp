using ReportsApp.Authentication.Dto;

namespace ReportsApp.WebApi.Controllers.Services
{
    public interface IAuthService
    {
        UserClientDto Register(UserClientDto user);

        UserClientDto Login(string login, string password);
    }
}