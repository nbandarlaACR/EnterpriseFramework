using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]
namespace Enterprise.Framework.Hooks
{
    [Binding]
    public class Logger
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ScenarioContext _scenarioContext;
       /* Logger(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }*/
        [BeforeTestRun(Order = -99)]
        public static void BeforeTestRun()
        {
            String customPattern = null;
            String scenarioName = Environment.GetEnvironmentVariable("scenarioTag");
            //String sessionId = _scenarioContext.Get<String>("session_id");
            String sessionId = "fsdfsdfdsfsdfsdfsd";
            if (scenarioName == null || scenarioName.Equals(""))
            {
                scenarioName = "NOT_YET_CREATED";
            }
            if (Environment.GetEnvironmentVariable("platform").Equals("mobile"))
            {
                String deviceName = Environment.GetEnvironmentVariable("deviceName");
                if (deviceName == null || deviceName.Equals(""))
                {
                    deviceName = "NOT_YET_CREATED";
                }
                String platformName = Environment.GetEnvironmentVariable("platformName");
                if (platformName == null || platformName.Equals(""))
                {
                    platformName = "NOT_YET_CREATED";
                }
                customPattern = "Tag : " + scenarioName + " | " + " Platform : " + platformName + " | " + " Device : " + deviceName + " | " + " SessionId : " + sessionId;
            }
            else
            {
                String browserName = Environment.GetEnvironmentVariable("browser");
                if (browserName == null || browserName.Equals(""))
                {
                    browserName = "NOT_YET_CREATED";
                }
                String platformName = Environment.GetEnvironmentVariable("platformName");
                if (platformName == null || platformName.Equals(""))
                {
                    platformName = "NOT_YET_CREATED";
                }
                customPattern = "Tag : " + scenarioName + " | " + " Platform : " + platformName + " | " + " Browser : " + browserName + " | " + " SessionId : " + sessionId;
            }
            //log4net.GlobalContext.Properties["customproperty"] = customPattern;
            log4net.ThreadContext.Properties["customproperty"] = customPattern;
            //log4net.LogicalThreadContext.Properties["customproperty"] = customPattern;
        }
        [BeforeScenario(Order = -98)]
        public void beforeScenario(ScenarioContext scenarioContext)
        {
            String customPattern = null;
            //String sessionId = _scenarioContext.Get<String>("session_id");
            String sessionId = "fsdfsdfdsfsdfsdfsd";
            if (Environment.GetEnvironmentVariable("platform").Equals("mobile"))
            {
                customPattern = "Tag : " + scenarioContext.Get<String>("scenarioTag") + " | " + " Platform : " + Environment.GetEnvironmentVariable("platformName") + " | " + " Device : " + Environment.GetEnvironmentVariable("deviceName") + " | " + " SessionId : " + sessionId; ;

            }
            else
            {
                // customPattern = scenarioContext.ScenarioInfo.Title.ToString() + " - " + Environment.GetEnvironmentVariable("browser");
                customPattern = "Tag : " + scenarioContext.Get<String>("scenarioTag") + " | " + " Platform : " + Environment.GetEnvironmentVariable("platform") + " | "+ " Browser : " + Environment.GetEnvironmentVariable("browser") + " | " + " SessionId : " + sessionId; ;
            }
            //log4net.GlobalContext.Properties["customproperty"] = customPattern;
            log4net.LogicalThreadContext.Properties["customproperty"] = customPattern;

        }

        

        [BeforeScenario(Order = -99)]
        public void findScenarioTags(ScenarioContext scenarioContext)
        {
            Console.WriteLine("I am in before scenario order -99");
            scenarioContext.Add("scenarioTag", GetScenarioTag(scenarioContext));
            Environment.SetEnvironmentVariable("scenarioTag", GetScenarioTag(scenarioContext));
        }

        public string GetScenarioTag(ScenarioContext scenarioContext)
        {
            string scenaro_tag = null;
            string[] scenarioTags = scenarioContext.ScenarioInfo.Tags;


            foreach (var scenarioTag in scenarioTags)
            {
                if (scenarioTag.Contains("test-"))
                {
                    scenaro_tag = scenarioTag.Split("test-")[1];
                    break;

                }
            }
            if (scenaro_tag == null)
            {
                throw new Exception("No scenario tags found with prefix 'test-'");
            }

            return scenaro_tag;
        }

    }
}
