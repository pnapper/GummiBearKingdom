using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GummiBearKingdom.Models
{
    public class EFReviewRepository : IReviewRepository
    {
        GummiBearDbContext db;

        public EFReviewRepository()
        {
            db = new GummiBearDbContext();
        }
        public EFReviewRepository(GummiBearDbContext thisDb)
        {
            db = thisDb;
        }

        public IQueryable<Review> Reviews
        { get { return db.Reviews; } }

        public Review Save(Review product)
        {
            db.Reviews.Add(product);
            db.SaveChanges();
            return product;
        }

        public Review Edit(Review product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return product;
        }

        public void Remove(Review product)
        {
            db.Reviews.Remove(product);
            db.SaveChanges();
        }

        public void ClearAll()
        {
            List<Review> AllReviews = db.Reviews.ToList();
            db.Reviews.RemoveRange(AllReviews);
            db.SaveChanges();
        }
    }
}