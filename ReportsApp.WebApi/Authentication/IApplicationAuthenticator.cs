using ReportsApp.Authentication;
using ReportsApp.Authentication.Dto;

namespace ReportsApp.WebApi.Authentication
{
    public interface IApplicationAuthenticator
    {
        UserClientDto Authenticate(string token);
    }
}