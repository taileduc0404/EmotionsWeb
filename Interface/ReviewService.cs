using CamXucWeb.Data;
using CamXucWeb.Models;
using CamXucWeb.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CamXucWeb.Interface
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;
        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddReview(CamXucVM vm, bool isPositive)
        {
            var review = new Review()
            {
                Text = vm.Text,
                IsPositive = isPositive
            };
            _context.reviews!.Add(review);
            _context.SaveChanges();
        }

        public async Task<double> GetPositivePercentage()
        {
            var totalReviews = await _context.reviews!.ToListAsync();
            var countTotalReviews = totalReviews.Count();

            var positiveReviews = await _context.reviews!.ToListAsync();
            var countPositiveReviews = positiveReviews.Count(r => r.IsPositive);

            return Math.Round(CalculatePercentage(countPositiveReviews, countTotalReviews),1);
        }

        public async Task<double> GetNegativePercentage()
        {
            var totalReviews = await _context.reviews!.ToListAsync();
            var countTotalReviews = totalReviews.Count();

            var positiveReviews = await _context.reviews!.ToListAsync();
            var countPositiveReviews = positiveReviews.Count(r => !r.IsPositive);

            return Math.Round(CalculatePercentage(countPositiveReviews, countTotalReviews),1);
        }

        private double CalculatePercentage(int part, int total)
        {
            return total > 0 ? (double)part / total * 100 : 0;
        }
    }
}
