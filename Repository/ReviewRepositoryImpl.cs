﻿using Database;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using movie_app_backend.Exceptions;

namespace Repository
{
    public class ReviewRepositoryImpl : IReviewRepository
    {
        private readonly MovieAppDbContext _dbContext;

        public ReviewRepositoryImpl(MovieAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Review> CreateReviewAsync(Review review)
        {
            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }

        public async Task<bool> DeleteReviewAsync(string reviewId)
        {
            var review = await _dbContext.Reviews.FirstOrDefaultAsync(x => x.ReviewId == reviewId);
            if (review == null) throw new NotFoundException($"Review with ID {reviewId} not found");
            _dbContext.Reviews.Remove(review);
            await _dbContext.SaveChangesAsync();
            return true;

        }

        public async Task<Review> UpdateReviewAsync(Review review)
        {
            var reviewToUpdate = await _dbContext.Reviews.FirstOrDefaultAsync(x => x.ReviewId == review.ReviewId);
            if (reviewToUpdate == null) throw new NotFoundException($"Review with ID {review.ReviewId} not found");

            reviewToUpdate.Author = review.Author;
            reviewToUpdate.ReviewText = review.ReviewText;
            reviewToUpdate.MovieTitle = review.MovieTitle;
            reviewToUpdate.PublishedOn = review.PublishedOn;

            await _dbContext.SaveChangesAsync();

            return reviewToUpdate;
        }

        public async Task<ICollection<Review>> GetAllReviewsAsync()
        {
            var allExistingReviews = await _dbContext.Reviews.ToListAsync();
            return allExistingReviews;
        }

        public async Task<ICollection<Review>> GetReviewByProfileIdAsync(string profileId)
        {
            return await _dbContext.Reviews.Where(x => x.RProfileId == profileId).ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(string reviewId)
        {
            var review = await _dbContext.Reviews.SingleOrDefaultAsync(x => x.ReviewId == reviewId);
            if (review == null)
            {
                throw new NotFoundException($"Review with ID {reviewId} not found");
            }

            return review;
        }

        public async Task<ICollection<Review>> GetReviewsByMovieIdAsync(string imbdId)
        {
            return await _dbContext.Reviews.Where(x => x.ImdbID == imbdId).ToListAsync();
        }

        public async Task<ICollection<Review>> GetTopReviewsAsync(string topReviewsCount, string imbdId)
        {
            var reviews = await _dbContext.Reviews.Where(x => x.ImdbID == imbdId).ToListAsync();
            return reviews.OrderByDescending(x => x.Rating).Take(int.Parse(topReviewsCount)).ToList();
        }

        public async Task<IEnumerable<string>> GetMostReviewedMoviesAsync(int topMoviesCount)
        {
            var movieReviews = await _dbContext.Reviews.GroupBy(x => x.ImdbID).ToListAsync();
            var mostReviewedMovies = movieReviews
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key)
                .Take(topMoviesCount);

            return mostReviewedMovies;
        }

        public async Task<double> GetAverageRatingForMovieAsync(string imbdId)
        {
            var reviews = await _dbContext.Reviews.Where(x => x.ImdbID == imbdId).ToListAsync();

            if (reviews.Count == 0)
            {
                throw new NotFoundException($"No reviews found for movie ID {imbdId}");
            }

            var averageRating = reviews.Sum(x => x.Rating) / reviews.Count;
            return averageRating;
        }
    }
}