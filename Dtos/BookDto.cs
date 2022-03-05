using System;
using System.ComponentModel.DataAnnotations;

namespace LibApp.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }


        public string AuthorName { get; set; }
        public GenreDto Genre { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime ReleaseDate { get; set; }


        public int NumberInStock { get; set; }
        public int NumberAvailable { get; set; }
    }
}
