using Microsoft.EntityFrameworkCore;
using ReportsApp.WebApi.Controllers.Domain.Dto;

namespace ReportsApp.WebApi.Controllers.Domain
{
    public interface IStudentContext : IDatabaseContext
    {
        DbSet<Student> Students { get; set; }
        
        DbSet<Dormitory> Dormitories { get; set; }
        
        DbSet<Faculty> Faculties { get; set; }
        
        DbSet<BenefitCategory> BenefitCategories { get; set; }
    }
}