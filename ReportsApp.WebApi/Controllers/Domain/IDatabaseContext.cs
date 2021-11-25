namespace ReportsApp.WebApi.Controllers.Domain
{
    public interface IDatabaseContext
    {
        int SaveChanges();
    }
}