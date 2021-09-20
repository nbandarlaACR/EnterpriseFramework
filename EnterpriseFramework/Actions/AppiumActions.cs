using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.iOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;
using System.Drawing;
using OpenQA.Selenium.Appium.MultiTouch;

namespace Enterprise.Framework.Actions
{
    public class AppiumActions : BaseActions
    {
        public AppiumActions(IWebDriver webDriver) : base(webDriver) { }
        
        public AndroidElement getAndoridElement(String locator) => ((AndroidDriver<AndroidElement>)Driver).FindElementByAndroidUIAutomator(locator);
        public void UIAutomatorClick(String locator)
        {
            log.Debug("Trying to perform UI Automator click on element " + locator.ToString());
            WaitFor(ByAndroidUIAutomator.AndroidUIAutomator(locator)).Click();
            log.Debug("Successfully performed UI Automator click on element " + locator.ToString());
        }
        public void Click(By locator) {
            log.Debug("Trying to perform click on element " + locator.ToString());
            WaitFor(locator).Click();
            log.Debug("Successfully performed click operation on element " + locator.ToString());
        }
        public void EnterText(By locator, String inputText)
        {
            log.Debug("Trying to enter text'"+ inputText + "' on element " + locator.ToString());
            WaitFor(locator).SendKeys(inputText);
            log.Debug("Successfully entered text '"+inputText+"' in the element " + locator.ToString());
        }
        public string GetText(By locator)
        {
            log.Debug("Trying to retrieve text from the element " + locator.ToString());
            String text = WaitFor(locator).Text;
            log.Debug("Successfully retrieved text '"+text+"from the element " + locator.ToString());
            return text;
        }
        public void HideKeyboard()
        {
            log.Debug("Trying to Hidekeyboard");
            if (GetDeviceType().Equals(DeviceType.ANDROID.ToString()))
            {
                ((AndroidDriver<AndroidElement>)Driver).HideKeyboard();                
            }
            else
            {
                ((IOSDriver<IOSElement>)Driver).HideKeyboard();
            }
            log.Debug("Successfully performed Hidekeyboard action ");
        }
        public Boolean IsKeyboradPresent()
        {
            log.Debug("Trying to check Keyboard is present");
            if (GetDeviceType().Equals(DeviceType.ANDROID.ToString()))
                return ((AndroidDriver<AndroidElement>)Driver).IsKeyboardShown();
            else
                return ((IOSDriver<IOSElement>)Driver).IsKeyboardShown();
        }
        public void StartChromeActivity()
        {
            log.Debug("Trying to start chrome activity");
            AndroidDriver<AndroidElement> driver = ((AndroidDriver<AndroidElement>)Driver);
            driver.StartActivity("com.android.chrome", "com.google.android.apps.chrome.Main");
            log.Debug("Successfully started chrome activity");

        }

        public void StartLowellAppActivity()
        {
            log.Debug("Trying to start lowell activity");
            AndroidDriver<AndroidElement> driver = ((AndroidDriver<AndroidElement>)Driver);
            driver.StartActivity("com.lowell.app.preprod", "com.lowell.app.AppActivity");
            log.Debug("Successfully started lowell activity");
        }        

        public void SwitchToWebContext()
        {
            log.Debug("Trying to switch to web context");
            Thread.Sleep(5000);
            if (GetDeviceType().Equals(DeviceType.ANDROID.ToString()))
            {
                AndroidDriver<AndroidElement> driver = ((AndroidDriver<AndroidElement>)Driver);
                var contexts = ((IContextAware)driver).Contexts;
                string webviewContext = null;
                for (int j=0; j < contexts.Count; j++)
                {
                    Console.WriteLine(contexts[j]);
                    log.Info("The available contexts are " + contexts[j]);
                }
                
                for (int i = 0; i < contexts.Count; i++)
                {
                    Console.WriteLine(contexts[i]);
                    log.Info("Contexts are "+i+". "+ contexts[i]);
                    if (contexts[i].Contains("WEBVIEW_chrome"))
                    {
                        webviewContext = contexts[i];
                        driver.Context = webviewContext;
                        log.Info("webviewContext is " + webviewContext);
                        break;
                    }
                }
                Thread.Sleep(3000);                
            }
            else
            {
                IOSDriver<IOSElement> driver = ((IOSDriver<IOSElement>)Driver);
                var contexts = ((IContextAware)driver).Contexts;
                Console.WriteLine("contexts are  " + contexts.Count);

                string webviewContext = null;
                for (int i = 0; i < contexts.Count; i++)
                {
                    Console.WriteLine(contexts[i]);
                    if (contexts[i].Contains("WEBVIEW"))
                    {
                        webviewContext = contexts[i];
                        driver.Context = webviewContext;
                        break;
                    }
                }
                Thread.Sleep(3000);
            }
            log.Debug("Successfully switched to web context");
        }

        public void SwitchToNativeContext()
        {
            log.Debug("Trying to switch to native context");
            Thread.Sleep(3000);
            AndroidDriver<AndroidElement> driver = ((AndroidDriver<AndroidElement>)Driver);
            var contexts = ((IContextAware)driver).Contexts;
            string nativeContext = null;
            for (int i = 0; i < contexts.Count; i++)
            {
                Console.WriteLine(contexts[i]);
                if (contexts[i].Contains("NATIVE"))
                {
                    nativeContext = contexts[i];
                    driver.Context = nativeContext;
                    break;
                }
            }
            Thread.Sleep(3000);
            log.Debug("Successfully switched to native context");
        }

        public void ScrollToElement(IWebElement webElement)
        {
            TouchActions action = new TouchActions(Driver);
            action.Scroll(webElement, 10, 100);
            action.Perform();
        }

        public void JavaScriptScroll()
        {
            Dictionary<string, string> scrollObject = new Dictionary<string, string>();
            scrollObject.Add("direction", "down");
            ((IJavaScriptExecutor)Driver).ExecuteScript("mobile: scroll", scrollObject);
        }
        public void ScrollToBottom()
        {
            Size dimensions = Driver.Manage().Window.Size;
            double screenHeightStart = (dimensions.Height * 0.5);
            double screenHeightEnd = dimensions.Height * 0.2;

            ITouchAction touchAction = new TouchAction((IPerformsTouchActions)Driver)
            .Press(0, screenHeightStart)
            .Wait(500)
            .MoveTo(0 - 0, screenHeightStart - screenHeightEnd)
            .Release();

            touchAction.Perform();
        }
    }
}
