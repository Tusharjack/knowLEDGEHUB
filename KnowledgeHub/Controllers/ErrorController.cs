using Microsoft.AspNetCore.Mvc;

namespace KnowledgeHub.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFoundPage()
        {
            return View();
        }

        public IActionResult ServerError()
        {
            return View();
        }
    }
}