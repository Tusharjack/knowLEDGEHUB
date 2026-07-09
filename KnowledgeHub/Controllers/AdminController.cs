using KnowledgeHub.Models;
using KnowledgeHub.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeHub.Controllers
{
    public class AdminController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IWebHostEnvironment _environment;

        public AdminController(IPostRepository postRepository,
                               IWebHostEnvironment environment)
        {
            _postRepository = postRepository;
            _environment = environment;
        }

        //================ LOGIN ===================

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AdminLogin model)
        {
            if (model.Email == "admin@gmail.com" &&
                model.Password == "Tushar@0220")
            {
                HttpContext.Session.SetString("Admin", "LoggedIn");
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid Email or Password";
            return View(model);
        }

        //================ DASHBOARD ===================

        public async Task<IActionResult> Dashboard()
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            var posts = await _postRepository.GetAllAsync();

            ViewBag.TotalPosts = posts.Count();
            ViewBag.PublishedPosts = posts.Count(x => x.IsPublished);
            ViewBag.DraftPosts = posts.Count(x => !x.IsPublished);

            return View(posts);
        }

        //================ POSTS ===================

        public async Task<IActionResult> Posts()
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            var posts = await _postRepository.GetAllAsync();
            return View(posts);
        }

        //================ CREATE ===================

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post post, IFormFile? ImageFile)
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            // Generate slug if empty
            if (string.IsNullOrWhiteSpace(post.Slug))
            {
                post.Slug = post.Title.Trim().ToLower().Replace(" ", "-");
            }

            // Make slug unique
            post.Slug += "-" + Guid.NewGuid().ToString("N").Substring(0, 6);

            // IMAGE UPLOAD DISABLED FOR RENDER
            post.ImageUrl = null;

            await _postRepository.AddAsync(post);

            return RedirectToAction("Posts");
        }

        //================ EDIT ===================

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            var post = await _postRepository.GetByIdAsync(id);

            if (post == null)
                return NotFound();

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post, IFormFile? ImageFile)
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            // IMAGE UPLOAD DISABLED
            // Existing ImageUrl will remain unchanged.

            await _postRepository.UpdateAsync(post);

            return RedirectToAction("Posts");
        }

        //================ DELETE ===================

        public async Task<IActionResult> Delete(Guid id)
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            await _postRepository.DeleteAsync(id);

            return RedirectToAction("Posts");
        }

        //================ CATEGORIES ===================

        public IActionResult Categories()
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            return View();
        }

        //================ SETTINGS ===================

        public IActionResult Settings()
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            return View();
        }

        //================ LOGOUT ===================

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
