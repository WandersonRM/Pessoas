using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Pessoas.Models;

namespace Pessoas
{
    public class Program
    {
 
       
        public static void Main(string[] args)
        {
            
            CreateHostBuilder(args)
               .Build()
               .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureAppConfiguration(configurationBuilder => { configurationBuilder.AddEnvironmentVariables(); }) // aqui � onde voc� adiciona outror `EnvironmentVariablesConfigurationSource`
            ;
    }
}
