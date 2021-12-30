using System.Collections.Generic;
using ReportsApp.WebApi.Controllers.Dto;

namespace ReportsApp.WebApi.Controllers.Services
{
    public interface IDormitoryService
    {
        IReadOnlyCollection<DormitoryClientDto> GetAll();
    }
}