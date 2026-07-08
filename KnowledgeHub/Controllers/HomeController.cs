using KnowledgeHub.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace KnowledgeHub.Controllers
{
    public class HomeController : Controller
    {

        private readonly IPostRepository _postRepository;


        public HomeController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }



        public async Task<IActionResult> Index()
        {

            var posts = await _postRepository.GetAllAsync();


            return View(posts);

        }

        public async Task<IActionResult> Details(Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id);

            if (post == null)
                return NotFound();

            return View(post);
        }


        public IActionResult About()
        {
            return View();
        }



        public IActionResult Contact()
        {
            return View();
        }

    }

    

    }