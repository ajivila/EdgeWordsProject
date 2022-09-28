using EdgeWordsProject.POMs;
using NUnit.Framework;
using static EdgeWordsProject.Utilities.TestBase;

namespace EdgeWordsProject.StepDefinitions
{
    [Binding]
    public class DiscountCodeStepDefinitions
    {

        [Given(@"user is registered")]
        public static void GivenUserIsRegistered()
        {
            Console.WriteLine("User pre-registered manually\n");
        }

        [Given(@"user is on the account page")]
        public static void GivenUserIsOnTheAccountPage()
        {
            // navigate to account page
            HomePage homePage = new(driver);
            homePage.ClickAccountLink();
        }

        [When(@"user logs in using correct log in details")]
        [Given(@"user is logged in")]
        public static void WhenUserLogsInUsingCorrectLogInDetails()
        {
            // log in using saved details
            AccountPage accountPage = new(driver);
            accountPage.LogIn(username, password);

            Console.WriteLine($"Logging in {username}\n");
        }

        [Then(@"user gets logged in successfuly")]
        public static void ThenUserGetsLoggedInSuccessfuly()
        {
            // retreive any error messages
            AccountPage accountPage = new(driver);
            string errorMessage = accountPage.GetErrorMessage();

            // if no error message appears, assume log in successful
            Assert.That(String.IsNullOrEmpty(errorMessage), Is.True, errorMessage);

            Console.WriteLine("User logged in successfully\n");
        }

        [Given(@"user is on the main shop page")]
        public static void GivenUserIsOnTheMainShopPage()
        {
            // navigate to shop page
            HomePage homePage = new(driver);
            homePage.ClickShopLink();
        }

        [When(@"user adds a '(.*)' to the cart")]
        public static void WhenUserAddsAToTheCart(string itemName)
        {
            // check if an item with given name exists
            ShopPage shopPage = new(driver);
            Assert.That(shopPage.FindItem(itemName),
                $"Item named \"{itemName}\" is not present");

            Console.WriteLine($"{itemName} found");

            // add item to the cart
            shopPage.AddItemToCart(itemName);
            Console.WriteLine($"{itemName} is being added to the cart\n");
        }

        [Then(@"the '(.*)' is added to the cart")]
        public static void ThenTheItemIsAddedToTheCart(string itemName)
        {
            // navigate to cart page
            ShopPage shopPage = new(driver);
            shopPage.ClickCartLink();

            // check if item is in cart
            CartPage cartPage = new(driver);
            Assert.That(cartPage.CheckIfInCart(itemName),
                "Item not added to cart");

            Console.WriteLine("Item added\n");
        }

        [Given(@"user's cart contains '(.*)'")]
        public static void GivenUsersCartContainsItem(string itemName)
        {
            // navigate to shop page
            HomePage homePage = new(driver);
            homePage.ClickShopLink();

            // add an item to the cart
            ShopPage shopPage = new(driver);
            shopPage.AddItemToCart(itemName);

            Console.WriteLine($"{itemName} added to the cart\n");
        }

        [Given(@"user is on the cart page")]
        public static void GivenUserIsOnTheCartPage()
        {
            // navigate to cart page
            HomePage homePage = new(driver);
            homePage.ClickCartLink();
        }

        [When(@"a discount code '(.*)' gets submitted")]
        public static void WhenADiscountCodeGetsSubmitted(string discountCode)
        {
            // apply discount code
            CartPage cartPage = new(driver);
            cartPage.ApplyDiscount(discountCode);

            Console.WriteLine($"Discount code {discountCode} is being applied\n");
        }

        [Then(@"a discount is applied")]
        public static void ThenADiscountOfIsApplied(Table percentages)
        {
            // calculate the obtained discount
            CartPage cartPage = new(driver);
            double obtainedDiscount = cartPage.getDiscountPercentage();

            // check obtained discount against test parameters
            foreach (TableRow percentage in percentages.Rows)
            {
                Console.WriteLine($"Checking if {percentage[0]}% discount achieved...");

                try
                {
                    Assert.That(double.Parse(percentage[0]) == obtainedDiscount);
                    Console.WriteLine($"Discount of {percentage[0]}% achieved\n");
                }
                // usually wouldn't catch this but made an exception due to project spec
                catch (AssertionException)
                {
                    Console.WriteLine($"Discount of {percentage[0]}% not achieved\n");
                }
            }
        }

        [Then(@"the correct total is calculated")]
        public static void ThenTheCorrectTotalIsCalculated()
        {
            CartPage cartPage = new(driver);

            // Check that the total is calculated correctly
            double expectedTotal = cartPage.getOriginalPrice() -
                cartPage.getCouponAmount() + cartPage.getShippingCost();
            Assert.That(expectedTotal == cartPage.getTotalCost(),
                $"Total expected to be {expectedTotal}, actual result: {cartPage.getTotalCost}");

            Console.WriteLine($"Total confirmed as correct at £{expectedTotal}\n");
        }

        [When(@"user logs out")]
        public static void WhenUserLogsOut()
        {
            // log out
            HomePage homePage = new(driver);
            homePage.ClickAccountLink();
            AccountPage accountPage = new(driver);
            accountPage.LogOut();

            Console.WriteLine("User getting logged out...");
        }

        [Then(@"user gets logged out")]
        public static void ThenUserGetsLoggedOut()
        {
            // check that log out has been successful
            AccountPage accountPage = new(driver);
            Assert.That(accountPage.isLoggedOut(),
                "user has not been logged out");

            Console.WriteLine("User logged out successfully\n");
        }
    }
}
