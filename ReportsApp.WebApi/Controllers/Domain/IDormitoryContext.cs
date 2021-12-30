using Microsoft.EntityFrameworkCore;
using ReportsApp.WebApi.Controllers.Domain.Dto;

namespace ReportsApp.WebApi.Controllers.Domain
{
    public interface IDormitoryContext : IDatabaseContext
    {
        DbSet<Dormitory> Dormitories { get; set; }
    }
}