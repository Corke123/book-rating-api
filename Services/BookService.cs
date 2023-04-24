using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using book_rating_api.data;
using book_rating_api.Exceptions;
using book_rating_api.Models;
using Microsoft.EntityFrameworkCore;

namespace book_rating_api.Services
{
    public class BookService
    {
        private readonly DataContext dataContext;

        public BookService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<IEnumerable<Book>> GetBooksByPageAndSize(int page, int size)
        {
            return await dataContext.Books
                .Include(b => b.Category)
                .Include(b => b.MainActors)
                .Include(b => b.UserRatings)
                .OrderByDescending(b => b.UserRatings.Average(r => r.Rating))
                .OrderBy(b => b.Title)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await dataContext.Books.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> AddBook(Book book)
        {
            var category = await GetCategoryById(book.CategoryId);
            book.Category = category;

            dataContext.Books.Add(book);
            await dataContext.SaveChangesAsync();

            return book;
        }

        public async Task<Book> DeleteBook(int id)
        {
            var book = await dataContext.Books
                .Include(b => b.MainActors)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book is null)
            {
                throw new BookNotFoundException($"There is no book with id: {id}");
            }

            dataContext.Actors.RemoveRange(book.MainActors);
            dataContext.Books.Remove(book);
            await dataContext.SaveChangesAsync();

            return book;
        }

        public async Task RateBook(int bookId, int userId, int rating)
        {
            var userRating = new UserRating
            {
                BookId = bookId,
                UserId = userId,
                Rating = rating
            };

            dataContext.UserRatings.Add(userRating);
            await dataContext.SaveChangesAsync();
        }

        private async Task<Category> GetCategoryById(int categoryId)
        {
            var result = await dataContext.Categories.FindAsync(categoryId);

            if (result is null)
            {
                throw new CategoryNotFoundException("Unknown category provided.");
            }

            return result;
        }
    }
}