using System;

namespace Authentication.Module.Dto
{
    public class UserInfo
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public string PasswordHash { get; set; }
        
        public string IsAdmin { get; set; }
        
        public int DormitoryId { get; set; }
    }
}