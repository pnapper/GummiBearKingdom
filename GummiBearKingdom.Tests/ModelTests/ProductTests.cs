using Microsoft.VisualStudio.TestTools.UnitTesting;
using GummiBearKingdom.Models;

namespace GummiBearKingdom.Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void GetName_ReturnsProductName_String()
        {
            //Arrange
            var product = new Product();
            product.Name = "Blue Raspberry 1 lb Bulk Package";

            //Act
            var result = product.Name;

            //Assert
            Assert.AreEqual("Blue Raspberry 1 lb Bulk Package", result);
        }

        [TestMethod]
        public void GetPrice_ReturnsProductPrice_Decimal()
        {
            //Arrange
            var product = new Product();
            product.Price = 4.99m;

            //Act
            var result = product.Price;

            //Assert
            Assert.AreEqual(4.99m, result);
        }

        [TestMethod]
        public void GetDescription_ReturnsProductDescription_String()
        {
            //Arrange
            var product = new Product();
            product.Description = "All your favorite flavors in one place! Enjoy the World's Best Gummi Bears in 12 fresh fruity flavors. Flavors include: Cherry, Pink Grapefruit, Watermelon, Strawberry, Orange, Blue Raspberry, Lime, Grape, Green Apple, Mango, Pineapple & Lemon.";

            //Act
            var result = product.Description;

            //Assert
            Assert.AreEqual("All your favorite flavors in one place! Enjoy the World's Best Gummi Bears in 12 fresh fruity flavors. Flavors include: Cherry, Pink Grapefruit, Watermelon, Strawberry, Orange, Blue Raspberry, Lime, Grape, Green Apple, Mango, Pineapple & Lemon.", result);
        }
    }
}
