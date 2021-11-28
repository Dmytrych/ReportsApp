using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Students
{
    public abstract class StudentSpecification : ISpecification<StudentClientDto>
    {
        public abstract bool IsSatisfiedBy(StudentClientDto candidate);

        public ISpecification<StudentClientDto> And(ISpecification<StudentClientDto> other)
        {
            return new AndSpecification(this, other);
        }
    }
}