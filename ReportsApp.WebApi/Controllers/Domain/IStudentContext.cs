using Microsoft.EntityFrameworkCore;
using ReportsApp.WebApi.Controllers.Domain.Dto;

namespace ReportsApp.WebApi.Controllers.Domain
{
    public interface IStudentContext : IDatabaseContext
    {
        DbSet<Student> Students { get; set; }
    }
}