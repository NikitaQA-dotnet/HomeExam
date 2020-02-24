using HomeExam.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace HomeExam
{
    public class PagesFactory
    {
        private IWebDriver _driver;
        private IDictionary<Object, Object> _pages;

        public PagesFactory()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Window.Maximize();

            _pages = new Dictionary<Object, Object>();
            AddPage(() => new BasePage(_driver));
            AddPage(() => new ProductPage(_driver));
            AddPage(() => new SignInPage(_driver));
        }

        public T GetPage<T>()
        {
            try
            {
                var lazy = (Lazy<T>)_pages[typeof(T)];
                return lazy.Value;
            }
            catch (KeyNotFoundException)
            {
                throw new ApplicationException("The requested service is not registered");
            }
        }

        private void AddPage<T>(Func<T> createdFn)
        {
            this._pages.Add(typeof(T), new Lazy<T>(createdFn, true));
        }
    }
}
