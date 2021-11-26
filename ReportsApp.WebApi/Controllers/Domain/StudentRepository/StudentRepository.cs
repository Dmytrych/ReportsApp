using System.Collections.Generic;
using System.Linq;
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
            
            var entity = _studentContext.Students.Add(studentEntity);

            _studentContext.SaveChanges();
            return _converter.Convert(entity.Entity);
        }

        public StudentClientDto GetStudent(int studentId)
            => _converter.Convert(
                _studentContext.Students
                    .FirstOrDefault(student => studentId == student.Id));

        public IReadOnlyCollection<StudentClientDto> GetAllStudents()
            => _studentContext.Students.Select(s => _converter.Convert(s)).ToList();
    }
}