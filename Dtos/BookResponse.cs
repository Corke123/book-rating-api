using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_rating_api.Dtos
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string CoverImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public ICollection<string> MainActors { get; set; }
        public CategoryDto Category { get; set; }
        public double AverageRating { get; set; }
    }
}