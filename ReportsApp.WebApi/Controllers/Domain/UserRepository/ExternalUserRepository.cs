using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using ReportsApp.Authentication.Dto;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ReportsApp.WebApi.Controllers.Domain.UserRepository
{
    public class ExternalUserRepository : IExternalUserRepository
    {
        private readonly string[] _uris = new[] {"http://localhost:5002/"};
        
        public  IReadOnlyCollection<UserClientDto> GetAll()
        {
            var users = new List<UserClientDto>();
            
            foreach (var uri in _uris)
            {
                var response = WebRequest.CreateHttp(uri).GetResponse();

                var responseStream = response.GetResponseStream();

                if (responseStream == null)
                {
                    continue;
                }

                using var reader = new StreamReader(responseStream);
                var content = reader.ReadToEnd();
                Console.WriteLine(content);
            
                try
                {
                    users.AddRange(JsonSerializer.Deserialize<List<UserClientDto>>(content) ?? new List<UserClientDto>());
                }
                catch (JsonException)
                {
                    continue;
                }
            }

            return users;
        }
    }
}