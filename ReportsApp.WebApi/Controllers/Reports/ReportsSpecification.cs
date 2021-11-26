using System.Collections.Generic;
using System.Linq;
using ReportsApp.WebApi.Controllers.Domain;
using ReportsApp.WebApi.Controllers.Domain.StudentRepository;
using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Reports
{
    public class ReportsSpecification : IReportsSpecification
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentContext _context;
        
        public ReportsSpecification(IStudentRepository studentRepository, IStudentContext context)
        {
            _studentRepository = studentRepository;
            _context = context;
        }
        
        public IReadOnlyCollection<StudentClientDto> GetBeneficiaryStudents()
            => _studentRepository.GetAllStudents().Where(s => s.IsBeneficial && !s.IsSettled).ToList();

        public IReadOnlyCollection<StudentClientDto> GetOrdinaryStudents()
            => _studentRepository.GetAllStudents().Where(s => !s.IsBeneficial && !s.IsSettled).ToList();

        public IReadOnlyCollection<StudentClientDto> GetNotSettledStudents()
            => _studentRepository.GetAllStudents().Where(s => !s.IsSettled).ToList();

        public bool DormitoryHasFreeSpace(int id)
        {
            var dorm = _context.Dormitories.FirstOrDefault(d => d.Id == id);

            if (dorm == null)
            {
                return false;
            }

            return dorm.Capacity > _studentRepository.GetAllStudents().Count(s => s.DormitoryNumber == dorm.Number);
        }
    }
}