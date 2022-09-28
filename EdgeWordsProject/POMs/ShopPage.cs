using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EdgeWordsProject.POMs
{
    internal class ShopPage
    {
        private IWebDriver _driver;

        // Constructor to set driver object
        public ShopPage(IWebDriver driver) => _driver = driver;

        // Set locators
        private IReadOnlyCollection<IWebElement> Items => _driver.FindElements(By.CssSelector("li h2"));
        private IWebElement CartLink => _driver.FindElement(By.CssSelector("#menu-item-44 a"));
        private IWebElement CheckoutLink => _driver.FindElement(By.CssSelector("#menu-item-45 a"));


        // check if item with the given name exists
        public bool FindItem(string name)
        {
            foreach (IWebElement item in Items)
            {
                if (item.Text.Equals(name))
                    return true;
            }
            return false;
        }

        public void AddItemToCart(string name)
        {
            _driver.FindElement(By.CssSelector($"a[aria-label*=\"{name}\"")).Click();

            // wait until action completes
            WebDriverWait wait = new(_driver, TimeSpan.FromSeconds(3));
            wait.Until(drv => drv.FindElement(By.CssSelector("a.added_to_cart")));
        }

        public void ClickCartLink()
        {
            CartLink.Click();
        }

        public void ClickCheckoutLink()
        {
            CheckoutLink.Click();
        }
    }
}
