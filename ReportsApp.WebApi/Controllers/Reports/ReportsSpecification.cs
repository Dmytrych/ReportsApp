using System.Collections.Generic;
using System.Linq;
using ReportsApp.WebApi.Controllers.Domain;
using ReportsApp.WebApi.Controllers.Domain.Dto;
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
            => _studentRepository.GetAllStudents().Where(s => s.IsBeneficial.Value && !s.IsSettled.Value).ToList();

        public IReadOnlyCollection<StudentClientDto> GetOrdinaryStudents()
            => _studentRepository.GetAllStudents().Where(s => !s.IsBeneficial.Value && !s.IsSettled.Value).ToList();

        public IReadOnlyCollection<StudentClientDto> GetNotSettledStudents()
            => _studentRepository.GetAllStudents().Where(s => !s.IsSettled.Value).ToList();

        public bool DormitoryHasFreeSpace(int id)
        { 
            Dormitory dorm = null;

            if (dorm == null)
            {
                return true;
            }

            return dorm.Capacity > _studentRepository.GetAllStudents().Count(s => s.DormitoryNumber == dorm.Number);
        }
    }
}