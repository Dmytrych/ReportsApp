using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Domain.StudentRepository
{
    public interface IStudentRepository
    {
        StudentClientDto AddStudent(StudentClientDto student);

        StudentClientDto GetStudent(int studentId);
    }
}