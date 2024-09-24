using Microsoft.AspNetCore.Mvc;

namespace WebApplication_Services.Controllers
{
    [Route("")]
    [ApiController]
    public class FrontendController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Frontend", "index.html"), "text/html");
        }
    }
}
