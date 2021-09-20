using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using Enterprise.Framework.Hooks;
using OpenQA.Selenium.Safari;
using Enterprise.Framework.Constants;

namespace Enterprise.Framework.Hooks
{
    [Binding]
    public class DriverSetup
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IObjectContainer _objectContainer;
        public IWebDriver WebDriver;
        public RemoteWebDriver RemoteWebDriver;
        public static IWebDriver desktopDriver;
        private String _testRunType = Environment.GetEnvironmentVariable("testrun");
        private String _platform = Environment.GetEnvironmentVariable("platform");
        private String _deviceType = Environment.GetEnvironmentVariable("deviceType");
        private String _bsUserName = Environment.GetEnvironmentVariable("bsUser");
        private String _bsKey = Environment.GetEnvironmentVariable("bsKey");
        private String _bsURL = Environment.GetEnvironmentVariable("bsURL");
        private String browser = Environment.GetEnvironmentVariable("browser");
        private static readonly int _webdriver_implicitTimeout = int.Parse(Environment.GetEnvironmentVariable("webdriver.implicitTimeout"));
        private static readonly int _webdriver_pageLoadTimeout = int.Parse(Environment.GetEnvironmentVariable("webdriver.pageLoadTimeout"));
        private static Uri appiumLocalServer = new Uri("http://127.0.01:4723/wd/hub");
        private String _appURL = Environment.GetEnvironmentVariable("URL");
        private AppiumOptions caps = null;
       // private ScenarioContext _scenarioContext;
        private string _sessionId;


