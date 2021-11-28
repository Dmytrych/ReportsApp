using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReportsApp.WebApi.Controllers.Converters;
using ReportsApp.WebApi.Controllers.Domain.Dto;
using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Domain.StudentRepository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IStudentContext _studentContext;
        private readonly IDtoConverter<StudentClientDto, Student> _converter;
        
        public StudentRepository(
            IStudentContext studentContext,
            IDtoConverter<StudentClientDto, Student> converter)
        {
            _studentContext = studentContext;
            _converter = converter;
        }
        
        public StudentClientDto AddStudent(StudentClientDto student)
        {
            if (student == null)
            {
                return null;
            }

            var studentEntity = _converter.Convert(student);

            studentEntity.Faculty = _studentContext.Faculties.FirstOrDefault(f => f.Name == student.FacultyName);
            studentEntity.BenefitCategory =
                _studentContext.BenefitCategories.FirstOrDefault(b => b.Name == student.BenefitCategory);
            studentEntity.IsBeneficial = studentEntity.BenefitCategory != null;

            if (string.IsNullOrEmpty(student.DormitoryNumber))
            {
                studentEntity.Dormitory =
                    _studentContext.Dormitories.FirstOrDefault(d => d.Number == student.DormitoryNumber);
            }

            var entity = _studentContext.Students.Add(studentEntity);

            _studentContext.SaveChanges();
            return _converter.Convert(entity.Entity);
        }

        public StudentClientDto GetStudent(int studentId)
            => _converter.Convert(
                _studentContext.Students
                    .Include(e => e.Dormitory)
                    .Include(e => e.Faculty)
                    .Include(e => e.BenefitCategory)
                    .FirstOrDefault(student => studentId == student.Id));

        public IReadOnlyCollection<StudentClientDto> GetAllStudents()
            => _studentContext.Students
                .Include(e => e.Dormitory)
                .Include(e => e.Faculty)
                .Include(e => e.BenefitCategory)
                .Select(s => _converter.Convert(s)).ToList();
    }
}