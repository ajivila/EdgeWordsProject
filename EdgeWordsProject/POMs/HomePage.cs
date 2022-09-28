using OpenQA.Selenium;

namespace EdgeWordsProject.POMs
{
    internal class HomePage
    {
        private IWebDriver _driver;

        // Constructor to set driver object
        public HomePage(IWebDriver driver) => _driver = driver;

        // Set locators
        private IWebElement MyAccountLink => _driver.FindElement(By.CssSelector("#menu-item-46 a"));
        private IWebElement ShopLink => _driver.FindElement(By.CssSelector("#menu-item-43 a"));
        private IWebElement CartLink => _driver.FindElement(By.CssSelector("#menu-item-44 a"));

        public void ClickAccountLink()
        {
            MyAccountLink.Click();
        }

        public void ClickShopLink()
        {
            ShopLink.Click();
        }

        public void ClickCartLink()
        {
            CartLink.Click();
        }
    }
}
