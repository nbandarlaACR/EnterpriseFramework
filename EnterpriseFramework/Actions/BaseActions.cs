using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Enterprise.Framework.Actions
{
    
    public class BaseActions
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly int _webdriver_explicitTimeout = int.Parse(Environment.GetEnvironmentVariable("webdriver.explicitTimeout"));
        public static readonly int _webdriver_pollingTimeout = int.Parse(Environment.GetEnvironmentVariable("webdriver.pollingTimeout"));
        public WebDriverWait webDriverWait => new WebDriverWait(Driver, TimeSpan.FromSeconds(_webdriver_explicitTimeout));
        public IWebDriver Driver { get; }

        public BaseActions(IWebDriver webDriver)
        {
            Driver = webDriver;
        }

        public enum DeviceType
        {
            ANDROID,
            IOS
        }

        public IWebDriver getDriver() => Driver;

        public IWebElement WaitFor(By by)
        {
            IWebElement webElement = null;
            try
            {
                webDriverWait.PollingInterval = TimeSpan.FromSeconds(_webdriver_pollingTimeout);
                webDriverWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                log.Debug("Waiting for the element " + by.ToString());
                webElement = webDriverWait.Until(drv => drv.FindElement(by));
                log.Debug("Element found successfully " + by.ToString());
                return webElement;
            }catch(Exception ex)
            {
                log.Error("Unable to locate element " + by.ToString());
                throw new Exception(ex.StackTrace);
            }
        }
        public IWebElement WaitFor(By by, int maxWaitTime)
        {
            IWebElement webElement = null;
            try
            {
                WebDriverWait webDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(maxWaitTime));
                webDriverWait.PollingInterval = TimeSpan.FromSeconds(_webdriver_pollingTimeout);
                webDriverWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                log.Debug("Waiting for the element " + by.ToString());
                webElement = webDriverWait.Until(drv => drv.FindElement(by));
                log.Debug("Element found successfully " + by.ToString());
                return webElement;
            }
            catch (Exception ex)
            {
                log.Error("Unable to locate element " + by.ToString());
                throw new Exception(ex.StackTrace);
            }
           
        }
        public IList<IWebElement> WaitForElements(By by)
        {
            IList<IWebElement> webElements = null;
            try
            {
                webDriverWait.PollingInterval = TimeSpan.FromSeconds(_webdriver_pollingTimeout);
                webDriverWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                log.Debug("Waiting for the element " + by.ToString());
                webElements =  webDriverWait.Until(drv => drv.FindElements(by));
                log.Debug("Elements found successfully " + by.ToString());
                return webElements;
            }
            catch(Exception ex)
            {
                log.Error("Unable to locate element " + by.ToString());
                throw new Exception(ex.StackTrace);
            }
            
        }
        
        public static String GetDeviceType() => Environment.GetEnvironmentVariable("deviceType").ToUpper();        
        public IWebElement GetWebElement(IWebDriver driver, By by) => driver.FindElement(by);
        public IWebElement WaitUntilElementClickable(By by) => webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        public IWebElement WaitUntilElementClickable(IWebElement element) => webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));        
        public Boolean VerifyTextPresent(By by, String text) => webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(by, text));
        public IWebElement WaitUntilElementVisible(By by) => webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        public Boolean VerifyTitleContains(By by, String title) => webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains(title));
        public Boolean VerifyElementNotVisible(By by) => webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(by));
        public Boolean VerifyElementEnabled(By by) => webDriverWait.Until(d=> d.FindElement(by).Enabled);        
        public IList<IWebElement> VerifyElementsPresent(By by) => webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
        public Boolean VerifyStaleElementPresent(IWebElement webElement) => webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(webElement));
        public IList<IWebElement> WaitUntilPresenceOfElementsLocated(By by) => webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
        public Boolean WaitUntilElementDisplayed(By by) => webDriverWait.Until(d => d.FindElement(by).Displayed);
        public Boolean VerifyStaleElementExist(By by)
        {
            IWebElement webElement = WaitFor(by);
            try
            {
                return !webElement.Displayed;
            }
            catch (StaleElementReferenceException elementHasDisappeared)
            {
                return true;
            }
        }
        public Boolean VerifyElementDisplayed(By by)
        {
            try
            {
                log.Debug("Waiting for the element to be displayed " + by.ToString());
                webDriverWait.Until(d => d.FindElement(by).Displayed);
                log.Debug("Element displayed successfully " + by.ToString());
                return true;
            }
            catch (Exception e)
            {
                log.Error("Element NOT displayed"+ by.ToString());
                return false;
            }
            
        }

        public string GetText(By locator)
        {
            log.Debug("Trying to retrieve text from the element " + locator.ToString());
            String text = WaitFor(locator).Text;
            log.Debug("Successfully retrieved text '" + text + "from the element " + locator.ToString());
            return text;
        }

    }
}

