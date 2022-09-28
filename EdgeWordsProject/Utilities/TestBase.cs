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
        }

        [AfterScenario]
        public static void TearDown()
        {

            // close windows
            driver.Quit();
        }
    }
}
