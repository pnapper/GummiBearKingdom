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
    public class ReviewControllerTests : IDisposable
    {
        Mock<IReviewRepository> mock = new Mock<IReviewRepository>();
        EFReviewRepository db = new EFReviewRepository(new TestDbContext());

        private void DbSetup()
        {
            mock.Setup(m => m.Reviews).Returns(new Review[]
            {
                new Review {ReviewId = 1, Author = "Joey Muncher", ContentBody = "Great gummis, love them!", Rating = 5},
                new Review {ReviewId = 2, Author = "Jane Dough", ContentBody = "Not bad, I've had better.", Rating = 3},
                new Review {ReviewId = 3, Author = "Franky Chompers", ContentBody = "These really weren't very good.", Rating = 1}
            }.AsQueryable());
        }

        [TestMethod]
        public void Mock_AddsReviewToIndexModelData_Collection()
        {
            // Arrange
            DbSetup();
            ReviewsController controller = new ReviewsController();
            Review testReview = new Review();
            testReview.Author = "Joey Muncher";
            testReview.ContentBody = "Great gummis, love them!";
            testReview.Rating = 5;
            testReview.ReviewId = 1;

            // Act
            controller.Create(testReview);
            ViewResult indexView = new ReviewsController().Index() as ViewResult;
            var collection = indexView.ViewData.Model as List<Review>;

            // Assert
            CollectionAssert.Contains(collection, testReview);
        }

        [TestMethod]
        public void Mock_IndexContainsModelData_List() // Confirms model as list of products
        {
            // Arrange
            DbSetup();
            ViewResult indexView = new ReviewsController(mock.Object).Index() as ViewResult;

            // Act
            var result = indexView.ViewData.Model;

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<Review>));
        }

        [TestMethod]
        public void Mock_IndexModelContainsReviews_Collection() // Confirms presence of known entry
        {
            // Arrange
            DbSetup();
            ReviewsController controller = new ReviewsController(mock.Object);
            Review testReview = new Review();
            testReview.Author = "Joey Muncher";
            testReview.ContentBody = "Great gummis, love them!";
            testReview.Rating = 5;
            testReview.ReviewId = 1;

            // Act
            ViewResult indexView = controller.Index() as ViewResult;
            List<Review> collection = indexView.ViewData.Model as List<Review>;

            // Assert
            CollectionAssert.Contains(collection, testReview);
        }

        [TestMethod]
        public void Mock_PostViewResultCreate_ViewResult()
        {
            // Arrange
            Review testReview = new Review
            {
                ReviewId = 1,
                Author = "Joey Chompers",
                ContentBody = "Great gummis, love them!",
                Rating = 5
            };

            DbSetup();
            ReviewsController controller = new ReviewsController(mock.Object);

            // Act
            var resultView = controller.Create(testReview) as ViewResult;


            // Assert
            Assert.IsInstanceOfType(resultView, typeof(ViewResult));

        }

        [TestMethod]
        public void Mock_GetDetails_ReturnsView()
        {
            // Arrange
            Review testReview = new Review
            {
                ReviewId = 1,
                Author = "Joey Chompers",
                ContentBody = "Great gummis, love them!",
                Rating = 5
            };

            DbSetup();
            ReviewsController controller = new ReviewsController(mock.Object);

            // Act
            var resultView = controller.Details(testReview.ReviewId) as ViewResult;
            var model = resultView.ViewData.Model as Review;

            // Assert
            Assert.IsInstanceOfType(resultView, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Review));
        }

        [TestMethod]
        public void DB_CreatesNewEntries_Collection()
        {
            // Arrange
            ReviewsController controller = new ReviewsController(db);
            Review testReview = new Review();
            testReview.Author = "Joey Muncher";
            testReview.ContentBody = "Great gummis, love them!";
            testReview.Rating = 5;
            testReview.ReviewId = 1;

            // Act
            controller.Create(testReview);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Review>;

            // Assert
            CollectionAssert.Contains(collection, testReview);
        }

        public void Dispose()
        {
            db.ClearAll();
        }
    }
}