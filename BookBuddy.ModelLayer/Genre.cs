namespace BookBuddy_MVC.Models {
    public class Genre {
        public int GenreId { get; set; }
        public string GenreName { get; set; }

        public Genre() { }

        public Genre(int genreId, string genreName) {
            GenreId = genreId;
            GenreName = genreName;
        }
    }
}