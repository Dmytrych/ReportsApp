namespace ReportsApp.WebApi.Controllers.Dto
{
    public class DormitoryClientDto
    {
        public int Id { get; set; }
        
        public string Number { get; set; }

        public int? CreationYear { get; set; }

        public string Address { get; set; }
        
        public int? Cost { get; set; }
        
        public string Description { get; set; }
    }
}