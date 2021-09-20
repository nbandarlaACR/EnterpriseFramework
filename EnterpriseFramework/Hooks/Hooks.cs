using Enterprise.Framework.GenericLib;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Enterprise.Framework.Hooks
{
    [Binding]
    public class Hooks
    {
        public IWebDriver WebDriver;
        private String _platform = Environment.GetEnvironmentVariable("platform");
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        Hooks(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }
      
        [AfterScenario(Order = 0)]
        public void AfterScenario()
        {
            if (_platform.ToLower().Equals("web"))
            {
                WebDriver.Close();
                WebDriver.Quit();
                WebDriver.Dispose();
            }
            else                                       
                WebDriver.Quit();         
                
        }

        [AfterScenario (Order =1)]
        public void AfterScenario1(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError != null)
            {
                log.Error("Failed with reason " + scenarioContext.TestError);
            }
        }

        [AfterScenario(Order = 2)]
        public void AfterScenario2()
        {
            if (_platform.ToLower().Equals("web"))
            {
                log.Debug("About to kill chrome process in task manager ");
                Utilities.KillChromeDriver();
                log.Debug("Killed the chrome process sucecssfully in task manager ");
            }
        }
       /* public String GetDeviceContext()
        {
            String defaultContext = "Native";
            AndroidDriver<AndroidElement> driver = ((AndroidDriver<AndroidElement>)WebDriver);
            var contexts = ((IContextAware)driver).Contexts;
            string webviewContext = null;
            for (int i = 0; i < contexts.Count; i++)
            {
                Console.WriteLine(contexts[i]);
                if (contexts[i].Contains("WEBVIEW"))
                {
                    webviewContext = contexts[i];
                    driver.Context = webviewContext;
                    return driver.Context;
                }
            }

            return defaultContext;
        }*/
    }
}
