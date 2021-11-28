using System.Collections.Generic;
using System.Linq;
using ReportsApp.WebApi.Controllers.Domain;
using ReportsApp.WebApi.Controllers.Dto;
using ReportsApp.WebApi.Controllers.Reports;
using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Services
{
    public class ReportsGenerationService : IReportsGenerationService
    {
        private readonly IReportsGenerationManager _reportsGenerationManager;
        private readonly IReportsSpecification _reportsSpecification;
        private readonly IStudentContext _studentContext;

        public ReportsGenerationService(
            IReportsGenerationManager reportsGenerationManager,
            IReportsSpecification reportsSpecification,
            IStudentContext studentContext)
        {
            _reportsGenerationManager = reportsGenerationManager;
            _reportsSpecification = reportsSpecification;
            _studentContext = studentContext;
        }

        public ReportResponseDto Generate()
        {
            var students = _reportsSpecification.GetBeneficiaryStudents();
            var results = SettleCollection(students);

            var ordinaryResults = SettleCollection(_reportsSpecification.GetOrdinaryStudents());

            if (results.settled.Count == 0 && ordinaryResults.settled.Count == 0)
            {
                return new ReportResponseDto
                {
                    Text = ""
                };
            }
            
            return GenerateReport(results.settled, ordinaryResults.settled, _reportsSpecification.GetNotSettledStudents());
        }

        private ReportResponseDto GenerateReport(
            IReadOnlyCollection<StudentClientDto> beneficiary,
            IReadOnlyCollection<StudentClientDto> ordinary,
            IReadOnlyCollection<StudentClientDto> notSettled)
            =>new ReportResponseDto
            {
                Text = _reportsGenerationManager.GetTextReport(
                    ordinary,
                    notSettled,
                    beneficiary)
            };

        private (IReadOnlyCollection<StudentClientDto> settled, bool allSettled) SettleCollection(IReadOnlyCollection<StudentClientDto> students)
        {
            var settled = new List<StudentClientDto>();
            foreach (var student in students)
            {
                if (!TrySettle(student))
                {
                    return (settled, false);
                }
                settled.Add(student);
            }

            return (settled, true);
        }

        private bool TrySettle(StudentClientDto student)
        {
            var entity = _studentContext.Students.FirstOrDefault(s => s.Id == student.Id);

            if (entity == null)
            {
                return false;
            }

            var dorm = _studentContext.Dormitories.ToList().FirstOrDefault(d => _reportsSpecification.DormitoryHasFreeSpace(d.Id));

            if (dorm == null)
            {
                return false;
            }

            entity.Dormitory = dorm;
            _studentContext.SaveChanges();
            return true;
        }
    }
}