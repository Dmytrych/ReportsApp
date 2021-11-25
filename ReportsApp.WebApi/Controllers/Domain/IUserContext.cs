using Microsoft.EntityFrameworkCore;
using ReportsApp.WebApi.Controllers.Domain.Dto;

namespace ReportsApp.WebApi.Controllers.Domain
{
    public interface IUserContext : IDatabaseContext
    {
        DbSet<User> Users { get; set; }
    }
}