        //public DriverSetup(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        public DriverSetup(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
           // _scenarioContext = scenarioContext;
        }


        [BeforeScenario(Order = 0)]
        public void beforeScenario(IObjectContainer objectContainer)
        {           
            WebDriver = createDriverInstance();            
            this._objectContainer.RegisterInstanceAs(WebDriver);

        }

        public IWebDriver createDriverInstance()
        {
            if(_testRunType.ToLower().Equals("grid"))
            {
                log.Debug("Test run type choosen as" + _testRunType);
                if (_platform.ToLower().Equals("mobile"))
                {
                    log.Debug("Platform choosen as "+_platform);
                    if (_deviceType.ToLower().Equals("android"))
                    {
                        log.Debug("Device type choosen as 'Android'");
                        caps = createAppiumCapabilities(_deviceType);                        
                        WebDriver = new AndroidDriver<AndroidElement>(new Uri(_bsURL), caps);                                          
                    
                    }   
                    else
                    {
                        log.Debug("Device type choosen as 'ios'");
                        caps = createAppiumCapabilities(_deviceType);
                        WebDriver = new IOSDriver<IOSElement>(new Uri(_bsURL), caps);                       
                        
                    }
                    _sessionId = ((RemoteWebDriver)WebDriver).SessionId.ToString();
                    log.Debug("Test Session ID : " + _sessionId);
                    //_scenarioContext.Add("session_id", _sessionId);
                    log.Info("Browser stack execution video recording link :" + FrameworkConstants.BROWSERSTACK_VIDEO_RECORDING_LINK + _sessionId);
                }
                else if (_platform.ToLower().Equals("web"))
                {
                    log.Debug("Platform type choosen as 'WEB'");
                    //BS-Desktop-WEB Integration has to be developed here
                }
                else {
                    log.Error("Platform value has to be given properly in the appSettings.json file");
                    throw new Exception("Platform value has to be given properly in the appSettings.json file"); 
                }

            }
            else
            {
                log.Debug("Test run type choosed as 'local'");
                if (_platform.ToLower().Equals("mobile"))
                {
                    log.Debug("Platform choosen as 'mobile'");
                    if (_deviceType.ToLower().Equals("android"))
                    {
                        log.Debug("Device type choosen as 'Android'");
                        caps = createAppiumCapabilities(_deviceType);
                        /*var AppiumService = new OpenQA.Selenium.Appium.Service.AppiumServiceBuilder().WithIPAddress("127.0.0.1").UsingPort(4723).Build();
                        AppiumService.Start();
                        WebDriver = new AndroidDriver<AndroidElement>(AppiumService, caps);*/
                        WebDriver = new AndroidDriver<AndroidElement>(appiumLocalServer, caps);
                        log.Debug("Android Appium driver created successfully at "+appiumLocalServer);
                        
                    }
                    else
                    {
                        log.Debug("Device type choosen as 'ios'");
                        caps = createAppiumCapabilities(_deviceType);
                        /*var AppiumService = new OpenQA.Selenium.Appium.Service.AppiumServiceBuilder().WithIPAddress("127.0.0.1").UsingPort(4723).Build();
                        AppiumService.Start();
                        WebDriver = new AndroidDriver<AndroidElement>(AppiumService, caps);*/
                        WebDriver = new IOSDriver<IOSElement>(appiumLocalServer, caps);
                        log.Debug("iOS Appium driver created successfully at " + appiumLocalServer);
                    }
                    log.Debug("Device Launched with capabilities " + caps);
                }
                else if (_platform.ToLower().Equals("web"))
                {
                    log.Debug("Platform choosen as 'web'");
                    if (browser.ToLower().Equals("chrome"))
                    {
                        log.Debug("Browser choosen as 'chrome'");
                        ChromeOptions options = new ChromeOptions();
                        options.AddArguments("enable-automation");
                        options.AddArguments("--no-sandbox");
                        WebDriver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(3));
                        WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                        WebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
                        WebDriver.Manage().Window.Maximize();
                        log.Debug("Chrome browser launched successfully with chrome options "+options);

                    }
                    else if (browser.ToLower().Equals("firefox"))
                    {
                        log.Debug("Browser choosen as firefox");
                        WebDriver = new FirefoxDriver();
                        log.Debug("Browser launched successfully");
                    }
                    else {
                        log.Error("No browser value selected in the appSettings.json file");
                        throw new Exception("No browser value selected in the appSettings.json file"); 
                    }
                    WebDriver.Navigate().GoToUrl(_appURL);
                    WebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(_webdriver_pageLoadTimeout);
                    log.Debug("Page load timeout is set to "+_webdriver_pageLoadTimeout);
                }
                else { throw new Exception("No platform value selected in the properties.json file"); }
            }
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_webdriver_implicitTimeout);
            log.Debug("Implicit wait timeout is set to " + _webdriver_implicitTimeout);
            return WebDriver;

        }

        public AppiumOptions createAppiumCapabilities(String driverType)
        {
            AppiumOptions caps = new AppiumOptions();

            if (_testRunType.ToLower().Equals("grid")) {
            log.Debug("Adding browser stack keys to the caps");
            caps.AddAdditionalCapability("browserstack.user", _bsUserName);
            caps.AddAdditionalCapability("browserstack.key", _bsKey);
            caps.AddAdditionalCapability("browserstack.idleTimeout", Environment.GetEnvironmentVariable("browserstack.idleTimeout"));
            }

            caps.AddAdditionalCapability("app", Environment.GetEnvironmentVariable("app"));
            caps.AddAdditionalCapability("appPackage", Environment.GetEnvironmentVariable("appPackage"));
            caps.AddAdditionalCapability("appActivity", Environment.GetEnvironmentVariable("appActivity"));
            caps.AddAdditionalCapability("device", Environment.GetEnvironmentVariable("deviceName"));
            caps.AddAdditionalCapability("os_version", Environment.GetEnvironmentVariable("os_version"));
            caps.AddAdditionalCapability("browser", Environment.GetEnvironmentVariable("browser"));
            caps.AddAdditionalCapability("browserVersion", Environment.GetEnvironmentVariable("browserVersion"));
            caps.AddAdditionalCapability("noReset", false);
            caps.AddAdditionalCapability("fullReset", true);
            
            caps.PlatformName = Environment.GetEnvironmentVariable("platformName");
            if (!(_deviceType.ToLower().Equals("android")))
                caps.AddAdditionalCapability("automationName", "XCUITest");
            return caps;
        }  
       
    }
}
