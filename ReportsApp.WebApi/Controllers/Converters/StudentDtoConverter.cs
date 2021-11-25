using ReportsApp.WebApi.Controllers.Domain.Dto;
using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Converters
{
    public class StudentDtoConverter : IDtoConverter<StudentClientDto, Student>
    {
        public Student Convert(StudentClientDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            
            return new()
            {
                Id = dto.Id,
                Name = dto.Name
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
                Name = dto.Name
            };
        }
    }
}