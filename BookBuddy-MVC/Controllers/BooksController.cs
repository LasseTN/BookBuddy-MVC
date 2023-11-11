using Microsoft.AspNetCore.Mvc;
using BookBuddy.ServiceLayer.Interface;
using BookBuddy_MVC.Models;
using BookBuddy.BusinessLogicLayer.Interface;

namespace BookBuddy_MVC.Controllers {
    public class BooksController : Controller {

        private readonly IBookControl _bookControl;


        public BooksController(IBookControl bookControl) {
            _bookControl = bookControl;
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

        // GET: BooksController/Create
        public ActionResult Create() {
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
