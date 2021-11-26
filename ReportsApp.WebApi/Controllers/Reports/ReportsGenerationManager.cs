using System.Collections.Generic;
using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Reports
{
    public class ReportsGenerationManager : IReportsGenerationManager
    {
        private readonly IReportsBuilder<string> _textReportsBuilder;
        
        public ReportsGenerationManager(IReportsBuilder<string> textReportsBuilder)
        {
            _textReportsBuilder = textReportsBuilder;
        }
        
        public string GetTextReport(
            IReadOnlyCollection<StudentClientDto> ordinaryStudents,
            IReadOnlyCollection<StudentClientDto> notSettledStudents,
            IReadOnlyCollection<StudentClientDto> beneficiaryStudents)
        {
            _textReportsBuilder.setStatistics(ordinaryStudents.Count + beneficiaryStudents.Count, notSettledStudents.Count, beneficiaryStudents.Count);
            _textReportsBuilder.setBeneficialStudents(beneficiaryStudents);
            _textReportsBuilder.setOrinaryStudents(ordinaryStudents);
            _textReportsBuilder.setNotSettledStudents(notSettledStudents);
            return _textReportsBuilder.GetResult();
        }
    }
}