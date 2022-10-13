using EdgeWordsProject.POMs;
using NUnit.Framework;
using static EdgeWordsProject.Utilities.TestBase;

namespace EdgeWordsProject.StepDefinitions
{
    [Binding]
    public class DiscountCodeStepDefinitions
    {

        [Given(@"user is on the main shop page")]
        public static void GivenUserIsOnTheMainShopPage()
        {
            // navigate to shop page
            Navigation navigation = new(driver);
            navigation.ClickShopLink();
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
            Navigation navigation = new(driver);
            navigation.ClickShopLink();

            // add an item to the cart
            ShopPage shopPage = new(driver);
            shopPage.AddItemToCart(itemName);

            Console.WriteLine($"{itemName} added to the cart\n");
        }

        [Given(@"user is on the cart page")]
        public static void GivenUserIsOnTheCartPage()
        {
            // navigate to cart page
            Navigation navigation = new(driver);
            navigation.ClickCartLink();
        }

        [When(@"a discount code '(.*)' gets submitted")]
        public static void WhenADiscountCodeGetsSubmitted(string discountCode)
        {
            // apply discount code
            CartPage cartPage = new(driver);
            cartPage.ApplyDiscount(discountCode);

            Console.WriteLine($"Discount code {discountCode} is being applied\n");
        }

        [Then(@"a discount of '(\d*)'% is applied")]
        public static void ThenADiscountOfIsApplied(decimal percentage)
        {
            // calculate the obtained discount
            CartPage cartPage = new(driver);
            decimal obtainedDiscount = cartPage.getDiscountPercentage();

            // check obtained discount against test parameter

            Console.WriteLine($"Checking if {percentage}% discount achieved...");

            Assert.That(percentage == obtainedDiscount, $"Discount incorrect at {obtainedDiscount}%\n");
            Console.WriteLine($"Discount of {percentage}% achieved\n");
        }

        [Then(@"the correct total is calculated")]
        public static void ThenTheCorrectTotalIsCalculated()
        {
            CartPage cartPage = new(driver);

            // Check that the total is calculated correctly
            decimal expectedTotal = cartPage.getOriginalPrice() -
                cartPage.getCouponAmount() + cartPage.getShippingCost();
            Assert.That(expectedTotal == cartPage.getTotalCost(),
                $"Total expected to be {expectedTotal}, actual result: {cartPage.getTotalCost}");

            Console.WriteLine($"Total confirmed as correct at £{expectedTotal}\n");
        }
    }
}
