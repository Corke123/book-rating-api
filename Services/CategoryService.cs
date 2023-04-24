using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using book_rating_api.data;
using book_rating_api.Models;

namespace book_rating_api.Services
{
    public class CategoryService
    {
        private readonly DataContext dataContext;

        public CategoryService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await dataContext.Categories.ToListAsync();
        }
    }
}