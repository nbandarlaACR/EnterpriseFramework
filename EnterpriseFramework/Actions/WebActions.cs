using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Enterprise.Framework.Actions
{
    public class WebActions : BaseActions
    {

        public WebActions(IWebDriver webDriver) : base(webDriver)
        {
        }

        public void Click(By by)
        {
            try
            {
                log.Debug("Trying to perform click operation on element " + by.ToString());
                WaitFor(by).Click();
                log.Debug("Successfully performed click operation on element " + by.ToString());
            }
            catch (StaleElementReferenceException ex) {
                log.Debug("Encountered Stale element exeception upon on clicking the element " + by.ToString());
                log.Error(ex);
                var element = WaitFor(by);
                element.Click();
                log.Debug("Successfully performed click operation after stale element exception on element " + element.ToString());             
            }
           
        }

        public void WaitForStaleElementAndClick_1(By by)
        {           
            if (!(VerifyStaleElementPresent(WaitFor(by))))
            {
                log.Debug("Trying to perform click operation on element " + by.ToString());
                Driver.FindElement(by).Click();
                log.Debug("Successfully performed click operation on element " + by.ToString());
            }
            else
            {
                log.Debug("Stale element found " + by.ToString());
                log.Debug("Trying to wait until element to be re attached to the DOM " + by.ToString());
                WaitUntilPresenceOfElementsLocated(by);
                WaitUntilElementDisplayed(by);
                Driver.FindElement(by).Click();
                log.Debug("Stale element handled and element re attached to the DOM successfully " + by.ToString());
                log.Debug("Successfully performed click operation on element " + by.ToString());
            }

        }        
        public void WaitForStaleElementAndClick_2(By by)
        {
            IWebElement webElement = null;
            try
            {
                webElement = WaitFor(by);
                log.Debug("Trying to perform click operation on element " + by.ToString());
                webElement.Click();
                log.Debug("Successfully performed click operation on element " + by.ToString());
            }
            catch (StaleElementReferenceException ex)
            {
                log.Debug("Encountered Stale element exeception upon on clicking the element " + by.ToString());
                log.Error(ex);
                if (VerifyStaleElementPresent(webElement)){
                    log.Debug("Trying to wait until element re attached to the DOM " + by.ToString());
                    WaitUntilPresenceOfElementsLocated(by);
                    WaitUntilElementDisplayed(by);
                    Driver.FindElement(by).Click();
                    log.Debug("Stale element handled and element re attached to the DOM successfully " + by.ToString());
                    log.Debug("Successfully performed click operation on element " + by.ToString());
                }
                else
                {
                    log.Debug("Stale element is not present");
                }               
            }
        }

        public void WaitForStaleElementAndClick_3(By by)
        {
            IWebElement webElement = null;
            try
            {
                webElement = WaitFor(by);
                log.Debug("Trying to perform click operation on element " + by.ToString());
                webElement.Click();
                log.Debug("Successfully performed click operation on element " + by.ToString());
            }
            catch (StaleElementReferenceException ex)
            {
                log.Debug("Encountered Stale element exeception upon on clicking the element " + by.ToString());
                log.Error(ex);
                log.Debug("Trying to handle Stale element exeception with try catch for loop");
                ClickUntilStaleElementGone(webElement);
            }
        }

        public void ClickUntilStaleElementGone(IWebElement webElement)
        {
            for(int i=1; i<=10; i++)
            {
                if (VerifyStaleElementPresent(webElement))
                {
                    log.Debug("Stale element found " + webElement.ToString());
                    try
                    {
                        log.Debug("Trying to perform click operation on element " + webElement.ToString());
                        webElement.Click();
                        log.Debug("Successfully performed click operation on element " + webElement.ToString());
                    }
                    catch(StaleElementReferenceException ex)
                    {
                        log.Debug("Stale element reference exception occured for '"+i+"' number of times");
                        log.Debug("Handeling Stale element reference exception with catch block");
                    }
                }
                else
                {
                    log.Debug("Stale element NOT found " + webElement.ToString());
                    break;
                }
            }

        }

        public void Click(IWebElement element)
        {
            try
            {
                WaitForItem(Driver, element);
                log.Debug("Trying to perform click operation on element " + element.ToString());
                element.Click();
                log.Debug("Successfully performed click operation on element " + element.ToString());
            }
            catch (StaleElementReferenceException ex)
            {
                log.Debug("Encountered Stale element exeception upon on clicking the element " + element.ToString());
                log.Error(ex);
                WaitForItem(Driver, element);
                element.Click();
                log.Debug("Successfully performed click operation after stale element exception on element " + element.ToString());
            }
            
        }

        public void WebElementClick(By by)
        {
            WaitUntilClickable(Driver, by);
            var element = Driver.FindElement(by);
            try
            {
                log.Debug("Trying to perform click operation on element " + element.ToString());
                element.Click();
                log.Debug("Successfully performed click on element " + by.ToString());
            }

            catch (Exception e) when (e is InvalidOperationException || e is ElementClickInterceptedException || e is StaleElementReferenceException)
            {
                log.Error(e);
                WaitForItem(Driver, by);
                element = Driver.FindElement(by);
                element.Click();
                log.Debug("Successfully performed click on element upon exception " + by.ToString());
            }
        }

        public void WebElementClick(IWebElement element)
        {
            WaitUntilClickable(Driver, element);
            try
            {
                log.Debug("Trying to perform click operation on element " + element.ToString());
                element.Click();
                log.Debug("Successfully performed click on element " + element.ToString());
            }

            catch (Exception e) when (e is InvalidOperationException || e is ElementClickInterceptedException || e is StaleElementReferenceException)
            {
                log.Error(e);
                WaitForItem(Driver, element);
                element.Click();
                log.Debug("Successfully performed click on element upon exception " + element.ToString());
            }
        }

        public void CheckBoxClick(By by)
        {
            var element = Driver.FindElement(by);
            try
            {
                log.Debug("Trying to perform click operation on element " + element.ToString());
                element.Click();
                log.Debug("Successfully performed click on chechbox " + by.ToString());
            }

            catch (Exception e) when (e is InvalidOperationException || e is ElementClickInterceptedException || e is StaleElementReferenceException)
            {
                log.Error(e);
                WaitForItem(Driver, element);
                element.Click();
                log.Debug("Successfully performed click on element upon exception " + element.ToString());
            }
        }
        public void CheckBoxClick(IWebElement element)
        {
            try
            {
                log.Debug("Trying to perform click operation on element " + element.ToString());
                element.Click();
                log.Debug("Successfully performed click on chechbox " + element.ToString());
            }

            catch (Exception e) when (e is InvalidOperationException || e is ElementClickInterceptedException || e is StaleElementReferenceException)
            {
                log.Error(e);                
                WaitForItem(Driver, element);
                element.Click();
                log.Debug("Successfully performed click on chechbox upon exception " + element.ToString());
            }
        }

        public void SetCheckBoxByList(List<IWebElement> checkBoxList, String setCheckBoxVal)
        {
            String attributeValue = "";
            log.Debug("Trying to set multiple checkboxes on");

            foreach (IWebElement el in checkBoxList)
            {
                attributeValue = GetElementAttribute(el, "checked");
                if (string.IsNullOrEmpty(attributeValue) && setCheckBoxVal == "true")
                {
                    CheckBoxClick(el);
                    ScrollElementIntoViewWebElm(el, 2, "plus");
                    Sleep(1);
                }

                else if (attributeValue == "true" && setCheckBoxVal == "false")
                {
                    CheckBoxClick(el);
                    Sleep(1);
                }
            }

            log.Debug("Successfully set multiple checkboxes on");
        }

        public void SetCheckBox(IWebElement element, String setCheckBoxVal)
        {
            log.Debug("Trying to set a single checkbox on");
            String attributeValue = "";
            ScrollToWebElement(element);
            attributeValue = GetElementAttribute(element, "checked");
            if (string.IsNullOrEmpty(attributeValue) && setCheckBoxVal == "true")
            {
                CheckBoxClick(element);
                ScrollElementIntoViewWebElm(element, 2, "plus");
            }

            else if (attributeValue == "true" && setCheckBoxVal == "false")
            {
                CheckBoxClick(element);
            }

            log.Debug("Successfully set a single checkbox on");
        }

        public void SetCheckBox(By by, String setCheckBoxVal)
        {
            log.Debug("Trying to set a single checkbox on");
            String attributeValue = "";
            IWebElement element = WaitFor(by);
            WaitUntilElementClickable(by);
            ScrollToWebElement(element);
            attributeValue = GetElementAttribute(element, "checked");
            if (string.IsNullOrEmpty(attributeValue) && setCheckBoxVal == "true")
            {
                CheckBoxClick(element);
                ScrollElementIntoViewWebElm(element, 2, "plus");
            }

            else if (attributeValue == "true" && setCheckBoxVal == "false")
            {
                CheckBoxClick(element);
            }

            log.Debug("Successfully set a single checkbox on");
        }

        public void Click(By by, int time = 30)
        {
            log.Debug("Trying to  perform click operation on element " + by.ToString());
            WaitUntilElementClickable(by).Click();
            log.Debug("Successfully performed click operation on element " + by.ToString());
        }

        public void ClickWithJS(By by, int time = 30)
        {
            log.Debug("Trying to  perform JS click operation on element " + by.ToString());
            WaitUntilItemEnabledAndDisplayed(Driver, by, time);
            var el = Driver.FindElement((by));
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", el);
            log.Debug("Successfully performed JS click operation on element " + by.ToString());
        }

        public void ClickWithJS(IWebElement element, int time = 30)
        {
            log.Debug("Trying to  perform JS click operation on element " + element.ToString());
            WaitForItem(Driver, element, time);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", element);
            log.Debug("Successfully performed JS click operation on element " + element.ToString());
        }

        public void EnterText(By by, String text)
        {
            log.Debug("Trying to enter text '" + text + "' in the element " + by.ToString());
            WaitForItem(Driver, by);
            var element = Driver.FindElement(by);
            element.SendKeys(text);
            log.Debug("Successfully entered text '" + text + "' in the element " + by.ToString());
        }

        public void EnterText(IWebElement element, String text)
        {
            log.Debug("Trying to enter text '" + text + "' in the element " + element.ToString());
            WaitForItem(Driver, element);
            element.SendKeys(text);
            log.Debug("Successfully entered text '" + text + "' in the element " + element.ToString());
        }

        public void EnterTextWithWait(IWebElement element, string text)
        {
            log.Debug("Trying to enter text '" + text + "' in the element " + element.ToString());
            WaitForItem(Driver, element);
            element.SendKeys(text);
            log.Debug("Successfully entered text '" + text + "' in the element " + element.ToString());
        }

        public void EnterTextWithWait(By by, string text)
        {
            log.Debug("Trying to enter text '" + text + "' in the element " + by.ToString());
            WaitForItem(Driver, by);
            var element = Driver.FindElement(by);
            element.SendKeys(text);
            log.Debug("Successfully entered text '" + text + "' in the element " + by.ToString());
        }

        public void ClearText(By by)
        {
            log.Debug("Trying to clear text in the text box '" + by.ToString());
            WaitForItem(Driver, by);
            var element = Driver.FindElement(by);
            element.Clear();
            log.Debug("Successfully cleared text in the text box '" + by.ToString());
        }

        public void ClearText(IWebElement element)
        {
            log.Debug("Trying to clear text in the text box '" + element.ToString());
            WaitForItem(Driver, element);
            element.Clear();
            log.Debug("Successfully cleared text in the text box '" + element.ToString());
        }

        public string GetTextUntilItemDisplayed(By by)
        {
            log.Debug("Trying to retrieve text from the element " + by.ToString());
            WaitForItem(Driver, by);
            string text = GetWebElement(Driver, by).Text;
            log.Debug("Successfully retrieved text '" + text + "from the element " + by.ToString());
            return text;
        }

        public string GetTextUntilItemDisplayed(IWebElement element)
        {
            log.Debug("Trying to retrieve text from the element " + element.ToString());
            WaitForItem(Driver, element);
            string text = element.Text;
            log.Debug("Successfully retrieved text '" + text + "from the element " + element.ToString());
            return text;
        }

        public string GetTextUntilItemDisplayed(By by, int time)
        {
            log.Debug("Trying to retrieve text from the element " + by.ToString());
            WaitForItem(Driver, by, time);
            string text = GetWebElement(Driver, by).Text;
            log.Debug("Successfully retrieved text '" + text + "from the element " + by.ToString());
            return text;
        }

        public string GetTextUntilPresent(IWebElement element, int time = 30)
        {
            log.Debug("Trying to retrieve text from the element " + element.ToString());
            WaitUntilTextIsPresent(Driver, element, time);
            var text = element.Text.Trim();
            log.Debug("Successfully retrieved text '" + text + "from the element " + element.ToString());
            return text;
        }

        public string GetTextUntilPresent(By by, int time = 30)
        {
            log.Debug("Trying to retrieve text from the element " + by.ToString());
            string text = "";
            var element = WaitFor(by);
            WaitUntilTextIsPresent(Driver, element, time);

            try
            {
                text = element.Text.Trim();
                log.Debug("Successfully retrieved text '" + text + "from the element " + element.ToString());
            }
            catch (StaleElementReferenceException ex)
            {
                element = WaitFor(by);
                WaitUntilTextIsPresent(Driver, element, time);
                text = element.Text.Trim();
                log.Debug("Successfully retrieved text with stale element exception '" + text + "from the element " + element.ToString());
                Console.WriteLine(ex);
                log.Error(ex);
            }            
            return text;
        }

        public void SelectByVisibleText(By by, String text)
        {
            log.Debug("Successfully to select dropdown text '" + text + "from the element " + by.ToString());
            WaitUntilElementVisible(by);
            SelectElement select = new SelectElement(GetWebElement(Driver, by));
            select.SelectByText(text);
            log.Debug("Successfully Selected dropdown text '" + text + "from the element " + by.ToString());
        }

        public void SelectByVisibleText(IWebElement element, String text)
        {
            log.Debug("Successfully to select dropdown text '" + text + "from the element " + element.ToString());
            WaitForItem(Driver, element);
            SelectElement select = new SelectElement(element);
            select.SelectByText(text);
            log.Debug("Successfully selected dropdown text '" + text + "from the element " + element.ToString());
        }

        public IList<IWebElement> SelectAllOptions(IWebElement element)
        {
            log.Debug("Trying to retrieve all dropdown options ");
            WaitForItem(Driver, element);
            SelectElement select = new SelectElement(element);
            log.Debug("Successfully retrieved all dropdown options ");
            return select.Options;
        }

        public IWebElement SelectAlreadySelectedOption(By by)
        {
            log.Debug("Trying to retrieved already selected dropdown options ");
            WaitUntilClickable(Driver, by);
            var element = Driver.FindElement(by);
            SelectElement select = new SelectElement(element);
            log.Debug("Successfully retrieved already selected dropdown options ");
            return select.SelectedOption;
        }

        public IList<string> GetAllSelectOptions(By by)
        {
            log.Debug("Trying to retrieve all dropdown options for element " + by.ToString());
            IList<string> actualDropDownOptions = new List<string>();
            WaitForItem(Driver, by);
            var element = Driver.FindElement(by);
            IList<IWebElement> retrievedDropDownOptions = SelectAllOptions(element);
            int ItemSize = retrievedDropDownOptions.Count;

            for (int i = 0; i < ItemSize; i++)
            {
                string ItemValue = retrievedDropDownOptions.ElementAt(i).Text.ToString();
                actualDropDownOptions.Add(ItemValue.Trim());
            }
            log.Debug("Successfully retrieved all dropdown options for element " + by.ToString());
            return actualDropDownOptions;
        }

        public void SelectByList(List<IWebElement> dropDownList, String dropDownVal)
        {
            log.Debug("Trying to select all dropdown options ");
            foreach (IWebElement el in dropDownList)
            {
                SelectByVisibleText(el, dropDownVal);
            }

            log.Debug("Successfully select all dropdown options ");
        }

        public IList<string> GetAllDropdownOptions(By by)
        {
            log.Debug("Trying to retrieve all dropdown options for element " + by.ToString());
            IList<IWebElement> itemList;
            IList<string> actualDropdownList = new List<string>();
            itemList = WaitForElements(by);            
            log.Info("List size: " + itemList.Count);
            int itemSize = itemList.Count;

            for (int i = 0; i < itemSize; i++)
            {
                String ItemValue = itemList.ElementAt(i).Text.ToString();
                actualDropdownList.Add(ItemValue.Trim());
            }
            log.Debug("Successfully retrieved all dropdown options for element " + by.ToString());
            return actualDropdownList;
        }

        public string GetSelectedDropdownOption(By by)
        {
            log.Debug("Trying to get the selected dropdown option from the dropdown " + by.ToString());
            IWebElement element = WaitFor(by);
            string text = GetTextUntilPresent(element);
            log.Debug("Successfully retrieved dropdown option '"+text+"' from the dropdown " + by.ToString());
            return text;
        }

        public void SetTextboxByList(List<IWebElement> textBoxList, String textBoxVal)
        {
            log.Debug("Trying to set values for multiple text boxes ");
            foreach (IWebElement el in textBoxList)
            {
                ClearText(el);
                EnterText(el, textBoxVal);
            }

            log.Debug("Successfully set values for multiple text boxes ");
        }

        public void Sleep(int seconds)
        {
            try
            {
                Thread.Sleep(seconds * 1000);
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e.StackTrace);
                log.Error(e.StackTrace);
            }
        }

        public void ScrollToTheBottom()
        {
            log.Debug("Trying to scroll to the bottom of the page ");
            ((IJavaScriptExecutor)Driver)
                .ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            log.Debug("Successfully scroll to the bottom of the page ");
        }

        public void ScrollElementIntoViewWebElm(IWebElement element, int adjustmentValue,
            string adjustment = "plus")
        {
            log.Debug("Trying to scroll to the page element " + element.ToString());
            int yCoOrdinate;
            if (adjustment.ToLower() == "minus")
            {
                yCoOrdinate = element.Location.Y - adjustmentValue; // coordinate adjustment to deal with the top nav bar
            }
            else
            {
                yCoOrdinate = element.Location.Y + adjustmentValue; // coordinate adjustment to deal with the top nav bar
            }

            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0," + yCoOrdinate + ")");
            log.Debug("Successfully scroll to the page element " + element.ToString());
        }

        public void ScrollToWebElement(IWebElement element)
        {
            log.Debug("Trying to scroll to the page element " + element.ToString());
            ((IJavaScriptExecutor)Driver).ExecuteScript(
                    "arguments[0].scrollIntoView();", element);
            log.Debug("Successfully scroll to the page element " + element.ToString());
        }

        public void ScrollToWebElement(By by)
        {
            log.Debug("Trying to scroll to the page element " + by.ToString());
            var element = Driver.FindElement(by);
            ((IJavaScriptExecutor)Driver).ExecuteScript(
                    "arguments[0].scrollIntoView();", element);
            log.Debug("Successfully scroll to the page element " + by.ToString());
        }

        public void ScrollElementIntoViewWebElm(By by, int adjustmentValue,
            string adjustment = "plus")
        {
            log.Debug("Trying to scroll to the page element " + by.ToString());
            var element = Driver.FindElement(by);
            int yCoOrdinate;
            if (adjustment.ToLower() == "minus")
            {
                yCoOrdinate = element.Location.Y - adjustmentValue; // coordinate adjustment to deal with the top nav bar
            }
            else
            {
                yCoOrdinate = element.Location.Y + adjustmentValue; // coordinate adjustment to deal with the top nav bar
            }

            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0," + yCoOrdinate + ")");
            log.Debug("Successfully scroll to the page element " + by.ToString());
        }

        public void ScrollToTheTop()
        {
            log.Debug("Trying to scroll to the top of the page ");
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, 0);");
            log.Debug("Successfully scroll to the top of the page ");
        }

        public bool WaitForPageToLoad(int timeout = 30)
        {
            IWait<IWebDriver> jsWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            IWait<IWebDriver> jqWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            IWait<IWebDriver> angularWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            bool flag = false;

            try
            {
                log.Debug("Trying to wait for the page to load ");
                jsWait.Until(
                    d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

                jqWait.Until(
                    d => ((IJavaScriptExecutor)d).ExecuteScript("return jQuery.active == 0"));

                angularWait.Until(
                    d => ((IJavaScriptExecutor)d).ExecuteScript("return (window.angular !== undefined) && (angular.element(document.body).injector() !== undefined) && (angular.element(document.body).injector().get('$http').pendingRequests.length === 0)"));
            }
            catch (Exception e)
            {
                flag = true;
                Console.WriteLine(e);
                log.Error(e);
                //throw;
            }
            log.Debug("Successfully wait for the page to load ");
            return flag;
        }

       

    public bool ItemIsFound(By by, int time = 30)
        {
            log.Debug("Trying to retrieve bool for the item found " + by.ToString());
            try
            {
                WaitForItem(Driver, by, time);
                IWebElement element = Driver.FindElement(by);
            }
            catch (Exception)
            {
                return false;
            }
            log.Debug("Retrived bool for the item found " + by.ToString());
            return true;
        }
        public static bool WaitForItem(IWebDriver driver, By by, int time = 30)
        {
            
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, time));
            try
            {
                log.Debug("Waiting for the element " + by.ToString());
                wait.Until(d => d.FindElement(by).Displayed);
                log.Debug("Element found successfully " + by.ToString());
            }
            catch (Exception)
            {
                log.Debug("Unable to locate element" + by.ToString());
                return false;
            }
            
            return true;
        }

        public static bool WaitForItem(IWebDriver driver, IWebElement element, int time = 30)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, time));
            try
            {
                log.Debug("Waiting for the element " + element.ToString());
                wait.Until(d => element.Displayed && element.Enabled);
                log.Debug("Element found successfully  " + element.ToString());
            }
            catch (Exception)
            {
                log.Error("Unable to locate element " + element.ToString());
                return false;
            }                     
            return true;
        }

        public bool WaitForItemToBeDisplayed(IWebDriver driver, IWebElement element, int time = 15)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, time));
            try
            {
                log.Debug("Waiting for the element to be displayed" + element.ToString());
                wait.Until(d => element.Displayed);
                log.Debug("Element displayed successfully  " + element.ToString());
            }
            catch (Exception)
            {
                log.Error("Unable to locate element " + element.ToString());
                return false;
            }
            
            return true;
        }

        public bool WaitUntilTextIsPresent(IWebDriver driver, IWebElement element, int time = 5,
            int minExpectLgth = 0)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, time));
            WaitForItem(driver, element);
            try
            {
                log.Debug("Waiting for the text to be present " + element.ToString());
                wait.Until(d => element.Text.Length > minExpectLgth);
                log.Debug("Element found with text " + element.ToString());
            }
            catch (Exception)
            {
                log.Error("Unable to locate element " + element.ToString());
                return false;
            }
            
            return true;
        }

        public bool WaitUntilItemEnabledAndDisplayed(IWebDriver driver, By by, int time = 30)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, time));
            try
            {
                log.Debug("Waiting for the element to be enabled and displayed " + by.ToString());
                wait.Until(d => d.FindElement(by).Displayed & d.FindElement(by).Enabled);
                log.Debug("Element found enabled and displayed " + by.ToString());
            }
            catch (Exception)
            {
                log.Error("Unable to locate element " + by.ToString());
                return false;
            }
            
            return true;
        }

        public bool WaitUntilClickable(IWebDriver driver, By by, int time = 30)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, time));
            try
            {
                log.Debug("Waiting for the element to be clickable " + by.ToString());
                wait.Until(d => d.FindElement(by).Enabled && d.FindElement(by).Displayed);
                log.Debug("Element found to be clickable " + by.ToString());
            }
            catch (Exception)
            {
                log.Error("Unable to locate element " + by.ToString());
                return false;
            }
            
            return true;
        }

        public bool WaitUntilClickable(IWebDriver driver, IWebElement element)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            try
            {
                log.Debug("Waiting for the element to be clickable " + element.ToString());
                wait.Until(d => element.Enabled && element.Displayed);
                log.Debug("Element found to be clickable " + element.ToString());
            }
            catch (Exception)
            {
                log.Error("Unable to locate element " + element.ToString());
                return false;
            }
            
            return true;
        }
        
        public string GetDecodedTextValueWithoutSpaces(By by)
        {
            log.Debug("Tryind to decode the string for element " + by.ToString());
            WaitUntilClickable(Driver, by);
            var element = Driver.FindElement(by);
            String encodedString = GetTextUntilItemDisplayed(element);
            Encoding encodingASCII = Encoding.ASCII;
            Byte[] encodedBytes = encodingASCII.GetBytes(encodedString);
            char[] asciiChars = encodingASCII.GetChars(encodedBytes);
            String decodedString = new String(asciiChars);
            decodedString = Regex.Replace(decodedString, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
            log.Debug("Decoded the string successfully for element " + by.ToString());
            return decodedString;
        }

        public bool IsWebElementDisplayed(IWebDriver driver, By by)
        {
            log.Debug("Waiting for element to be displayed " + by.ToString());
            WaitUntilClickable(driver, by);
            var element = driver.FindElement(by);
            bool val = element.Displayed;
            log.Debug("Element displayed successfully " + by.ToString());
            return val;
        }

        public bool IsWebElementDisplayed(IWebDriver driver, IWebElement element)
        {
            log.Debug("Waiting for element to be displayed " + element.ToString());
            WaitForItem(driver, element);
            bool val = element.Displayed;
            log.Debug("Element displayed successfully " + element.ToString());
            return val;
        }

        public bool IsWebElementEnabled(IWebDriver driver, By by)
        {
            log.Debug("Waiting for the element to be enabled " + by.ToString());
            WaitUntilClickable(driver, by);
            var element = driver.FindElement(by);
            bool val = element.Enabled;
            log.Debug("Element enabled successfully " + by.ToString());
            return val;
        }

        public bool IsWebElementEnabled(IWebDriver driver, IWebElement element)
        {
            log.Debug("Waiting for the element to be enabled " + element.ToString());
            WaitForItem(driver, element);
            bool val = element.Enabled;
            log.Debug("Element enabled successfully " + element.ToString());
            return val;
        }

        public string GetElementAttribute(By by, String attribute)
        {
            log.Debug("Trying to retrieve element attribute " + by.ToString());
            WaitFor(by);
            string attVal = Driver.FindElement(by).GetAttribute(attribute);
            log.Debug("Element attribute value returned successfully " + by.ToString());
            return attVal;
        }

        public string GetElementAttribute(IWebElement element, String attribute)
        {
            log.Debug("Trying to get the element attribute value " + element.ToString());
            string attVal = element.GetAttribute(attribute);
            log.Debug("Element attribute value returned successfully " + element.ToString());
            return attVal;
        }

        public string GetElementCss(By by, String cssAttribute)
        {
            log.Debug("Trying to get elements css attribute value " + by.ToString());
            string attVal = Driver.FindElement(by).GetAttribute(cssAttribute);
            log.Debug("Element css attribute value returned successfully " + by.ToString());
            return attVal;
        }

        public void ClickSubmitButton(IWebDriver driver, By by)
        {
            log.Debug("Trying to performed click operation on element " + by.ToString());
            WaitUntilClickable(driver, by);
            driver.FindElement(by).Submit();
            log.Debug("Successfully performed click operation on element " + by.ToString());
        }

        public void ClickSubmitButton(IWebDriver driver, IWebElement element)
        {
            log.Debug("Trying to performed click operation on element " + element.ToString());
            WaitUntilClickable(driver, element);
            element.Submit();
            log.Debug("Successfully performed click operation on element " + element.ToString());
        }

        public void BrowserRefresh()
        {
            log.Debug("Trying to refresh browser ");
            Driver.Navigate().Refresh();
            log.Debug("Browser refresh happens ");
        }

        public IWebElement GetElement(IWebDriver driver, By by, int time = 30)
        {
            log.Debug("Trying to get the element " + by.ToString());
            WaitForItem(driver, by, time);
            var element = driver.FindElement(by);
            log.Debug("Element retrieved successfully " + by.ToString());
            return element;
        }

        public void PressTab()
        {
            log.Debug("Trying to press the keyboard Tab ");
            OpenQA.Selenium.Interactions.Actions actionObject = new OpenQA.Selenium.Interactions.Actions(Driver);
            actionObject.SendKeys(Keys.Tab).Perform();
            log.Debug("Tab keyboard button pressed ");
        }

        public void PressBackspace()
        {
            log.Debug("Trying to press the keyboard Backspace ");
            OpenQA.Selenium.Interactions.Actions actionObject = new OpenQA.Selenium.Interactions.Actions(Driver);
            actionObject.SendKeys(Keys.Backspace).Perform();
            log.Debug("Backspace keyboard button pressed ");
        }
    }
}
