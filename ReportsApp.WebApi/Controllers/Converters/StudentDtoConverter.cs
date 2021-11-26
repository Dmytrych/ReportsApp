﻿using System.Linq;
using ReportsApp.WebApi.Controllers.Domain;
using ReportsApp.WebApi.Controllers.Domain.Dto;
using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Converters
{
    public class StudentDtoConverter : IDtoConverter<StudentClientDto, Student>
    {
        private readonly IStudentContext _studentContext;
        
        public StudentDtoConverter(IStudentContext studentContext)
        {
            _studentContext = studentContext;
        }
        
        public Student Convert(StudentClientDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            
            return new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Surname = dto.Surname,
                BenefitCategory = _studentContext.BenefitCategories.FirstOrDefault(b => b.Name == dto.BenefitCategory),
                BirthDate = dto.BirthDate,
                Dormitory = _studentContext.Dormitories.FirstOrDefault(d => d.Number == dto.DormitoryNumber),
                Faculty = _studentContext.Faculties.FirstOrDefault(f => f.Name == dto.FacultyName),
                IsBeneficial = _studentContext.BenefitCategories.FirstOrDefault(b => b.Name == dto.BenefitCategory) != null
            };
        }

        public StudentClientDto Convert(Student dto)
        {
            if (dto == null)
            {
                return null;
            }
            
            return new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Surname = dto.Surname,
                BenefitCategory = dto.BenefitCategory.Name,
                BirthDate = dto.BirthDate,
                DormitoryNumber = dto.Dormitory?.Number ?? 0,
                FacultyName = dto.Faculty.Name,
                IsBeneficial = dto.BenefitCategory != null,
                IsSettled = dto.Dormitory != null
            };
        }
    }
}