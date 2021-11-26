using System.Collections.Generic;
using ReportsApp.WebApi.Dto;

namespace ReportsApp.WebApi.Controllers.Reports
{
    public class TextReportsBuilder : IReportsBuilder<string>
    {
        private string Statistics { get; set; }
        
        private string BeneficialStudentsSettled { get; set; }
        
        private string OrdinaryStudentsSettled { get; set; }
        
        private string StudentsNotSettled { get; set; }

        public TextReportsBuilder()
        {
            Refresh();
        }

        public void Refresh()
        {
            Statistics = "";
            BeneficialStudentsSettled = "";
            OrdinaryStudentsSettled = "";
            StudentsNotSettled = "";
            StudentsNotSettled = "";
        }

        public void setStatistics(int settled, int notSettled, int benefitialStudents)
            => Statistics = "\n---Statistics---\n" +
                            $"Total settled: {settled}\n" +
                            $"Not settled: {notSettled}\n" +
                            $"Beneficial: {benefitialStudents}\n";

        public void setBeneficialStudents(IReadOnlyCollection<StudentClientDto> students)
        {
            BeneficialStudentsSettled = "\n---Beneficial Students Settled---\n";
            foreach (var student in students)
            {
                BeneficialStudentsSettled +=
                    $"{student.Name}, {student.Surname}, Birth Date: {student.BirthDate.ToString()}, Benefit: {student.BenefitCategory}, Dormitory: {student.DormitoryNumber}, Faculty: {student.FacultyName}\n";
            }
        }

        public void setOrinaryStudents(IReadOnlyCollection<StudentClientDto> students)
        {
            OrdinaryStudentsSettled = "\n---Ordinary Students Settled---\n";
            foreach (var student in students)
            {
                OrdinaryStudentsSettled +=
                    $"{student.Name}, {student.Surname}, Birth Date: {student.BirthDate.ToString()}, Dormitory: {student.DormitoryNumber}, Faculty: {student.FacultyName}\n";
            }
        }

        public void setNotSettledStudents(IReadOnlyCollection<StudentClientDto> students)
        {
            StudentsNotSettled = "\n---Ordinary Students Settled---\n";
            foreach (var student in students)
            {
                StudentsNotSettled +=
                    $"{student.Name}, {student.Surname}, Birth Date: {student.BirthDate.ToString()}, Faculty: {student.FacultyName}\n";
            }
        }

        public string GetResult()
            => Statistics + BeneficialStudentsSettled + OrdinaryStudentsSettled + StudentsNotSettled;
    }
}