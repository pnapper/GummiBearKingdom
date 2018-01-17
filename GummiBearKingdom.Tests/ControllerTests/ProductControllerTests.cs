using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GummiBearKingdom.Models;
using GummiBearKingdom.Tests.Models;
using GummiBearKingdom.Controllers;
using Moq;
using System.Linq;
using System;

namespace GummiBearKingdom.Tests.ControllerTests
{
    [TestClass]
    public class ProductControllerTests : IDisposable
    {
        Mock<IProductRepository> mock = new Mock<IProductRepository>();
        EFProductRepository db = new EFProductRepository(new TestDbContext());

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
        public void Mock_AddsProductToIndexModelData_Collection()
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

        [TestMethod]
        public void Mock_IndexContainsModelData_List() // Confirms model as list of products
        {
            // Arrange
            DbSetup();
            ViewResult indexView = new ProductsController(mock.Object).Index() as ViewResult;

            // Act
            var result = indexView.ViewData.Model;

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }

        [TestMethod]
        public void Mock_IndexModelContainsProducts_Collection() // Confirms presence of known entry
        {
            // Arrange
            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);
            Product testProduct = new Product();
            testProduct.Name = "5 Lb. Bag (Assorted Flavors)";
            testProduct.Price = 12.99m;
            testProduct.Description = "Yummi Gummis!";
            testProduct.ProductId = 1;

            // Act
            ViewResult indexView = controller.Index() as ViewResult;
            List<Product> collection = indexView.ViewData.Model as List<Product>;

            // Assert
            CollectionAssert.Contains(collection, testProduct);
        }

        [TestMethod]
        public void Mock_PostViewResultCreate_ViewResult()
        {
            // Arrange
            Product testProduct = new Product
            {
                ProductId = 1,
                Name = "5 Lb. Bag (Assorted Flavors)",
                Price = 12.99m,
                Description = "Yummi Gummis!"
            };

            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);

            // Act
            var resultView = controller.Create(testProduct) as ViewResult;


            // Assert
            Assert.IsInstanceOfType(resultView, typeof(ViewResult));

        }

        [TestMethod]
        public void Mock_GetDetails_ReturnsView()
        {
            // Arrange
            Product testProduct = new Product
            {
                ProductId = 1,
                Name = "5 Lb. Bag (Assorted Flavors)",
                Price = 12.99m,
                Description = "Yummi Gummis!"
            };

            DbSetup();
            ProductsController controller = new ProductsController(mock.Object);

            // Act
            var resultView = controller.Details(testProduct.ProductId) as ViewResult;
            var model = resultView.ViewData.Model as Product;

            // Assert
            Assert.IsInstanceOfType(resultView, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Product));
        }

        [TestMethod]
        public void DB_CreatesNewEntries_Collection()
        {
            // Arrange
            ProductsController controller = new ProductsController(db);
            Product testProduct = new Product();
            testProduct.Name = "5 Lb. Bag (Assorted Flavors)";
            testProduct.Price = 12.99m;
            testProduct.Description = "Yummi Gummis!";
            testProduct.ProductId = 1;

            // Act
            controller.Create(testProduct);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            // Assert
            CollectionAssert.Contains(collection, testProduct);
        }

        [TestMethod]
        public void DB_EditsEntries_Collection()
        {
            ProductsController controller = new ProductsController(db);
            Product testProduct = new Product();
            testProduct.Name = "5 Lb. Bag (Assorted Flavors)";
            testProduct.Price = 12.99m;
            testProduct.Description = "Yummi Gummis!";
            testProduct.ProductId = 1;

            // Act
            controller.Create(testProduct);
            testProduct.Name = "2.5 Lb. Bag (Assorted Flavors)";
            controller.Edit(testProduct);
            var foundProduct = (controller.Details(testProduct.ProductId) as ViewResult).ViewData.Model as Product;

            // Assert
            Assert.AreEqual(foundProduct.Name, "2.5 Lb. Bag (Assorted Flavors)");
        }

        public void Dispose()
        {
            db.ClearAll();
        }
    }
}