using Microsoft.EntityFrameworkCore;
using ReportsApp.WebApi.Controllers.Domain.Dto;

namespace ReportsApp.WebApi.Controllers.Domain
{
    public class MsSqlDatabaseContext : DbContext, IStudentContext, IUserContext
    {
        public MsSqlDatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
                "Data Source=DESKTOP-4FGGBK0\\MSSQLSERVER01;Initial Catalog=ReportsAppDb;Integrated Security=True;");
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Student> Students { get; set; }
        
        public DbSet<Dormitory> Dormitories { get; set; }
        
        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<BenefitCategory> BenefitCategories { get; set; }
    }
}