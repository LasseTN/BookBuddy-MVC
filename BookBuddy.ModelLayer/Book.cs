using Microsoft.CodeAnalysis;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace BookBuddy_MVC.Models {
    public class Book {
        [DisplayName("Id")]
        public int BookId { get; set; }
        [JsonPropertyName("genreId")]
        public int? GenreId { get; set; }
        [JsonPropertyName("loactionId")]
        public int? LocationId { get; set; } 
        public string Title { get; set; }
        public string Author { get; set; }
        public Genre? Genre { get; set; }
        [DisplayName("Pages")]
        public int NoOfPages { get; set; }
        [DisplayName("ISBN")]
        public string IsbnNo { get; set; }
        public Location? Location { get; set; }
        public string Status { get; set; }
        [DisplayName("Book cover")]
        public string ImageURL { get; set; }

        public Book() { }

        public Book(int bookId, string title, string author, Genre? genre, int noOfPages, string isbnNo, Location? location, string status, string imageURL) {
            BookId = bookId;
            Title = title;
            Author = author;
            Genre = genre;
            NoOfPages = noOfPages;
            IsbnNo = isbnNo;
            Location = location;
            Status = status;
            ImageURL = imageURL;
        }
    }
}
