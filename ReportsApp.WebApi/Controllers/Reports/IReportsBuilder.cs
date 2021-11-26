using System.Collections.Generic;
using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Reports
{
    public interface IReportsBuilder<TResult>
    {
        void Refresh();
        
        void setStatistics(int settled, int notSettled, int benefitialStudents);

        void setBeneficialStudents(IReadOnlyCollection<StudentClientDto> students);

        void setOrinaryStudents(IReadOnlyCollection<StudentClientDto> students);

        void setNotSettledStudents(IReadOnlyCollection<StudentClientDto> students);

        TResult GetResult();
    }
}