using OpenQA.Selenium;

namespace HomeExam.PageObjects
{
    class SignInPage : BasePage
    {
        private IWebDriver _driver;
        public SignInPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        #region locators
        private string _signInText = "//h1[contains(text(),'Sign-In')]";
        #endregion

        public bool IsSignInPageDisplayed()
        {
            try
            {
                WaitElementIsPresent(_signInText);
                return true;
            }
            catch(NoSuchElementException)
            {
                return false;
            }
        }
    }
}
