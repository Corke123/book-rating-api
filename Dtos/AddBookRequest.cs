using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace book_rating_api.Dtos
{
    public class AddBookRequest
    {
        [Required]
        // [SearchOption]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateOnly ReleaseDate { get; set; }

        [Required]
        // Here also could be check AtLeastOneMainActor
        public ICollection<String> MainActors { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}