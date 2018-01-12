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

    [TestMethod]
    public void ProductController_AddsProductToIndexModelData_Collection()
    {
        // Arrange
        ProductsController controller = new ProductController();
        Product testProduc = new Product();
        testProduct.Description = "test product";

        // Act
        controller.Create(testProduct);
        ViewResult indexView = new ProductsController().Index() as ViewResult;
        var collection = indexView.ViewData.Model as List<Product>;

        // Assert
        CollectionAssert.Contains(collection, testProduct);
    }
}