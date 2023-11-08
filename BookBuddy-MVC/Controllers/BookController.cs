using Microsoft.AspNetCore.Mvc;

namespace BookBuddy_MVC.Controllers {
    public class BookController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
