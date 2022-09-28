using EdgeWordsProject.POMs;
using NUnit.Framework;
using static EdgeWordsProject.Utilities.TestBase;

namespace EdgeWordsProject.StepDefinitions
{
    [Binding]
    public class CheckoutStepDefinitions
    {

        [Given(@"User is logged in")]
        public static void GivenUserIsLoggedIn()
        {
            // navigate to account page
            HomePage homePage = new(driver);
            homePage.ClickAccountLink();

            // log in using saved details
            AccountPage accountPage = new(driver);
            accountPage.LogIn(username, password);

            Console.WriteLine($"Logging in user {username}\n");
        }

        [Given(@"user adds '(.*)' to their cart")]
        public static void GivenUserAddsAnItemToTheirCart(string itemName)
        {
            // navigate to shop page
            HomePage homePage = new(driver);
            homePage.ClickShopLink();

            // check if an item with given name exists
            ShopPage shopPage = new(driver);
            Assert.That(shopPage.FindItem(itemName),
                $"Item named \"{itemName}\" is not present");

            // add item to the cart
            shopPage.AddItemToCart(itemName);

            Console.WriteLine($"{itemName} has been added to the cart\n");
        }

        [Given(@"fills in valid billing details")]
        public static void GivenFillsInValidBillingDetails()
        {
            // navigate to checkout
            ShopPage shopPage = new(driver);
            shopPage.ClickCheckoutLink();

            // fill in billing details
            CheckoutPage checkoutPage = new(driver);
            checkoutPage.FillName("Livija", "Rukmane");
            checkoutPage.FillAddress("0 Nowhere Street", "Neverland", "N0 0NN");
            checkoutPage.FillContactDetails("01234567890");

            Console.WriteLine("Billing details filled in\n");
        }

        [When(@"order is placed")]
        public static void WhenOrderIsPlaced()
        {
            // place order
            CheckoutPage checkoutPage = new(driver);
            checkoutPage.ClickPlaceOrderButton();

            Console.WriteLine("Placing order...");
        }

        [Then(@"correct order appears in user's account")]
        public static void ThenCorrectOrderAppearsInUsersAccount()
        {
            // note order number
            CheckoutPage shopPage = new(driver);
            string orderNumber = shopPage.GetOrderNumber();

            Console.WriteLine($"Order number recorded as: {orderNumber}\n"); 

            // navigate to user's orders page
            HomePage homePage = new(driver);
            homePage.ClickAccountLink();
            AccountPage accountPage = new(driver);
            accountPage.ClickOrdersButton();

            Console.WriteLine("Navigating to my orders page...");

            // check order number
            OrdersPage ordersPage = new(driver);
            Assert.That(ordersPage.getOrderNumber().Equals(orderNumber),
                $"Expected order number {orderNumber}, received {ordersPage.getOrderNumber()}");

            Console.WriteLine("Order number confirmed\n");
        }

    }
}
