using System.Collections.Generic;
using ReportsApp.WebApi.Controllers.Dto;
using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Services
{
    public interface IStudentService
    {
        public IReadOnlyCollection<StudentClientDto> FilterStudents(StudentFilterClientDto filter);

        StudentClientDto AddStudent(StudentClientDto student);
    }
}