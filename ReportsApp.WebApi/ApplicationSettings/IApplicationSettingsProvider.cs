namespace ReportsApp.WebApi.ApplicationSettings
{
    public interface IApplicationSettingsProvider
    {
        public AppSettings Get();
    }
}