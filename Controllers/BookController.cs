using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using book_rating_api.Dtos;
using book_rating_api.Exceptions;
using book_rating_api.Models;
using book_rating_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace book_rating_api.Controllers
{
    [ApiController]
    [Route("api/v1/books")]
    public class BookController : ControllerBase
    {
        private readonly BookService bookService;
        private readonly ImageService imageService;
        private readonly IMapper mapper;
        private readonly ILogger<BookController> logger;


        public BookController(BookService bookService, ImageService imageService, IMapper mapper, ILogger<BookController> logger)
        {
            this.bookService = bookService;
            this.imageService = imageService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet(Name = "Get Books")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BookResponse>>> GetBooks(int page = 1, int size = 10)
        {
            // TODO filter elements to understand phrases like “4 star”, “after 2010”, “older than 3 years”.
            logger.LogInformation($"Get books by page {page} and size {size}");
            var books = await bookService.GetBooksByPageAndSize(page, size);
            var response = mapper.Map<IEnumerable<BookResponse>>(books);
            logger.LogInformation($"Result size: {response.Count()}");

            return Ok(response);
        }

        [HttpPost(Name = "Add new book")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<BookResponse>> CreateBook([FromForm][Required] AddBookRequest addBookRequest, [FromForm][Required] IFormFile coverImage)
        {
            try
            {
                logger.LogInformation("Attempt to create new book");
                var uploadResult = await imageService.UploadImage(coverImage);

                var book = mapper.Map<Book>(addBookRequest);
                book.CoverImagePublicId = uploadResult.Key;
                book.CoverImage = uploadResult.Value;

                book = await bookService.AddBook(book);

                var bookResponse = mapper.Map<BookResponse>(book);

                logger.LogInformation($"Book successffully saved, generated Id: {book.Id}");
                return CreatedAtAction(nameof(CreateBook), new { id = bookResponse.Id }, bookResponse);
            }
            catch (CategoryNotFoundException exception)
            {
                logger.LogWarning(exception, "Unknown category provided");
                return UnprocessableEntity("Unknown category!");
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Error occured, unable to save new book");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured, unable to save new book");
            }
        }

        [HttpPost("/api/v1/books/{id}/ratings")]
        [Authorize(Policy = "MemberOnly")]
        public async Task<ActionResult> RateBook(int id, [FromBody] RateRequest rateRequest)
        {
            try
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId is null)
                {
                    return Unauthorized();
                }

                var book = await bookService.GetBookById(id);
                if (book is null)
                {
                    return NotFound();
                }

                await bookService.RateBook(book.Id, int.Parse(userId), rateRequest.Rate);
                return Ok();
            }
            catch (BookNotFoundException exception)
            {
                logger.LogWarning(exception, $"Unable to find book by id {id}");
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                logger.LogInformation($"Attempt to delete book by id {id}");
                var deletedBook = await bookService.DeleteBook(id);
                await imageService.DeleteImage(deletedBook.CoverImagePublicId);
                scope.Complete();
                logger.LogInformation($"Deleted book by id {id}");
                return NoContent();
            }
            catch (BookNotFoundException exception)
            {
                logger.LogWarning(exception, $"Unable to find book by id {id}");
                return NotFound();
            }
            catch (CloudinaryException exception)
            {
                logger.LogError(exception, "Error occured, unable to delete book cover image");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured, unable to delete book cover image");
            }
        }

        [HttpPost("bulk")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UploadBooks()
        {
            using (var reader = new StreamReader(Request.Body))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var serializer = new JsonSerializer();
                while (await jsonReader.ReadAsync())
                {
                    if (jsonReader.TokenType == JsonToken.StartObject)
                    {
                        var book = serializer.Deserialize<Book>(jsonReader);
                        if (book is not null)
                        {
                            await bookService.AddBook(book);
                        }
                    }
                }
            }

            return Ok();
        }

    }
}