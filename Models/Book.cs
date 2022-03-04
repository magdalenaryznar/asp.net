using System;
using System.ComponentModel.DataAnnotations;

namespace LibApp.Models
{
    public class Book
    {
        public int Id { get; set; }
		[Required(ErrorMessage ="Name is required")]
		[StringLength(255)]
		public string Name { get; set; }
		[Required(ErrorMessage = "Author is required")]
		public string AuthorName { get; set; }
		public Genre Genre { get; set; }
		[Required(ErrorMessage = "Choose genre")]
		[Display(Name="Genre")]
		public byte GenreId { get; set; }

		public DateTime DateAdded { get; set; }
		[Display(Name="Release Date")]

		[Required(ErrorMessage = "Type release date")]
		public DateTime ReleaseDate { get; set; }
		[Display(Name = "Number in Stock")]

		[Required(ErrorMessage = "Type value between 1 and 20")]
		[Range(1, 20)]
		public int NumberInStock { get; set; }
		public int NumberAvailable { get; set; }
	}
      
}
