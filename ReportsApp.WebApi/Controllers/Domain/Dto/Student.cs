using System;

namespace ReportsApp.WebApi.Controllers.Domain.Dto
{
    public class Student
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }

        public Dormitory Dormitory { get; set; }

        public Faculty Faculty { get; set; }

        public DateTime BirthDate { get; set; }
        
        public bool IsBeneficial { get; set; }
        
        public BenefitCategory BenefitCategory { get; set; }
    }
}