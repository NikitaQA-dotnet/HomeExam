using HomeExam.PageObjects;
using NUnit.Framework;


namespace HomeExam
{
    class TestSuite
    {
        PagesFactory pagesFactory = new PagesFactory();
        private BasePage BasePage;
        private ProductPage ProductPage;
        private SignInPage SignInPage;
        private const string EXPECTED_COLOR = "G - Orange";

        [SetUp]
        public void SetUp()
        {
            BasePage = pagesFactory.GetPage<BasePage>();
            ProductPage = pagesFactory.GetPage<ProductPage>();
            SignInPage = pagesFactory.GetPage<SignInPage>();
        }

        [Test]
        public void AmazonTest()
        {
            BasePage.GoToHomePage()
                .FillSearchInput("Bluetooth Portable Speaker")
                .ClickGoButton()
                .SelectDepartment("Portable Bluetooth Speakers")
                .SelectCustomerReviewsSay("Good Portability")
                .SelectProduct("OontZ Angle 3 (3rd Gen)")
                .SelectProductColor("orange");  //sorry, blue speakers were sold out

            string color = ProductPage.GetSelectedColor();
            Assert.AreEqual(EXPECTED_COLOR, color, "Expected color is not ");

            ProductPage.ClickBuyNowButton();
            Assert.IsTrue(SignInPage.IsSignInPageDisplayed());
        }

        [TearDown]
        public void TearDown()
        {
            BasePage.Close();
        }
    }
}
