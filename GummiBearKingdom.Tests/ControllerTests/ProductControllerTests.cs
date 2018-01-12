using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GummiBearKingdom.Models;
using GummiBearKingdom.Controllers;
using Moq;
using System.Linq;
using System;

namespace GummiBearKingdom.Tests.ControllerTests
{
    [TestClass]
    public class ProductControllerTests
    {
        Mock<IProductRepository> mock = new Mock<IProductRepository>();

        private void DbSetup()
        {
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, Name = "5 Lb. Bag (Assorted Flavors)", Price = 12.99m, Description = "Yummi Gummis!" },
                new Product {ProductId = 2, Name = "2.5 Lb. Bag (Assorted Flavors)", Price = 4.99m, Description = "Yummi Gummis!" },
                new Product {ProductId = 3, Name = "Blue Raspberry 1 lb Bulk Package", Price = 2.99m, Description = "Yummi Gummis!" }
            }.AsQueryable());
        }


        [TestMethod]
        public void ProductsController_AddsProductToIndexModelData_Collection()
        {
            // Arrange
            ProductsController controller = new ProductsController();
            Product testProduct = new Product();
            testProduct.Description = "test product";

            // Act
            controller.Create(testProduct);
            ViewResult indexView = new ProductsController().Index() as ViewResult;
            var collection = indexView.ViewData.Model as List<Product>;

            // Assert
            CollectionAssert.Contains(collection, testProduct);
        }
    }
}