using System.Collections.Generic;
using ReportsApp.Authentication.Dto;

namespace ReportsApp.WebApi.Controllers.Domain.UserRepository
{
    public interface IExternalUserRepository
    {
        public IReadOnlyCollection<UserClientDto> GetAll();
    }
}