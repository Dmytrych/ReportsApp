using System.Collections.Generic;
using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Reports
{
    public interface IReportsGenerationManager
    {
        string GetTextReport(
            IReadOnlyCollection<StudentClientDto> ordinaryStudents,
            IReadOnlyCollection<StudentClientDto> notSettledStudents,
            IReadOnlyCollection<StudentClientDto> beneficiaryStudents);
    }
}