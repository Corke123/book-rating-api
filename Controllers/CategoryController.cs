using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using book_rating_api.Dtos;
using book_rating_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace book_rating_api.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService categoryService;
        private readonly IMapper mapper;

        public CategoryController(CategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        [HttpGet(Name = "Get Categories")]
        [Authorize]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await categoryService.GetAllCategories();
            var result = categories.Select(c => mapper.Map<CategoryDto>(c));

            return Ok(result);
        }
    }
}