using OpenQA.Selenium;

namespace EdgeWordsProject.POMs
{
    internal class CartPage
    {
        private IWebDriver _driver;

        // Constructor to set driver object
        public CartPage(IWebDriver driver) => _driver = driver;

        // Set locators
        private IWebElement ItemName => _driver.FindElement(By.CssSelector("td.product-name a"));
        private IWebElement DiscountCodeField => _driver.FindElement(By.Id("coupon_code"));
        private IWebElement DiscountCodeButton => _driver.FindElement(By.CssSelector("div.coupon button"));
        private string CouponAmount => _driver.FindElement(By.CssSelector("tr.cart-discount span")).Text;
        private string OriginalPrice => _driver.FindElement(By.CssSelector("span.amount bdi")).Text;
        private string ShippingCost => _driver.FindElement(By.CssSelector("label[for*=\"shipping\"] bdi")).Text;
        private string TotalCost => _driver.FindElement(By.CssSelector("td[data-title=\"Total\"] bdi")).Text;


        public bool CheckIfInCart(string itemName)
        {
            if (ItemName.Text.Equals(itemName))
                return true;
            return false;
        }

        public void ApplyDiscount(string discountCode)
        {
            DiscountCodeField.Clear();
            DiscountCodeField.SendKeys(discountCode);
            DiscountCodeButton.Click();
        }

        private double toDouble(String num)
        {
            // remove £ sign
            num = num[1..];

            // convert to double
            return double.Parse(num);
        }

        // calculate the obtained discount as a percentage
        public double getDiscountPercentage()
        {
            // convert to numbers
            double originalPrice = toDouble(OriginalPrice);
            double couponAmount = toDouble(CouponAmount);

            return couponAmount / originalPrice * 100;
        }

        public double getOriginalPrice()
        {
            return toDouble(OriginalPrice);
        }

        public double getCouponAmount()
        {
            return toDouble(CouponAmount);
        }

        public double getShippingCost()
        {
            return toDouble(ShippingCost);
        }

        public double getTotalCost()
        {
            return toDouble(TotalCost);
        }
    }
}
