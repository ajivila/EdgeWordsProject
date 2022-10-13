using EdgeWordsProject.POMs;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace EdgeWordsProject.Utilities
{
    [Binding]
    internal class TestBase
    {
        public static IWebDriver driver;
        public static readonly string username = Environment.GetEnvironmentVariable("SECRET_USERNAME");
        public static readonly string password = Environment.GetEnvironmentVariable("SECRET_PASSWORD");

        [BeforeScenario]
        public static void SetUp()
        {

            driver = new ChromeDriver();

            // set up maximum wait periods
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(2);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.Url = Environment.GetEnvironmentVariable("BASE_URL");

            LogIn();
            EmptyCart();
        }

        private static void LogIn()
        {
            // navigate to account page
            Navigation navigation = new(driver);
            navigation.ClickAccountLink();

            // log in using saved details
            AccountPage accountPage = new(driver);
            accountPage.LogIn(username, password);

            Console.WriteLine($"Logging in {username}\n");

            // retreive any error messages
            string errorMessage = accountPage.GetErrorMessage();

            // if no error message appears, assume log in successful
            if (String.IsNullOrEmpty(errorMessage))
                Console.WriteLine("User logged in successfully\n");
            else
                Console.WriteLine(errorMessage);
        }

        private static void EmptyCart()
        {
            // navigate to cart page
            Navigation navigation = new(driver);
            navigation.ClickCartLink();

            // remove items
            CartPage cartPage = new(driver);
            try
            {
                cartPage.RemoveItem();
            }
            catch (NoSuchElementException nsee)
            {
                Console.WriteLine("Cart already empty\n");
            }
        }

        [AfterScenario]
        public static void TearDown()
        {
            LogOut();

            // close windows
            driver.Quit();
        }

        private static void LogOut()
        {
            // navigate to account page
            Navigation navigation = new(driver);
            navigation.ClickAccountLink();

            // log out
            AccountPage accountPage = new(driver);
            accountPage.LogOut();

            Console.WriteLine("User getting logged out...");

            // check that log out has been successful
            if(accountPage.isLoggedOut())
                Console.WriteLine("User logged out successfully\n");
            else
                Console.WriteLine("user has not been logged out");            
        }
    }
}
