using Microsoft.AspNetCore.Mvc;
using BookBuddy.ServiceLayer.Interface;
using BookBuddy_MVC.Models;

namespace BookBuddy_MVC.Controllers {
    public class BooksController : Controller {

        private readonly IBookAccess _bookAccess;

        public BooksController(IBookAccess bookAccess) {
            _bookAccess = bookAccess;
        }
        // GET: BooksController
        public async Task<ActionResult> Index() {
            IEnumerable<Book> allBooks = await _bookAccess.GetAllBooks();
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
        public ActionResult Create(IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
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
