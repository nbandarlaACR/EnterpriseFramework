using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Autofac;

using Enterprise.Framework.DataLib;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Diagnostics;

namespace Enterprise.Framework.GenericLib
{
    public static class Utilities
    {
        static dynamic datasample, dtSample;
        static JSONReader r;
        static IContainer _container;

        //public static IConfigurationRoot getConfiguration()
        //{
        //    //jsonPath = Path.GetFullPath(Path.Combine(CommonPageInit.Instance.path, @"..\..\..\Resources\PreProd\" + filename + ".json"));
        //    //var dirPath = Directory.GetCurrentDirectory();

        //    var settingsFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "appsettings.*.json");
        //    if (settingsFiles.Length != 1) throw new Exception($"Expect to have exactly one configuration-specfic settings file, but found {string.Join(", ", settingsFiles)}.");
        //    var settingsFile = settingsFiles.First();

        //    var builder = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //        .AddJsonFile(settingsFile)
        //        .AddEnvironmentVariables();

        //    var configuration = builder.Build();
        //    return configuration;

        //}

        /*public static IContainer getContainer()
        {
            if (_container == null)
            {
                //var configuration = Utilities.getConfiguration();
                var configuration = SelectConfig.get().getConfiguration();

                var builder = new ContainerBuilder();

                builder.RegisterType<LoggerFactory>()
                    .As<ILoggerFactory>()
                    .SingleInstance();

                builder.RegisterGeneric(typeof(Logger<>))
                    .As(typeof(ILogger<>))
                    .SingleInstance();

                builder.RegisterGeneric(typeof(DefaultTraceIdProvider<>))
                    .As(typeof(ITraceIdProvider<>));

                builder.SetupMessaging(configuration,
                    assembliesToInclude: new[] { typeof(AddOrRemovePortalUser).Assembly });

                _container = builder.Build();
            }

            return _container;
        }*/

        public static string generateSurname()
        {
            string surname;
            DateTime dt = DateTime.Now;
            return surname = "TestAuto-" + dt.ToString("yyyyMMddHHmmssfff"); ;
        }

        public static string generateEmailAddress()
        {
            string email;
            DateTime dt = DateTime.Now;
            email = "auto" + dt.ToString("yyyyMMddHHmmssfff") + "@auto.com";
            Console.WriteLine("New account email address is: " + email);
            return email;
        }

        public static string generateEmailAddressForWeblogs()
        {
            string email;
            DateTime dt = DateTime.Now;
            email = "weblogs" + dt.ToString("yyyyMMddHHmmssfff") + "@auto.com"; ;
            Console.WriteLine("New account email address is: " + email);
            return email;
        }


        public static int getPaymentDivisor(string outstandingBalance)
        {
            int divisor;
            Random rnd = new Random();
            double bal = Convert.ToDouble(outstandingBalance);

            if (bal >= 2000 && bal <= 10000)
            {
                divisor = rnd.Next(901, 999);
            }
            else if (bal > 10000 && bal <= 20000)
            {
                divisor = rnd.Next(1901, 1999);
            }
            else if (bal > 20000 && bal <= 30000)
            {
                divisor = rnd.Next(2901, 2999);
            }
            else if (bal > 30000 && bal <= 40000)
            {
                divisor = rnd.Next(3901, 3999);
            }
            else if (bal > 40000 && bal <= 50000)
            {
                divisor = rnd.Next(4901, 4999);
            }
            else
            { divisor = 0; }

            return divisor;
        }

        public static JObject getRelevantDataSampleFull(string TCID, string testDataFileName)
        {
            int i = 0;
            r = new JSONReader();
            if (datasample == null || !(datasample[0]["UseCaseArea"].ToString().Equals(testDataFileName)))
            {
                datasample = r.LoadJson(testDataFileName);
            }
            switch (TCID)
            {
                case "TC001":
                    i = 0;
                    break;
                case "TC002":
                    i = 1;
                    break;
                case "TC003":
                    i = 2;
                    break;
                case "TC004":
                    i = 3;
                    break;
                case "TC005":
                    i = 4;
                    break;
                case "TC006":
                    i = 5;
                    break;
                case "TC007":
                    i = 6;
                    break;
                case "TC008":
                    i = 7;
                    break;
                case "TC009":
                    i = 8;
                    break;
                case "TC010":
                    i = 9;
                    break;
                case "TC011":
                    i = 10;
                    break;
                case "TC012":
                    i = 11;
                    break;
                case "TC013":
                    i = 12;
                    break;
                case "TC014":
                    i = 13;
                    break;
                case "TC015":
                    i = 14;
                    break;
                case "TC016":
                    i = 15;
                    break;
                case "TC017":
                    i = 16;
                    break;
                case "TC018":
                    i = 17;
                    break;
                case "TC019":
                    i = 18;
                    break;
            }
            dtSample = datasample[i];
            return dtSample;
        }
        public static String getCurrentDateTimeMilliSeconds()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

        }
        public static int generateSixDigitRandomNumber()
        {
            Random generator = new Random();
            return generator.Next(100000, 1000000);
        }

        public static int generateThreeDigitRandomNumber()
        {
            Random generator = new Random();
            return generator.Next(100, 1000);
        }

        public static void KillChromeDriver()
        {
            Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");
            foreach (var chromeDriverProcess in chromeDriverProcesses)
            {
                chromeDriverProcess.Kill();
            }
        }
    }
    
}


