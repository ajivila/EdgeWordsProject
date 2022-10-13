using EdgeWordsProject.POMs;
using NUnit.Framework;
using static EdgeWordsProject.Utilities.TestBase;

namespace EdgeWordsProject.StepDefinitions
{
    [Binding]
    public class CheckoutStepDefinitions
    {
        [Given(@"user adds '(.*)' to their cart")]
        public static void GivenUserAddsAnItemToTheirCart(string itemName)
        {
            // navigate to shop page
            Navigation navigation = new(driver);
            navigation.ClickShopLink();

            // check if an item with given name exists
            ShopPage shopPage = new(driver);
            Assert.That(shopPage.FindItem(itemName),
                $"Item named \"{itemName}\" is not present");

            // add item to the cart
            shopPage.AddItemToCart(itemName);

            Console.WriteLine($"{itemName} has been added to the cart\n");
        }

        [Given(@"fills in valid billing details:")]
        public static void GivenFillsInValidBillingDetails(Table details)
        {
            // navigate to checkout
            ShopPage shopPage = new(driver);
            shopPage.ClickCheckoutLink();

            // fill in billing details
            CheckoutPage checkoutPage = new(driver);
            //foreach TableRow row in 
            checkoutPage.FillName(details.Rows[0][0], details.Rows[0][1]);
            checkoutPage.FillAddress(details.Rows[0][2], details.Rows[0][3], details.Rows[0][4]);
            checkoutPage.FillContactDetails(details.Rows[0][5]);

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
            Navigation navigation = new(driver);
            navigation.ClickAccountLink();
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
