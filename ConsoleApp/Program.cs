using System;
using System.IO;
using BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile(@"appsettings.json", false)
                .Build();

            switch (config["realization"])
            {
                case "ado.net":
                    var adoNetService = (IDatabaseService)new DependencyResolver.DependencyResolver().CreateAdoNetDataBaseConverter().GetService(typeof(IDatabaseService));
                    adoNetService.Convert(config["filename"]);
                    adoNetService.GetStat();
                    break;
                case "entity":
                    var entityService = (IDatabaseService)new DependencyResolver.DependencyResolver().CreateEntityDataBaseConverter().GetService(typeof(IDatabaseService));
                    entityService.Convert(config["filename"]);
                    entityService.GetStat();
                    break;
            }
            
        }
    }
}
