using Microsoft.VisualStudio.TestTools.UnitTesting;
using GummiBearKingdom.Models;

namespace GummiBearKingdom.Tests
{
    [TestClass]
    public class UnitTest1
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
    }
}
