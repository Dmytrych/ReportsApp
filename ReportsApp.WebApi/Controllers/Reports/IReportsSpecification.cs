using System.Collections.Generic;
using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Reports
{
    public interface IReportsSpecification
    {
        IReadOnlyCollection<StudentClientDto> GetBeneficiaryStudents();
        
        IReadOnlyCollection<StudentClientDto> GetOrdinaryStudents();

        IReadOnlyCollection<StudentClientDto> GetNotSettledStudents();
        
        bool DormitoryHasFreeSpace(int id);
    }
}