using Microsoft.AspNetCore.Mvc;

namespace BookBuddy_MVC.Controllers {
    public class MyBooksController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
