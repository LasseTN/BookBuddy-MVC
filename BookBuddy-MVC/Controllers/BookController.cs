using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookBuddy_MVC.Models; // If this is where the Book model is located
using BookBuddy.BusinessLogicLayer.Interface; // Interface for business logic
using Microsoft.Extensions.Logging;


namespace BookBuddy_MVC.Controllers {
    public class BookController : Controller {
        private readonly IBookControl _bookControl;
        private readonly ILogger<BookController> _logger;

        // Injecting IBookControl and ILogger via constructor
        public BookController(IBookControl bookControl, ILogger<BookController> logger) {
            _bookControl = bookControl;
            _logger = logger;
        }

        // Action method for the Index view, which displays the book list
        public async Task<IActionResult> Index() {
            // Try to get the list of books from the business logic layer
            IEnumerable<Book> books = await _bookControl.GetAllBooks();

            // Pass the list of books to the view
            return View(books);
        }
    }
}
