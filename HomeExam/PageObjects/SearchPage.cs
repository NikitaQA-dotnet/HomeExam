using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeExam.PageObjects
{
    class SearchPage : BasePage
    {
        private IWebDriver _driver;
        public SearchPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        #region locators
        private string _departments = "//div[@id='departments']/ul/li[@id]";
        private string _customerReviewsSay = "//ul[contains(@aria-labelledby,'feature')]/li[@id]//span[text()='{0}']";
        private string _productName = "//div[contains(@class,'s-search-results')]//span[contains(@class,'a-text-normal') and contains(text(),'{0}')]";
        #endregion

        public SearchPage SelectDepartment(string department)
        {
            WaitAllElementsIsVisible(_departments).First(x => x.Text == department).Click();
            return this;
        }

        public ProductPage SelectProduct(string text)
        {
            WaitElementIsPresent(String.Format(_productName,text), 10).Click();
            return new ProductPage(_driver);
        }

        public SearchPage SelectCustomerReviewsSay(string text)
        {
            WaitElementIsPresent(String.Format(_customerReviewsSay,text), 10).Click();
            return this;
        }
    }
}
