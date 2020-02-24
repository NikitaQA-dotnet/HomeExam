using OpenQA.Selenium;
using System.Linq;

namespace HomeExam.PageObjects
{
    class ProductPage : BasePage
    {
        private IWebDriver _driver;
        public ProductPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }

        #region locators
        private string _productColorsImages = "//li[contains(@id,'color_name')]//img";
        private string _buyNowButton = "//input[@id='buy-now-button']";
        private string _selectedColorLabel = "//div[@id='variation_color_name']//span[@class='selection']";
        #endregion

        public ProductPage SelectProductColor(string color)
        {
            WaitAllElementsIsVisible(_productColorsImages)
                .First(x => x.GetAttribute("alt").ToLower().Contains(color)).Click();
            return this;
        }

        public void ClickBuyNowButton()
        {
            WaitElementIsPresent(_buyNowButton);
            WaitForElementToBeClickableAndClick(_buyNowButton);
        }

        public string GetSelectedColor()
        {
            return WaitElementIsPresent(_selectedColorLabel).Text;
        }
    }
}
