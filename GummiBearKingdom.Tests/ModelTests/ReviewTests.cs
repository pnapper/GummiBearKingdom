using Microsoft.VisualStudio.TestTools.UnitTesting;
using GummiBearKingdom.Models;

namespace GummiBearKingdom.Tests
{
    [TestClass]
    public class ReviewTests
    {
        [TestMethod]
        public void GetAuthor_ReturnsReviewAuthor_String()
        {
            //Arrange
            var review = new Review();
            review.Author = "Jimmy Muncher";

            //Act
            var result = review.Author;

            //Assert
            Assert.AreEqual("Jimmy Muncher", result);
        }

        [TestMethod]
        public void GetContentBody_ReturnsReviewContentBody_String()
        {
            //Arrange
            var review = new Review();
            review.ContentBody = "Pretty good, a little on the sweet side.";

            //Act
            var result = review.ContentBody;

            //Assert
            Assert.AreEqual("Pretty good, a little on the sweet side.", result);
        }

        [TestMethod]
        public void GetRating_ReturnsReviewRating_String()
        {
            //Arrange
            var review = new Review();
            review.Rating = 4;

            //Act
            var result = review.Rating;

            //Assert
            Assert.AreEqual(4, result);
        }
    }
}