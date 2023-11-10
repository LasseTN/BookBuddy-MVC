using Microsoft.CodeAnalysis;

namespace BookBuddy_MVC.Models {
    public class Book { 
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Genre? Genre { get; set; }
        public int NoOfPages { get; set; }
        public string IsbnNo { get; set; }
        public Location? Location { get; set; }
        public string Status { get; set; }
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
