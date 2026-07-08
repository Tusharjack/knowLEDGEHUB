using Microsoft.AspNetCore.Mvc;

namespace KnowledgeHub.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(string slug)
        {
            ViewBag.Slug = slug;
            return View();
        }

        public IActionResult Category(string id)
        {
            ViewBag.Category = id;
            return View("Index");
        }

        public IActionResult Search(string keyword)
        {
            ViewBag.Keyword = keyword;
            return View("Index");
        }
    }
}