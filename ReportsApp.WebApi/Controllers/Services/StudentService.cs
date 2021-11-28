using System.Collections.Generic;
using System.Linq;
using ReportsApp.WebApi.Controllers.Domain.StudentRepository;
using ReportsApp.WebApi.Controllers.Dto;
using ReportsApp.WebApi.Controllers.Students;
using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        
        public StudentService(
            IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public StudentClientDto AddStudent(StudentClientDto student)
        {
            if (student == null 
                || string.IsNullOrEmpty(student.Name) 
                || string.IsNullOrEmpty(student.Surname)
                || string.IsNullOrEmpty(student.FacultyName))
            {
                return null;
            }
            
            return _studentRepository.AddStudent(student);
        }

        public IReadOnlyCollection<StudentClientDto> FilterStudents(StudentFilterClientDto filter)
        {
            ISpecification<StudentClientDto> spec = new EmptySpecification();

            if (filter == null)
            {
                return _studentRepository.GetAllStudents().ToList();
            }

            if (filter.Settled.HasValue)
            {
                spec = spec.And(new SettleFilterSpecification(filter.Settled.Value));
            }
            
            if (!string.IsNullOrEmpty(filter.Name?.Trim()))
            {
                spec = spec.And(new NameFilterSpecification(filter.Name.Trim()));
            }

            return _studentRepository.GetAllStudents().Where(s => spec.IsSatisfiedBy(s)).ToList();
        }
    }
}