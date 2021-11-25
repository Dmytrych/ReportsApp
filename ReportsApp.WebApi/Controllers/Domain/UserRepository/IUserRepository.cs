using ReportsApp.Authentication.Dto;

namespace ReportsApp.WebApi.Controllers.Domain.UserRepository
{
    public interface IUserRepository
    {
        UserClientDto GetUser(string login, string password);

        UserClientDto AddUser(UserClientDto user);
    }
}