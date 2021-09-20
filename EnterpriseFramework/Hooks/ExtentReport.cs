using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Enterprise.Framework.GenericLib;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.IO;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;


namespace Enterprise.Framework.Hooks
{
    [Binding]
    public class ExtentReport
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        [ThreadStatic]
        private static ExtentTest _featureName;
        [ThreadStatic]
        private static ExtentTest _scenario;
        private static ExtentReports _extent;
        public static string ReportPath;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private static String _testResultsPath = "";
        private static String _screenshotsPath = "";
        private IWebDriver driver;

        ExtentReport(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Console.WriteLine("I am in before test run order 0");
            String basePath = Regex.Replace(System.AppContext.BaseDirectory.ToString(), "bin(.*)", "");
            _testResultsPath = basePath + "TestResults\\";
            _screenshotsPath = _testResultsPath + "\\Screenshots";
            if (!Directory.Exists(_testResultsPath))
            {
                Directory.CreateDirectory(_testResultsPath);
            }
            if (!Directory.Exists(_screenshotsPath))
            {
                Directory.CreateDirectory(_screenshotsPath);
            }
            else
            {
                // Delete all files in a directory.
                string[] files = Directory.GetFiles(_screenshotsPath);
                foreach (string file in files)
                {
                    File.Delete(file);                    
                    log.Info($"{file} is deleted.");
                }
            }
            //log.Info("base path is " + basePath);
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(_testResultsPath);
            //ExtentV3HtmlReporter htmlReporter = new ExtentV3HtmlReporter(_testResultsPath+"//Results.html");
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            htmlReporter.Config.ReportName = "ACR-TestExecution-Results";
            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);
            _extent.AddSystemInfo("Environment", Environment.GetEnvironmentVariable("Environment"));
            _extent.AddSystemInfo("Device Name", Environment.GetEnvironmentVariable("deviceName"));
            _extent.AddSystemInfo("Device Version", Environment.GetEnvironmentVariable("os_version"));
            log.Debug("Test execution results can be found from the path "+ _testResultsPath+"\\index.html");
            log.Debug("Test execution logs can be found from the path "+ basePath+"\\test-execution-logs.log");            
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {            
            _featureName = _extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public static void BeforeScenario(ScenarioContext scenarioContext)
        {
            _scenario = _featureName.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);            
            log.Debug("Extent report scenario node created ");
        }        
        [AfterStep]
        public void InsertReportingSteps(ScenarioContext scenarioContext)
        {

            //var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            //log.Debug("Extent report steps node created ");
            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            if (scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text);                    
                }
                else if (stepType == "When")
                    _scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "Then")
                    _scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "And")
                    _scenario.CreateNode<And>(scenarioContext.StepContext.StepInfo.Text);
            }
            else if(scenarioContext.TestError != null)
            {
                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                    _scenario.AddScreenCaptureFromPath(TakeScreenshot(), "FailedScreeshot");
                    /*_scenario.Log(Status.Fail, "<pre>" + scenarioContext.TestError.ToString() + "</pre>");*/
                }
                else if(stepType == "When")
                {
                    _scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                    _scenario.AddScreenCaptureFromPath(TakeScreenshot(), "FailedScreeshot");
                }
                else if(stepType == "Then") {
                    _scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                    _scenario.AddScreenCaptureFromPath(TakeScreenshot(), "FailedScreeshot");
                }
                else if(stepType == "And")
                {
                    _scenario.CreateNode<And>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                    _scenario.AddScreenCaptureFromPath(TakeScreenshot(), "FailedScreeshot");
                }
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extent.Flush();
        }

        public String TakeScreenshot()
        {
            string path = _screenshotsPath + "\\FailedScreenshot-" + Utilities.generateSixDigitRandomNumber() + ".png";
            log.Debug("Screenshots captured under the path "+path);
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
            return path;

        }


    }
}
