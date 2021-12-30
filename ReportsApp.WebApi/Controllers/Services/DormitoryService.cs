using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using ReportsApp.WebApi.Controllers.Domain;
using ReportsApp.WebApi.Controllers.Dto;

namespace ReportsApp.WebApi.Controllers.Services
{
    public class DormitoryService : IDormitoryService
    {
        private string InfoUri = "http://localhost:5002/FacultyInfoApi/get-all";
        private string CostsUri = "http://localhost:5004/FacultyDetailsApi/get-costs";
        private string DetailsUri = "http://localhost:5004/FacultyDetailsApi/get-description";

        private readonly IDormitoryContext _dbContext;

        public DormitoryService(IDormitoryContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IReadOnlyCollection<DormitoryClientDto> GetAll()
        {
            var dormitories = _dbContext.Dormitories.ToList();

            var dormInfo = GetAllInfo();

            var dormCosts = GetPrices();

            var result = dormitories.Select(d =>
            {
                var info = dormInfo.FirstOrDefault(dInfo => dInfo.number == d.Number);
                var cost = dormCosts.FirstOrDefault(dInfo => dInfo.number == d.Number);
                var description = GetDescription(d.Number);

                return new DormitoryClientDto
                {
                    Address = info?.address,
                    CreationYear = info?.creationYear,
                    Number = d.Number,
                    Id = d.Id,
                    Cost = cost?.cost,
                    Description = description
                };
            });

            return result.ToList();
        }
        
        public IReadOnlyCollection<DormitoryInfoModel> GetAllInfo()
        {
            var res = WebRequest.CreateHttp(InfoUri).GetResponse();

            using Stream stream = res.GetResponseStream();
            using StreamReader reader = new StreamReader(stream);

            try
            {
                return JsonSerializer.Deserialize<List<DormitoryInfoModel>>(reader.ReadToEnd());
            }
            catch (JsonException)
            {
                Console.WriteLine("Data from url could not be reached: " + InfoUri);
                return new List<DormitoryInfoModel>();
            }
        }
        
        public IReadOnlyCollection<DormitoryСostsInfoModel> GetPrices()
        {
            var res = WebRequest.CreateHttp(CostsUri).GetResponse();

            using Stream stream = res.GetResponseStream();
            using StreamReader reader = new StreamReader(stream);

            try
            {
                var str = reader.ReadToEnd();
                return JsonSerializer.Deserialize<List<DormitoryСostsInfoModel>>(str);
            }
            catch (JsonException)
            {
                Console.WriteLine("Data from url could not be reached: " + DetailsUri);
                return new List<DormitoryСostsInfoModel>();
            }
        }
        
        public string GetDescription(string number)
        {
            var res = WebRequest.CreateHttp(DetailsUri + "/" + number).GetResponse();

            using Stream stream = res.GetResponseStream();
            using StreamReader reader = new StreamReader(stream);

            try
            {
                return reader.ReadToEnd();
            }
            catch (JsonException)
            {
                Console.WriteLine("Data from url could not be reached: " + DetailsUri);
                return String.Empty;
            }
        }
    }
    
    public class DormitoryInfoModel
    {
        public int id { get; set; }
        
        public string number { get; set; }

        public int creationYear { get; set; }

        public string address { get; set; }
    }
    
    public class DormitoryСostsInfoModel
    {
        public string number { get; set; }
        
        public int cost { get; set; }
    }
}