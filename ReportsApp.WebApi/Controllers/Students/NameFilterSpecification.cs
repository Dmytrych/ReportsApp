using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Students
{
    public class NameFilterSpecification : StudentSpecification
    {
        private readonly string _nameFilter;
        
        public NameFilterSpecification(string nameFilterFilter)
        {
            _nameFilter = nameFilterFilter;
        }
        
        public override bool IsSatisfiedBy(StudentClientDto candidate)
            => candidate?.Name?.Contains(_nameFilter) ?? false;
    }
}