using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Students
{
    public class SettleFilterSpecification : StudentSpecification
    {
        private readonly bool _settleFilter;
        
        public SettleFilterSpecification(bool settleFilter)
        {
            _settleFilter = settleFilter;
        }

        public override bool IsSatisfiedBy(StudentClientDto candidate)
            => candidate != null && candidate.IsSettled == _settleFilter;
    }
}