using Microsoft.AspNetCore.Mvc;
using BookBuddy.ServiceLayer.Interface;
using BookBuddy_MVC.Models;
using BookBuddy.BusinessLogicLayer.Interface;
using BookBuddy_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookBuddy_MVC.Controllers {
    public class BooksController : Controller {

        private readonly IBookControl _bookControl;
        private readonly HttpClient _httpClient;

        public BooksController(IHttpClientFactory clientFactory, IBookControl bookControl) {
            _httpClient = clientFactory.CreateClient("API");
            _bookControl = bookControl;
        }


        // GET: Books/Create
        public async Task<IActionResult> Create() {
            var genres = await _httpClient.GetFromJsonAsync<List<GenreViewModel>>("genre");
            var locations = await _httpClient.GetFromJsonAsync<List<LocationViewModel>>("location");

            ViewBag.GenreId = new SelectList(genres, "Id", "Name");
            ViewBag.LocationId = new SelectList(locations, "Id", "Name");

            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book inBook) {
            if (!ModelState.IsValid) {
                return View(inBook);
            }
            try {
                var response = await _httpClient.PostAsJsonAsync("book", inBook);
                if (response.IsSuccessStatusCode) {
                    return RedirectToAction(nameof(Index));
                } else {
                    // Handle failure response
                    ModelState.AddModelError(string.Empty, "Server error. Please contact an administrator.");
                }
            } catch (Exception ex) {
                // Log the exception details here
                ModelState.AddModelError(string.Empty, "An error occurred while creating the book.");
            }
            return View(inBook);
        }

        // GET: BooksController
        public async Task<ActionResult> Index() {
            IEnumerable<Book> allBooks = await _bookControl.GetAllBooks();
            return View(allBooks);
        }

        // GET: BooksController/Details/5
        public ActionResult Details(int id) {
            return View();
        }


        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBook(Book inBook) {
            if (!ModelState.IsValid) {
                return View(inBook); // Return the view with validation errors
            }
            try {
                await _bookControl.CreateBook(inBook);
                return RedirectToAction(nameof(Index));
            } catch (Exception ex) {
                // Log the exception details here, for example:
                // _logger.LogError(ex, "Error creating book");
                return View(inBook);
            }
        }


        // GET: BooksController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: BooksController/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }
    }
}
