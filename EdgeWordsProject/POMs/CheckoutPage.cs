using OpenQA.Selenium;


namespace EdgeWordsProject.POMs
{
    internal class CheckoutPage
    {
        private IWebDriver _driver;

        // Constructor to set driver object
        public CheckoutPage(IWebDriver driver) => _driver = driver;

        // set up locators
        private IWebElement FNameField => _driver.FindElement(By.Id("billing_first_name"));
        private IWebElement LNameField => _driver.FindElement(By.Id("billing_last_name"));
        private IWebElement StreetAddressField => _driver.FindElement(By.Id("billing_address_1"));
        private IWebElement CityField => _driver.FindElement(By.Id("billing_city"));
        private IWebElement PostcodeField => _driver.FindElement(By.Id("billing_postcode"));
        private IWebElement PhoneField => _driver.FindElement(By.Id("billing_phone"));
        private IWebElement PlaceOrderButton => _driver.FindElement(By.Id("place_order"));
        private string OrderNumber => _driver.FindElement(By.CssSelector("li.order strong")).Text;



        public void FillName(string fName, string lName)
        {
            FNameField.Clear();
            FNameField.SendKeys(fName);

            LNameField.Clear();
            LNameField.SendKeys(lName);
        }

        public void FillAddress(string street, string city, string postcode)
        {
            StreetAddressField.Clear();
            StreetAddressField.SendKeys(street);

            CityField.Clear();
            CityField.SendKeys(city);

            PostcodeField.Clear();
            PostcodeField.SendKeys(postcode);
        }

        public void FillContactDetails(string phone)
        {
            PhoneField.Clear();
            PhoneField.SendKeys(phone);
        }

        public void ClickPlaceOrderButton()
        {
            Thread.Sleep(1000);
            PlaceOrderButton.Click();
        }

        public string GetOrderNumber()
        {;
            return OrderNumber;
        }
    }
}
