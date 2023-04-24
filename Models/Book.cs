using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace book_rating_api.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string CoverImage { get; set; }
        public string CoverImagePublicId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public ICollection<Actor> MainActors { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<UserRating> UserRatings { get; set; }
    }
}
