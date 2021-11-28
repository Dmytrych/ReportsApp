using System;
using System.Collections.Generic;
using ReportsApp.WebApi.Controllers.Domain.Dto;

namespace ReportsApp.WebApi.Dto
{
    public class StudentClientDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }

        public string DormitoryNumber { get; set; }

        public string FacultyName { get; set; }
        
        public bool? IsBeneficial { get; set; }
        
        public bool? IsSettled { get; set; }

        public string BenefitCategory { get; set; }
    }
}