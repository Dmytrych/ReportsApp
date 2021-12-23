using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using ReportsApp.WebApi.Controllers.Domain.Dto;

namespace ReportsApp.WebApi.Controllers.Domain.UserRepository
{
    public class ExternalUserRepository : IExternalUserRepository
    {
        private List<string> ServiceUris = new List<string>
        {
            "localhost:5002"
        };
        
        public IReadOnlyCollection<User> GetUsers()
        {
            var users = new List<User>();

            foreach (var uri in ServiceUris)
            {
                users.AddRange(GetSingleSourceUsers(uri));
            }

            return users;
        }

        private IReadOnlyCollection<User> GetSingleSourceUsers(string uri)
        {
            var res = WebRequest.CreateHttp(uri).GetResponse();

            using Stream stream = res.GetResponseStream();
            using StreamReader reader = new StreamReader(stream);

            try
            {
                return JsonSerializer.Deserialize<List<User>>(reader.ReadToEnd());
            }
            catch (JsonException)
            {
                Console.WriteLine("Data from url could not be reached: " + uri);
                return new List<User>();
            }
        }
    }
}