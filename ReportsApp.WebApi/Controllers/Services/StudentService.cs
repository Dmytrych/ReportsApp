using ReportsApp.WebApi.Controllers.Domain.StudentRepository;
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
            => _studentRepository.AddStudent(student);
    }
}