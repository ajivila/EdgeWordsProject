using OpenQA.Selenium;

namespace EdgeWordsProject.POMs
{
    internal class OrdersPage
    {
        private IWebDriver _driver;

        // Constructor to set driver object
        public OrdersPage(IWebDriver driver) => _driver = driver;

        // Set locators
        private IWebElement OrderNumber => _driver.FindElement(By.CssSelector("td[data-title=\"Order\"] a"));

        public string getOrderNumber()
        {
            // return order number with # symbol removed
            return OrderNumber.Text[1..];
        }
    }
}
