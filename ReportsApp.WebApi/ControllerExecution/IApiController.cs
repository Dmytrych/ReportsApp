using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;

namespace ReportsApp.WebApi.ControllerExecution
{
    public interface IApiController
    {
        Dictionary<string, (Func<IApiChainParameter, ApiChainExecutionResult>, bool)> GetActionInfo();
        
        string GetControllerName();
        
        static (bool isValid, TValue result) ParseDto<TValue>(Stream dataStream) where TValue : class 
        {
            try
            {
                using var reader = new StreamReader(dataStream);
                var result = JsonSerializer.Deserialize<TValue>(reader.ReadToEnd());
                return (result != null, result);
            }
            catch
            {
                return (false, null);
            }
        }
    }
}