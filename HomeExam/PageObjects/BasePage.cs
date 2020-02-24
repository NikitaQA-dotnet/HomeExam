using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeExam.PageObjects
{
    class BasePage
    {
        private IWebDriver _driver;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        #region locators
        private string _searchInput = "//input[@id='twotabsearchtextbox']";
        private string _goButton = "//input[@value='Go']";
        #endregion

        #region common page methods
        public BasePage FillSearchInput(string searchValue)
        {
            WaitElementIsPresent(_searchInput).SendKeys(searchValue);
            return this;
        }

        public SearchPage ClickGoButton()
        {
            WaitElementIsPresent(_goButton).Click();
            return new SearchPage(_driver);
        }

        public BasePage GoToHomePage()
        {
            _driver.Navigate().GoToUrl("https://www.amazon.com/");
            return this;
        }
        #endregion

        #region service methods
        public void Close()
        {
            _driver.Close();
        }

        public IWebElement WaitElementIsPresent(String xPath, Int32 seconds = 5)
        {
            try
            {
                return new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(xPath)));
            }
            catch (NoSuchElementException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<IWebElement> WaitAllElementsIsVisible(String xPath, Int32 seconds = 5)
        {
            try
            {
                return new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(xPath))).ToList();
            }
            catch (Exception ex)
            {
                if(ex is ElementClickInterceptedException)
                {
                    return new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(xPath))).ToList();
                }

                throw ex;
            }
        }

        public IWebElement WaitForElementToBeClickable(string xpath, Int32 seconds = 5)
        {
            IWebElement element = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_driver.FindElement(By.XPath(xpath))));
            return element;
        }

        public void WaitForElementToBeClickableAndClick(string xpath, Int32 seconds = 5)
        {
            try
            {
                WaitForElementToBeClickable(xpath,seconds).Click();
            }
            catch(Exception ex)
            {
                if (ex is ElementClickInterceptedException || ex is StaleElementReferenceException)
                {
                    Thread.Sleep(5000);
                    WaitForElementToBeClickable(xpath, seconds).Click();
                }
            }
        }
        #endregion
    }
}
