using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Students
{
    public class AndSpecification : StudentSpecification
    {
        private ISpecification<StudentClientDto> One;
        private ISpecification<StudentClientDto> Other;
        
        public AndSpecification(ISpecification<StudentClientDto> specOne, ISpecification<StudentClientDto> specTwo) 
        {
            One = specOne;
            Other = specTwo;
        }
        
        public override bool IsSatisfiedBy(StudentClientDto candidate)
        {
            return One.IsSatisfiedBy(candidate) && Other.IsSatisfiedBy(candidate);
        }
    }
}