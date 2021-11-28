using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Students
{
    public class EmptySpecification : StudentSpecification
    {
        public override bool IsSatisfiedBy(StudentClientDto candidate)
            => true;
    }
}