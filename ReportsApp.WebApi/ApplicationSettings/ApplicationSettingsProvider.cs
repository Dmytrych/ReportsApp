using System;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace ReportsApp.WebApi.ApplicationSettings
{
    public class ApplicationSettingsProvider : IApplicationSettingsProvider
    {
        private const string FileNotFoundMessage = "App settings file was not found";
        private const string AppSettingsResourceName = "ReportsApp.WebApi.AppSettings.json";

        private readonly AppSettings _appSettings;
        
        public ApplicationSettingsProvider()
        {
            _appSettings = ReadSettings();
        }

        public AppSettings Get()
            => _appSettings;

        private AppSettings ReadSettings()
        {
            try
            {
                var settingsStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(AppSettingsResourceName);

                if (settingsStream == null)
                {
                    throw new FileNotFoundException(FileNotFoundMessage);
                }

                using var settingsReader = new StreamReader(settingsStream);

                return JsonSerializer.Deserialize<AppSettings>(settingsReader.ReadToEnd());
            }
            catch (Exception ex)
            {
                throw new Exception(FileNotFoundMessage, ex);
            }
        }
    }
}