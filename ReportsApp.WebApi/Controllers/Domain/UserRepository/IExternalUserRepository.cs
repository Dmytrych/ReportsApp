using System.Collections.Generic;
using ReportsApp.WebApi.Controllers.Domain.Dto;

namespace ReportsApp.WebApi.Controllers.Domain.UserRepository
{
    public interface IExternalUserRepository
    {
        IReadOnlyCollection<User> GetUsers();
    }
}