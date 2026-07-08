using Dapper;
using KnowledgeHub.Data;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeHub.Controllers
{
    public class TestController : Controller
    {
        private readonly DbConnectionFactory _db;

        public TestController(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                using var connection = _db.CreateConnection();

                var categories = await connection.QueryAsync(
                    "SELECT * FROM categories");

                return Json(categories);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }
    }
}