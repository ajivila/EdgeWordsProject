using OpenQA.Selenium;

namespace EdgeWordsProject.POMs
{
    internal class AccountPage
    {
        private IWebDriver _driver;

        // Constructor to set driver object
        public AccountPage(IWebDriver driver) => _driver = driver;

        // Set locators
        private IWebElement UsernameField => _driver.FindElement(By.Id("username"));
        private IWebElement PasswordField => _driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => _driver.FindElement(By.CssSelector("form.login button"));
        private IWebElement LoginForm => _driver.FindElement(By.CssSelector("form.login"));
        private IWebElement LogOutButton => _driver.FindElement(By.CssSelector("li[class*=\"logout\"] a"));

        private IWebElement OrdersButton => _driver.FindElement(By.CssSelector("li[class*=\"orders\"] a"));
        private IReadOnlyCollection<IWebElement> _errorMessage => _driver.FindElements(By.CssSelector(".woocommerce-error li"));

        public void LogIn(string username, string password)
        {
            UsernameField.Clear();
            UsernameField.SendKeys(username);

            PasswordField.Clear();
            PasswordField.SendKeys(password);

            LoginButton.Click();
        }
        public void LogOut()
        {
            LogOutButton.Click();
        }

        public void ClickOrdersButton()
        {
            OrdersButton.Click();
        }

        public bool isLoggedOut()
        {
            if (LoginForm.Displayed)
                return true;
            return false;
        }

        public string GetErrorMessage()
        {
            // return error message if it exists
            if (_errorMessage.Count > 0)
            {
                return _errorMessage.ElementAt(0).Text;
            }
            else
                return string.Empty;
        }
    }
}
