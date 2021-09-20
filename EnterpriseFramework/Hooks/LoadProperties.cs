using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Enterprise.Framework.Hooks

{
    [Binding]
    public class LoadProperties
    {
        //public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static IConfigurationRoot _configuration;       

        [BeforeTestRun(Order = -100)]
        public static IConfigurationRoot getProjectProperties()
        {
            var settingsFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "appsettings.*.json");
            Console.WriteLine("Config files are ==> "+settingsFiles.Length);
            if (settingsFiles.Length != 1)
            {
                //log.Error($"Expect to have exactly one appsettings config file, but found {string.Join(", ", settingsFiles)}.");
                throw new Exception($"Expect to have exactly one appsettings config file, but found {string.Join(", ", settingsFiles)}.");
            }
            var settingsFile = settingsFiles.First();

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile(settingsFiles.First())
                .AddEnvironmentVariables();

            var configuration = builder.Build();
            _configuration = configuration;          
          

            var keys = builder.Build().AsEnumerable().ToList();

            foreach (var key in keys)
            {                
                if (Environment.GetEnvironmentVariable(key.Key) == "" || Environment.GetEnvironmentVariable(key.Key) == null)
                {
                    //log.Debug("Environment value not found for the key '" + key.Key + "' hence setting it");
                    Environment.SetEnvironmentVariable(key.Key, key.Value);
                }
                //log.Debug("Environment values are " + Environment.GetEnvironmentVariable(key.Key));
            }          
            return configuration;

        }
    }
}
