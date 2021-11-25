using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Services
{
    public interface IStudentService
    {
        StudentClientDto AddStudent(StudentClientDto student);
    }
}