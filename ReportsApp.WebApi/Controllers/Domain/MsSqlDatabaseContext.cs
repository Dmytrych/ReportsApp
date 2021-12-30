using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReportsApp.WebApi.Controllers.Domain.Dto;

namespace ReportsApp.WebApi.Controllers.Domain
{
    public class MsSqlDatabaseContext : DbContext, IStudentContext, IUserContext, IDormitoryContext
    {
        public MsSqlDatabaseContext()
        {
            Database.EnsureCreated();

            if (!Users.Any())
            {
                Users.Add(new User
                {
                    Login = "admin",
                    Password = "admin"
                });
                SaveChanges();
            }
            
            if (!Dormitories.Any())
            {
                Dormitories.AddRange(new List<Dormitory>
                {
                    new Dormitory
                    {
                        Capacity = 10,
                        Number = "1"
                    },
                    new Dormitory
                    {
                        Capacity = 10,
                        Number = "2"
                    },
                    new Dormitory
                    {
                        Capacity = 10,
                        Number = "3"
                    },
                    new Dormitory
                    {
                        Capacity = 10,
                        Number = "4"
                    }
                });
                SaveChanges();
            }
            
            if (!Faculties.Any())
            {
                Faculties.AddRange(new List<Faculty>
                {
                    new Faculty
                    {
                        Name = "FICT"
                    },
                    new Faculty
                    {
                        Name = "FPM"
                    },
                    new Faculty
                    {
                        Name = "IASA"
                    }
                });
                SaveChanges();
            }
            
            if (!BenefitCategories.Any())
            {
                BenefitCategories.AddRange(new List<BenefitCategory>
                {
                    new BenefitCategory
                    {
                        Name = "Orphan"
                    },
                    new BenefitCategory
                    {
                        Name = "ATO"
                    }
                });
                SaveChanges();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
                "Data Source=DESKTOP-B69V4OT;Initial Catalog=ReportsAppDbOriginal;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasOne<Dormitory>(s => s.Dormitory);
            modelBuilder.Entity<Student>().HasOne<Faculty>(s => s.Faculty);
            modelBuilder.Entity<Student>().HasOne<BenefitCategory>(s => s.BenefitCategory);
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Student> Students { get; set; }
        
        public DbSet<Dormitory> Dormitories { get; set; }
        
        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<BenefitCategory> BenefitCategories { get; set; }
    }
}