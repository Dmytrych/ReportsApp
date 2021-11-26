using ReportsApp.WebApi.Controllers.Dto;

namespace ReportsApp.WebApi.Controllers.Services
{
    public interface IReportsGenerationService
    {
        ReportResponseDto Generate();
    }
}